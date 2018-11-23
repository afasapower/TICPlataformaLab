using Microsoft.AspNetCore.Mvc;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using Plataforma.Ui.OrgTs.ViewModel.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Ui.OrgTs.ViewComponents
{
    public class ComboPessoas : ViewComponent
    {
        public IModuloService ModuloService { get; private set; }
        public IMenuService MenuService { get; private set; }
        public IMenu_SubService Menu_SubService { get; private set; }
        public IUsuario_EmpresaService Usuario_EmpresaService { get; private set; }
        public IPessoaService PessoaService { get; private set; }
        public Guid IdOrgsystem { get; set; }

        public ComboPessoas(IModuloService moduloService,
                         IMenuService menuService,
                         IMenu_SubService menu_SubService,
                         IUsuario_EmpresaService usuario_EmpresaService,
                         IPessoaService pessoaService)
        {
            ModuloService = moduloService;
            MenuService = menuService;
            Menu_SubService = menu_SubService;
            Usuario_EmpresaService = usuario_EmpresaService;
            PessoaService = pessoaService;
            IdOrgsystem = Startup.id_empresa_unifacef;
        }

        public IViewComponentResult Invoke(ComboPessoaViewModel parametros,
                                           Parametros_Busca_Navegacao _Parametros_Busca_Navegacao,
                                           Parametros_Busca_Grid _Parametros_Busca_Grid)
        {
            // Captura ID do usuario logado
            string id = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "id").Value;
            string idEmpresaClaim = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "id_pessoa_empresa").Value;
            string id_pessoa_empresa = string.Empty;

            if (parametros.idPessoaEmpresa == Guid.Empty)
            {
                id_pessoa_empresa = parametros.idPessoaEmpresa.ToString();

                if(new Guid(idEmpresaClaim) == IdOrgsystem)
                {
                    id_pessoa_empresa = IdOrgsystem.ToString();
                }
            }else{
                id_pessoa_empresa = parametros.idPessoaEmpresa.ToString();
            }

            List<Pessoa> listaPessoas = PessoaService.GetListOptions(Guid.Empty, _Parametros_Busca_Navegacao.id_empresa, false, false, false, false, false, false, false, true, true, true, true, true, false, _Parametros_Busca_Grid).ToList();

            // Retorna na ViewModel
            var viewCombo = new ComboPessoaViewModel()
            {
                listaDadosPessoas = listaPessoas,
                id_pessoa_empresa = new Guid(id_pessoa_empresa),
                id_usuario_logado = new Guid(id),
                visivelCombo = (new Guid(idEmpresaClaim) == IdOrgsystem ? true : false),
                gridInicial = parametros.gridInicial
            };          
            return View(viewCombo);
        }        
    }
}