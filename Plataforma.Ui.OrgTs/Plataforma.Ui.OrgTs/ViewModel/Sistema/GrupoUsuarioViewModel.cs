using Plataforma.Domain.Entities.Sistema;
using System.Collections.Generic;

namespace Plataforma.Ui.OrgTs.ViewModel.Sistema
{
    public class GrupoUsuarioViewModel
    {
        public Grupo_Usuario grupoUsuario { get; set; }
        public List<Grupo_Usuario> listagrupoUsuario { get; set; }

        public Grupo grupo { get; set; }
        public List<Grupo> listagrupo { get; set; }

        public Usuario usuario { get; set; }
        public List<Usuario> listausuario { get; set; }
    }
}
