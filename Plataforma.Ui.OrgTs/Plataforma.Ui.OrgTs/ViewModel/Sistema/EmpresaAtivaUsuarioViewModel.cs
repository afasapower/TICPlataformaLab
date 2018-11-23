using Plataforma.Domain.Entities.Sistema;
using System.Collections.Generic;

namespace Plataforma.Ui.OrgTs.ViewModel.Sistema
{
    public class EmpresaAtivaUsuarioViewModel
    {
        public List<Usuario_Empresa> listaUsuarioEmpresa { get; set; }
        public Usuario_Empresa usuarioEmpresa { get; set; }

        public Usuario_Empresa_Ativo usuarioEmpresaAtivo { get; set; }
        public List<Usuario_Empresa_Ativo> listaUsuarioEmpresaAtivo { get; set; }
    }
}
