using System.Collections.Generic;
using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;

namespace Plataforma.Ui.OrgTs.ViewModel.Sistema
{
    public class MenuViewModel
    {
        public IEnumerable<Situacao_Cadastral> situacao { get; set; }
        public List<Situacao_Cadastral> listaSituacao { get; set; }

        public Modulo modulos { get; set; }
        public List<Modulo> listaModulos { get; set; }

        public Menu menu { get; set; }
        public IEnumerable<Menu> menuLista { get; set; }

        public Menu_Sub menuSub { get; set; }
        public List<Menu_Sub> menuSubLista { get; set; }
        
        public IEnumerable<Pagina> pagina { get; set; }
        public List<Pagina> paginaLista { get; set; }

        public Retorno_Permissao_Grupo_Usuario permissoesMenus { get; set; }
        public List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus { get; set; }

    }
}
