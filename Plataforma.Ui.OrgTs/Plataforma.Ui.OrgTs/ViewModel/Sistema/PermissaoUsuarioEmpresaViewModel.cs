using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using System.Collections.Generic;

namespace Plataforma.Ui.OrgTs.ViewModel.Sistema
{
    public class PermissaoUsuarioEmpresaViewModel
    {
        public Usuario usuario { get; set; }
        public List<Usuario> listausuario { get; set; }

        public Pessoa pessoa { get; set; }
        public List<Pessoa> listapessoa { get; set; }

        public Usuario_Empresa usuarioEmpresa { get; set; }
        public List<Usuario_Empresa> listausuarioEmpresa { get; set; }

        public Usuario_Empresa dadosUsuarioEmpresa { get; set; }
        public List<Dados_Usuario_Empresa> listaDadosUsuarioEmpresa { get; set; }

        public Retorno_Permissao_Grupo_Usuario permissoesMenus { get; set; }
        public List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus { get; set; }
    }
}
