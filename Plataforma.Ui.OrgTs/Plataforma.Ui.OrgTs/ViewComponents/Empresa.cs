using Microsoft.AspNetCore.Mvc;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using Plataforma.Ui.OrgTs.ViewModel.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Ui.OrgTs.ViewComponents
{
    public class Empresa : ViewComponent
    {

        public IUsuario_EmpresaService Usuario_EmpresaService { get; private set; }
        public IUsuario_Empresa_AtivoService Usuario_Empresa_AtivoService { get; private set; }

        public Empresa(IUsuario_EmpresaService usuario_EmpresaService,
                       IUsuario_Empresa_AtivoService usuario_Empresa_AtivoService)
        {
            Usuario_EmpresaService = usuario_EmpresaService;
            Usuario_Empresa_AtivoService = usuario_Empresa_AtivoService;
        }

        public IViewComponentResult Invoke()
        {
            EmpresaAtivaUsuarioViewModel empresaAtiva = new EmpresaAtivaUsuarioViewModel();
            try
            {
                // Captura ID do usuario logado
                string id = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "id").Value;

                List<Usuario_Empresa_Ativo> listaUsuaioAtivo = Usuario_Empresa_AtivoService.GetList_Empresa_Ativa(new Guid(id), null).ToList();
                empresaAtiva = new EmpresaAtivaUsuarioViewModel()
                {
                    listaUsuarioEmpresa = Usuario_EmpresaService.GetList(new Guid(id), Guid.Empty, new Parametros_Busca_Grid() { length = 100 }).ToList(),
                    usuarioEmpresaAtivo = (listaUsuaioAtivo.Count > 0 ? listaUsuaioAtivo[0] : new Usuario_Empresa_Ativo())
                };
            }
            catch (Exception)
            {
                
            }
            return View(empresaAtiva);
        }
    }
}
