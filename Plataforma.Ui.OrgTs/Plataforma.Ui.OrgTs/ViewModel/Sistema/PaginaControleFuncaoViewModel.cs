using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using System.Collections.Generic;

namespace Plataforma.Ui.OrgTs.ViewModel.Sistema
{
    public class PaginaControleFuncaoViewModel
    {
        public Retorno_Permissao_Grupo_Usuario permissoesMenus { get; set; }
        public List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus { get; set; }

        public Pagina pagina{ get; set; }
        public List<Pagina> listapagina { get; set; }

    }
}
