using Microsoft.AspNetCore.Mvc;
using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Services.Interfaces.Sistema;
using Plataforma.Ui.OrgTs.ViewModel.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Ui.OrgTs.ViewComponents
{
    public class ComboEmpresas : ViewComponent
    {
        public IModuloService ModuloService { get; private set; }
        public IMenuService MenuService { get; private set; }
        public IMenu_SubService Menu_SubService { get; private set; }
        public IUsuario_EmpresaService Usuario_EmpresaService { get; private set; }
        public IPessoaService PessoaService { get; private set; }
        public Guid IdOrgsystem { get; set; }

        public ComboEmpresas(IModuloService moduloService,
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

        public IViewComponentResult Invoke(ComboEmpresaViewModel paramentros)
        {
            // Captura ID do usuario logado
            string id = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "id").Value;
            string idEmpresaClaim = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "id_pessoa_empresa").Value;
            string id_pessoa_empresa = string.Empty;

            if (paramentros.idPessoaEmpresa == Guid.Empty)
            {
                id_pessoa_empresa = paramentros.idPessoaEmpresa.ToString();

                if(new Guid(idEmpresaClaim) == IdOrgsystem)
                {
                    id_pessoa_empresa = IdOrgsystem.ToString();
                }
            }else{
                id_pessoa_empresa = paramentros.idPessoaEmpresa.ToString();
            }

            Configuracao_Sistema _Configuracao_Sistema = new Configuracao_Sistema()
            {
                id_empresa_unifacef = Startup.id_empresa_unifacef
            };

            List<Dados_Usuario_Empresa> listaUsuarioEmpresa = Usuario_EmpresaService.GetListDadosUsuario(PessoaService, Usuario_EmpresaService, new Guid(id), new Guid(id_pessoa_empresa), new Parametros_Busca_Grid() { length = 10000 }, _Configuracao_Sistema);
            
            // Retorna na ViewModel
            var viewCombo = new ComboEmpresaViewModel()
            {
                listaDadosUsuarioEmpresa = listaUsuarioEmpresa,
                id_pessoa_empresa = new Guid(id_pessoa_empresa),
                id_usuario_logado = new Guid(id),
                visivelCombo = (new Guid(idEmpresaClaim) == IdOrgsystem ? true : false),
                gridInicial = paramentros.gridInicial
            };          
            return View(viewCombo);
        }
    }
}
