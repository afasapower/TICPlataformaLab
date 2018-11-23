using Microsoft.AspNetCore.Mvc;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Plataforma.Ui.OrgTs.ViewComponents
{
    public class ModalImagemPerfil: ViewComponent
    {      
        public IPessoaService PessoaService { get; private set; }
        public ILog_Erro_AplicacaoService Log_Erro_AplicacaoService { get; private set; }
        public IUsuario_EmpresaService Usuario_EmpresaService { get; private set; }

        public ModalImagemPerfil(IPessoaService pessoaService,
                                ILog_Erro_AplicacaoService log_Erro_AplicacaoService,
                                IUsuario_EmpresaService usuario_EmpresaService)
        {          
            PessoaService = pessoaService;
            Log_Erro_AplicacaoService = log_Erro_AplicacaoService;
            Usuario_EmpresaService = usuario_EmpresaService;
        }

        public IViewComponentResult Invoke()
        {
            // Captura ID do usuario logado
            string id = ((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "id").Value;
            //usuario_empresa_dal usuarioempresa = new usuario_empresa_dal();

            List<Usuario_Empresa> selectusuarioempresa = Usuario_EmpresaService.GetList(new Guid(id), Guid.Empty, null).ToList();

            Usuario_Empresa usuario_Empresa = (selectusuarioempresa.Count > 0 ? selectusuarioempresa[0] : new Usuario_Empresa());
                                    
            Pessoa pessoa = PessoaService.GetById(usuario_Empresa.id_pessoa_empresa);

            return View(pessoa);
        }
    }
}