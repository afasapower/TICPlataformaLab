using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.InfraEstrutura.Helpers;
using Plataforma.Services.Interfaces.Sistema;
using Plataforma.Ui.OrgTs.ViewModel.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Ui.OrgTs.Controllers.Sistema
{
    public class Sistema_UsuarioController : BaseController
    {
        public IUsuarioService UsuarioService { get; private set; }
        public ILog_Erro_AplicacaoService Log_Erro_AplicacaoService { get; private set; }
        public IGrupo_UsuarioService Grupo_UsuarioService { get; private set; }
        public IGrupoService GrupoService { get; private set; }
        public IPessoaService PessoaService { get; private set; }
        public IUsuario_EmpresaService Usuario_EmpresaService { get; private set; }
        private IHostingEnvironment hostingEnv;

        public Sistema_UsuarioController(IUsuarioService usuarioService,
                                         ILog_Erro_AplicacaoService log_Erro_AplicacaoService,
                                         IGrupo_UsuarioService grupo_UsuarioService,
                                         IGrupoService grupoService,
                                         IPessoaService pessoaService,
                                         IUsuario_EmpresaService usuario_EmpresaService,
                                         IHostingEnvironment env,
                                         IMapper mapper) : base(mapper)
        {
            UsuarioService = usuarioService;
            Log_Erro_AplicacaoService = log_Erro_AplicacaoService;
            Grupo_UsuarioService = grupo_UsuarioService;
            GrupoService = grupoService;
            PessoaService = pessoaService;
            Usuario_EmpresaService = usuario_EmpresaService;
            this.hostingEnv = env;
        }

        [Authorize]
        public IActionResult Index(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                   Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, new Parametros_Busca_Grid());
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, new Parametros_Busca_Grid());
            return View(listaPermissoesMenus);
        }

        #region Cadastro de Grid
        [Authorize, HttpPost]
        public IActionResult SelecionarTodos(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                             Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            var usuario = UsuarioService.GetListGrid(_Parametros_Busca_Navegacao.id_empresa, Guid.Empty, _Parametros_Busca_Grid);
            return Json(new { draw = _Parametros_Busca_Grid.draw, recordsTotal = usuario.Count(), recordsFiltered = 0, data = usuario });
        }
        #endregion

        #region Cadastro de Usuario

        [Authorize]
        public IActionResult Interno()
        {
            return View();
        }

        [Authorize, Route("Sistema_Usuario/usuario-aba")]
        public IActionResult Usuario(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao, Parametros_Pessoa _Parametros_Pessoa)
        {
            Parametros_Busca_Grid _Parametros_Busca_Grid = new Parametros_Busca_Grid()
            {
                length = 1000
            };

            Configuracao_Sistema configuracao = new Configuracao_Sistema();
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, new Parametros_Busca_Grid());
            Usuario usuarioId = UsuarioService.GetById(_Parametros_Busca_Navegacao.id) ?? new Usuario();
            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, new Parametros_Busca_Grid());
            List<Dados_Usuario_Empresa> listaUsuarioEmpresa = Usuario_EmpresaService.GetListDadosUsuario(PessoaService, Usuario_EmpresaService, _Parametros_Busca_Navegacao.id_usuario_logado, _Parametros_Busca_Navegacao.id_empresa, _Parametros_Busca_Grid, configuracao);

            List<Grupo_Usuario> listaGrupoUsuario = Grupo_UsuarioService.GetList(_Parametros_Busca_Navegacao.id, Guid.Empty, _Parametros_Busca_Grid).ToList();

            var usuario = new UsuarioViewModel()
            {
                usuario = usuarioId,
                pessoa = (usuarioId == null ? new Pessoa() : PessoaService.GetById(usuarioId.id_pessoa)),
                listaPermissoesMenus = listaPermissoesMenus,
                listaDadosUsuarioEmpresa = listaUsuarioEmpresa,
                grupo_usuario = (listaGrupoUsuario.Count > 0 ? listaGrupoUsuario[0] : new Grupo_Usuario())
            };
            return View("usuario-aba", usuario);
        }

        [Authorize, Route("Sistema_Usuario/usuario-aba"), HttpPost, ValidateAntiForgeryToken]
        public IActionResult UsuarioAbaOperacoes(UsuarioViewModel usuarioView, 
                                                 Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                                 Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            bool status = true;
            object data = null;

            Pessoa pessoausuario = new Pessoa();
            pessoausuario.data_nascimento_abertura = usuarioView.data_nascimento_abertura;
            pessoausuario.razao_social_nome = usuarioView.razao_social_nome;
            pessoausuario.usuario = _Parametros_Busca_Navegacao.usuario;
            
            Usuario usuarios = new Usuario();
            usuarios.id = _Parametros_Busca_Navegacao.id;
            usuarios.usuario = _Parametros_Busca_Navegacao.usuario;
            usuarios.login = usuarioView.login;
            usuarios.acesso_restrito = usuarioView.acesso_restrito;
            usuarios.id_pessoa_empresa = _Parametros_Busca_Navegacao.id_pessoa_empresa;

            Usuario_Empresa usuario_Empresa = new Usuario_Empresa()
            {
                excluido = false,
                id_pessoa_empresa = _Parametros_Busca_Navegacao.id_pessoa_empresa,                
                usuario = _Parametros_Busca_Navegacao.usuario
            };

            var _usuario = UsuarioService.GetById(usuarios.id);

            List<Usuario> users = UsuarioService.GetList(Guid.Empty, Guid.Empty, _Parametros_Busca_Grid).ToList();

            try
            {
                if (_Parametros_Busca_Navegacao.acao == "D")
                {
                    Usuario excluirUsuario = UsuarioService.GetById(_Parametros_Busca_Navegacao.id);
                    // Exclui a Pessoa
                    Pessoa excluirPessoa = PessoaService.GetById(excluirUsuario.id_pessoa);
                    excluirPessoa.usuario = _Parametros_Busca_Navegacao.usuario;
                    excluirPessoa.excluido = true;
                    PessoaService.Update(excluirPessoa);   
                    

                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        pessoausuario.nome_fantasia_apelido = usuarioView.razao_social_nome;
                        pessoausuario.user = true;
                        pessoausuario.id_pessoa_empresa = _Parametros_Busca_Navegacao.id_pessoa_empresa;

                        switch (_Parametros_Busca_Navegacao.acao)
                        {
                            case "I":
                                if (!users.Exists(x => x.login == usuarioView.login))
                                {
                                    // Verifica se o usuário existe no banco de dados - Por empresa
                                    if (UsuarioService.ValidarUsuario(_Parametros_Busca_Navegacao.id_pessoa_empresa, usuarioView.login) == false)
                                    {
                                        // Adiciona a Pessoa
                                        PessoaService.Add(pessoausuario);

                                        usuarios.id_pessoa = pessoausuario.id;
                                        usuarios.usuario = _Parametros_Busca_Navegacao.usuario;

                                        string senhaAutomatica = Seguranca.GeradorDeSenha();
                                        usuarios.senha = Seguranca.MD5Hash(senhaAutomatica);


                                        // Grava Usuário na tabela Usuario
                                        UsuarioService.Add(usuarios);

                                        // Adiciona grupo de usuário                                   

                                        Grupo_Usuario insertGrupo = new Grupo_Usuario()
                                        {
                                            id_grupo = usuarioView.id_grupo,
                                            id_usuario = usuarios.id,
                                            usuario = _Parametros_Busca_Navegacao.usuario
                                        };
                                        Grupo_UsuarioService.Add(insertGrupo);

                                        // Grava Usuário Empresa
                                        usuario_Empresa.id_usuario = usuarios.id;
                                        Usuario_EmpresaService.Add(usuario_Empresa);


                                        string corpoEmail = $"<ul>" +
                                            $"<li><b>Nome do usuário:</b> {usuarios.login}</li>" +
                                            $"<li><b>Senha:</b> {senhaAutomatica}</li>" +
                                            $"<li><a href=\"http://tribunaleclesiastico.orgts.com.br\" target=\"_blank\"><h4>Acesse Tribunal Eclesiástico</h4></a></li>" +
                                            $"</ul>";

                                        Configuracao_Sistema _Configuracao_Sistema = new Configuracao_Sistema()
                                        {
                                            DadosConexaoEmail = Startup.DadosEmail
                                        };

                                        EmailEnvio.Email(usuarioView.email, "Cadastro de usuário Tribunal", corpoEmail, _Configuracao_Sistema);
                                    }
                                }
                                else
                                {
                                    status = false;
                                    ModelState.AddModelError("1001", "Usuário já existente na base de dados!");
                                    data = Formularios.CapturaModelErros(ModelState, true);
                                }
                                    break;
                            case "U":

                                // Edita Usuario na tabela Usuario 
                                Usuario UpdateUsuario = UsuarioService.GetById(_Parametros_Busca_Navegacao.id);
                                if (UpdateUsuario.login != usuarioView.login)
                                {
                                    if (!users.Exists(x => x.login == usuarioView.login))
                                    {
                                        UpdateUsuario.login = usuarioView.login;
                                    }
                                    else
                                    {
                                        ModelState.AddModelError("1002", "Login já existente");
                                        status = false;
                                        data = Formularios.CapturaModelErros(ModelState, true);
                                        break;
                                    }
                                }

                                UpdateUsuario.usuario = _Parametros_Busca_Navegacao.usuario;
                                //UpdateUsuario.login = usuarioView.login;
                                UpdateUsuario.id_pessoa_empresa = _Parametros_Busca_Navegacao.id_pessoa_empresa;
                                UpdateUsuario.acesso_restrito = usuarioView.acesso_restrito;
                                UsuarioService.Update(UpdateUsuario);

                                // Edita grupo de usuário
                                Grupo_UsuarioService.RemoveAll(UpdateUsuario.id);

                                Grupo_Usuario insertNovoGrupo = new Grupo_Usuario()
                                {
                                    id_grupo = usuarioView.id_grupo,
                                    id_usuario = UpdateUsuario.id,
                                    usuario = _Parametros_Busca_Navegacao.usuario
                                };

                                Grupo_UsuarioService.Add(insertNovoGrupo);

                                // Permissão usuario empresa

                                //Usuario_EmpresaRepository.RemoveAll(UpdateUsuario.id);

                                // Edita pessoa na tabela Pessoa
                                Pessoa UpdatePessoa = PessoaService.GetById(UpdateUsuario.id_pessoa);
                                UpdatePessoa.data_nascimento_abertura = usuarioView.data_nascimento_abertura;
                                UpdatePessoa.razao_social_nome = usuarioView.razao_social_nome;
                                UpdatePessoa.usuario = _Parametros_Busca_Navegacao.usuario;
                                UpdatePessoa.id_pessoa_empresa = _Parametros_Busca_Navegacao.id_pessoa_empresa;
                                UpdatePessoa.id = UpdateUsuario.id_pessoa;
                                PessoaService.Update(UpdatePessoa);

                                Parametros_Pessoa _Parametros_Pessoa = new Parametros_Pessoa()
                                {
                                    id = UpdatePessoa.id
                                };

                                // Edita Usuario Empresa
                                List<Usuario_Empresa> existeUsuario = Usuario_EmpresaService.GetList(_Parametros_Busca_Navegacao.id_modulo, _Parametros_Busca_Navegacao.id_pessoa_empresa, _Parametros_Busca_Grid).ToList();

                                if (existeUsuario.Count() == 0)
                                {
                                    Usuario_EmpresaService.Add(usuario_Empresa);
                                }

                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        status = false;
                        data = Formularios.CapturaModelErros(ModelState);
                    }
                }
            }
            catch (Exception ex)
            {
                Log_Erro_AplicacaoService.Add(new Log_Erro_Aplicacao() {
                id_pessoa_empresa = _Parametros_Busca_Navegacao.id_empresa,
                origem = "Cadastro Usuario",
                mensagem = ex.Message + " - " + ex.StackTrace,
                objeto = usuarios.ToString(),
                metodo = "Sistema_UsuarioController - UsuarioAbaOperacoes",
                usuario = usuarios.usuario
            });
                ModelState.AddModelError("1001", "Erro Não Tratado");
                status = false;
                data = Formularios.CapturaModelErros(ModelState, true);
            }
            return Json(new { status = status, data = data, id = usuarios.id });
        }

        [Authorize, Route("Sistema_Usuario/gruposUsuarios")]
        public IActionResult GruposUsuarios(Guid valor, Guid id_empresa)
        {
            Parametros_Busca_Grid _Parametros_Busca_Grid = new Parametros_Busca_Grid()
            {
                length = 1000
            };

            List<Guid> valores = new List<Guid>();
            List<string> descricao = new List<string>
            {
                "Selecione uma grupo"
            };
            valores.Add(Guid.Empty);
            List<Grupo> grupo = GrupoService.GetList(valor, Guid.Empty, _Parametros_Busca_Grid).ToList();
            foreach (var item in grupo)
            {
                valores.Add(item.id);
                descricao.Add(item.nome);
            }
            if (valores.Count == 1)
            {
                descricao = new List<string>();
                valores = new List<Guid>
                {
                    Guid.Empty
                };
                descricao.Add("Nenhuma grupo cadastrado");
            }
            return Json(new { valores = valores, descricao = descricao });
        }

        #endregion

        #region Cadastro de Usuários em Grupos

        [Authorize, HttpPost]
        public JsonResult GruposelecionarTodos(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao)
        {            
            Parametros_Busca_Grid _Parametros_Busca_Grid = new Parametros_Busca_Grid();
            List<Grupo_Usuario> grupos = Grupo_UsuarioService.GetList(_Parametros_Busca_Navegacao.iditem, Guid.Empty, _Parametros_Busca_Grid).ToList();
           
            return Json(new { draw = _Parametros_Busca_Grid.draw, recordsTotal = grupos.Count(), recordsFiltered = 0, data = grupos });
        }

        [Authorize, Route("Sistema_Usuario/grupousuario-aba")]
        public IActionResult Grupo(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao)
        {
            Parametros_Busca_Grid _Parametros_Busca_Grid = new Parametros_Busca_Grid()
            {
                length = 1000
            };

            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, new Parametros_Busca_Grid());
            Grupo_Usuario grupoUsuario = Grupo_UsuarioService.GetById(_Parametros_Busca_Navegacao.id);
            Usuario usuario  = UsuarioService.GetById((grupoUsuario != null ? grupoUsuario.id_usuario : _Parametros_Busca_Navegacao.id)) ?? new Usuario();

            var grupoUsuarioModel = new UsuarioViewModel()
            {
                grupo_usuario = grupoUsuario ?? new Grupo_Usuario(), // Grupo_UsuarioRepository.GetById(parametros.id),
                listagrupo = GrupoService.GetList(usuario.id_pessoa_empresa, Guid.Empty, _Parametros_Busca_Grid).ToList()
            };
            return View("grupousuario-aba", grupoUsuarioModel);
        }

        [Authorize, Route("Sistema_Usuario/grupousuario-aba"), HttpPost, ValidateAntiForgeryToken]
        public IActionResult GrupoAbaOperacoes(Grupo_Usuario grupo, Parametros_Busca_Navegacao _Parametros_Busca_Navegacao)
        {
            Parametros_Busca_Grid _Parametros_Busca_Grid = new Parametros_Busca_Grid();

            bool status = true;
            object data = null;
            try
            {
                if (_Parametros_Busca_Navegacao.acao == "D")
                {
                    var excluir = Grupo_UsuarioService.GetById(grupo.id);
                    excluir.excluido = true;
                    Grupo_UsuarioService.Update(excluir);
                }
                else
                    if (ModelState.IsValid)
                {
                    switch (_Parametros_Busca_Navegacao.acao)
                    {
                        case "I":
                            // Verifica se o usuário já esta cadastrado com o grupo
                            var _grupo_usuario = Grupo_UsuarioService.GetList(_Parametros_Busca_Navegacao.id_modulo, grupo.id_grupo, _Parametros_Busca_Grid);

                            if (_grupo_usuario.Count() == 0)
                            {
                                grupo.id_usuario = _Parametros_Busca_Navegacao.id_modulo;
                                grupo.usuario = _Parametros_Busca_Navegacao.usuario;
                                Grupo_UsuarioService.Add(grupo);
                            }
                            else
                            {
                                status = false;
                                ModelState.AddModelError("1001", "Usuário já cadastrado com o grupo desejado!");
                                data = Formularios.CapturaModelErros(ModelState, true);
                            }

                            break;
                        case "U":
                            grupo.usuario = _Parametros_Busca_Navegacao.usuario;
                            grupo.id_usuario = _Parametros_Busca_Navegacao.id_modulo;
                            Grupo_UsuarioService.Update(grupo);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    status = false;
                    data = Formularios.CapturaModelErros(ModelState);
                }
            }
            catch (Exception ex)
            {
                Log_Erro_AplicacaoService.Add(new Log_Erro_Aplicacao()
                { 
                    origem = "Cadastro Grupo Usuario",
                    mensagem = ex.Message + " - " + ex.StackTrace,
                    objeto = grupo.id.ToString(),
                    metodo = "Sistema_UsuarioController - GrupoAbaOperacoes",
                    usuario = _Parametros_Busca_Navegacao.usuario
                });
                ModelState.AddModelError("1001", "Erro Não Tratado");
                status = false;
                data = Formularios.CapturaModelErros(ModelState, true);
            }
            return Json(new { status = status, data = data, id = grupo.id });
        }

        #endregion

        [Authorize, Route("Sistema_Usuario/usuarioApp-aba")]
        public IActionResult UsuarioAppAba(Parametros_Busca_Navegacao _Parametros_Busca_Navegacao)
        {
            Parametros_Busca_Grid _Parametros_Busca_Grid = new Parametros_Busca_Grid()
            {
                length = 100
            };

            Configuracao_Sistema configuracao = new Configuracao_Sistema();
            UsuarioService.ValidaPagina(User, _Parametros_Busca_Navegacao.id_pagina, Response, new Parametros_Busca_Grid());
            Usuario usuario = UsuarioService.GetById(_Parametros_Busca_Navegacao.id_modulo);
            List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus = UsuarioService.ValidaMenusUtilitarios(User, _Parametros_Busca_Navegacao.id_pagina, new Parametros_Busca_Grid());
            List<Dados_Usuario_Empresa> listaUsuarioEmpresa = Usuario_EmpresaService.GetListDadosUsuario(PessoaService, Usuario_EmpresaService, _Parametros_Busca_Navegacao.id_usuario_logado, _Parametros_Busca_Navegacao.id_empresa, _Parametros_Busca_Grid, configuracao);

            var usuApp = new UsuarioViewModel()
            {
                usuario = usuario,
                listaPermissoesMenus = listaPermissoesMenus,
                listaDadosUsuarioEmpresa = listaUsuarioEmpresa
            };
            return View("usuarioApp-aba", usuApp);
        }        
    }
}