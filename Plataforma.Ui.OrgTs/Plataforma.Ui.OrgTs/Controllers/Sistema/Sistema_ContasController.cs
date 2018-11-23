using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace Plataforma.Ui.OrgTs.Controllers.Sistema
{
    public class Sistema_ContasController : BaseController
    {
        public IUsuarioService UsuarioService { get; private set; }
        public IUsuario_Empresa_AtivoService Usuario_Empresa_AtivoService { get; set; }
        public ILog_Erro_AplicacaoService Log_Erro_AplicacaoService { get; set; }
        private IHostingEnvironment hostingEnv;

        public Sistema_ContasController(IUsuarioService usuarioService,
                                        IUsuario_Empresa_AtivoService usuario_Empresa_AtivoService,
                                        ILog_Erro_AplicacaoService log_Erro_AplicacaoService,
                                        IHostingEnvironment env,
                                        IMapper mapper) : base(mapper)
        {
            UsuarioService = usuarioService;
            Usuario_Empresa_AtivoService = usuario_Empresa_AtivoService;
            Log_Erro_AplicacaoService = log_Erro_AplicacaoService;
            this.hostingEnv = env;
        }

        // GET: /<controller>/
        public IActionResult NaoAutorizado()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult Proibido()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewBag.Retorno = true;
            ViewBag.httpTipo = "get";
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, string returnUrl)
        {
            ViewBag.httpTipo = "POST";

            bool autenticado = false;
            string urlAutenticacao = "";

            ApiFacef apiFacef;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("http://dev2.unifacef.com.br:8000/api/matriculadoGrad/" + username).Result;

                if (response.IsSuccessStatusCode)
                {
                    string conteudo =
                        response.Content.ReadAsStringAsync().Result;

                    dynamic resultado = JsonConvert.DeserializeObject(conteudo);

                    apiFacef = new ApiFacef();
                    if (conteudo.Length > 2)
                    {
                        apiFacef.id_aluno = resultado[0].id_aluno;
                        apiFacef.nome_completo_aluno = resultado[0].nome_completo_aluno;
                        apiFacef.hash_senha = resultado[0].hash_senha;
                    }



                    List<Usuario> dadosAutenticacao = UsuarioService.Autenticar("unifacef", "e10adc3949ba59abbe56e057f20f883e").ToList();
                    if (apiFacef.id_aluno.ToString() == username && apiFacef.hash_senha == password)
                    {
                        List<Claim> claimsUsuario = new List<Claim>
                        {
                            new Claim(ClaimTypes.Role, "Tribunal"),
                            new Claim(ClaimTypes.Role, "admin"),
                            new Claim("id_pessoa_empresa", dadosAutenticacao[0].id_pessoa_empresa.ToString()),
                            new Claim("id_empresa_atual", dadosAutenticacao[0].id_pessoa_empresa.ToString()),
                            new Claim("id", dadosAutenticacao[0].id.ToString()),
                            new Claim("id_pessoa", dadosAutenticacao[0].id_pessoa.ToString()),
                            new Claim("nome", dadosAutenticacao[0].Pessoa.razao_social_nome),
                            new Claim("login", dadosAutenticacao[0].login),
                            new Claim("acesso_restrito", dadosAutenticacao[0].acesso_restrito.ToString())
                        };

                        // Ativa o usuário na empresa - Usuario_Empresa_Ativo
                        AlteraUsuarioEmpresa(new Guid(claimsUsuario.FirstOrDefault(x => x.Type == "id").Value), new Guid(dadosAutenticacao[0].id_pessoa_empresa.ToString()));

                        var claimsIdentity = new ClaimsIdentity(claimsUsuario, "usuario");
                        var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);

                        await HttpContext.SignInAsync("Cookies", claimsPrinciple,
                                new AuthenticationProperties
                                {
                                    // ExpiresUtc = DateTime.Now.AddMinutes(2),
                                    ExpiresUtc = DateTime.Now.AddHours(8),
                                    IsPersistent = false,
                                    AllowRefresh = false
                                });

                        if (Url.IsLocalUrl("~/"))
                        {
                            autenticado = true;
                            urlAutenticacao = "//" + HttpContext.Request.Host.Value;
                        }
                    }
                }
            }

            return Json(new { autenticado, urlAutenticacao });
         
        }

        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public JsonResult DadosUsuario()
        {
            IEnumerable<Claim> dados = ((ClaimsIdentity)User.Identity).Claims.ToList();
            List<Usuario> filtro = new List<Usuario>()
            {
                new Usuario
                {
                    id = new Guid(dados.FirstOrDefault(x => x.Type == "id").Value),
                    id_pessoa = new Guid(dados.FirstOrDefault(x => x.Type == "id_pessoa").Value),
                    id_pessoa_empresa = new Guid(dados.FirstOrDefault(x => x.Type == "id_pessoa_empresa").Value),
                    login = dados.FirstOrDefault(x => x.Type == "login").Value
                }
            };

            return Json(new { dados = filtro });
        }

        [Authorize]
        public JsonResult AlteraUsuarioEmpresa(Guid id_usuario, Guid id_empresa)
        {
            // Remove todas empresas ativas do usuário
            Func<Usuario_Empresa_Ativo, bool> predicate = (entity => entity.id_usuario == id_usuario);
            Usuario_Empresa_AtivoService.Remove(predicate);

            string usuarioAtivo = "";
            IEnumerable<Claim> dados = ((ClaimsIdentity)User.Identity).Claims.ToList();          
            List<Claim> claimsUsuario = dados.ToList();
            if (claimsUsuario.Count > 0)
            {
                claimsUsuario.RemoveAll(x=> x.Type == "id_empresa_atual");
                claimsUsuario.Add(new Claim("id_empresa_atual", id_empresa.ToString()));

                usuarioAtivo = claimsUsuario.Where(x => x.Type == "login").ToList()[0].Value;

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claimsUsuario, "usuario");
                ClaimsPrincipal claimsPrinciple = new ClaimsPrincipal(claimsIdentity);

                HttpContext.SignInAsync("Cookies", claimsPrinciple, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddHours(8),
                    IsPersistent = false,
                    AllowRefresh = false
                });
            }

            Usuario_Empresa_Ativo _Usuario_Empresa_Ativo = new Usuario_Empresa_Ativo()
            {
                id_usuario = id_usuario,
                id_pessoa_empresa = id_empresa,
                usuario = (String.IsNullOrEmpty(usuarioAtivo) ? "Sistema" : usuarioAtivo)
            };

            Parametros_Busca_Grid _Parametros_Busca_Grid = new Parametros_Busca_Grid();

            // Gera a empresa ativa para o usuário
            Usuario_Empresa_AtivoService.Add(_Usuario_Empresa_Ativo);

            List<Usuario_Empresa_Ativo> dadosUsuario = Usuario_Empresa_AtivoService.GetList_Empresa_Ativa(id_usuario, _Parametros_Busca_Grid).ToList();

            return Json(new { dados = dadosUsuario[0], id_empresa = id_empresa });
        }
    }
}