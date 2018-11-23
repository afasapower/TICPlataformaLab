using System.Collections.Generic;
using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;

namespace Plataforma.Ui.OrgTs.ViewModel.Sistema
{
    public class GrupoViewModel
    {
        public Grupo grupo { get; set; }
        public IEnumerable<Situacao_Cadastral> situacao { get; set; }

        public Modulo modulos { get; set; }
        public List<Modulo> listaModulos { get; set; }

        public Menu menu { get; set; }
        public List<Menu> listaMenu { get; set; }

        public Menu_Sub menuSub { get; set; }
        public List<Menu_Sub> listaMenuSub { get; set; }

        public Permissao_Grupo permissaoGrupo { get; set; }
        public List<Permissao_Grupo> listaPermissaoGrupo { get; set; }

        public Retorno_Permissao_Grupo_Usuario permissoesMenus { get; set; }
        public List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus { get; set; }

        public Retorno_Permissao_Grupo_Pagina permissaoGrupoPaginas { get; set; }
        public List<Retorno_Permissao_Grupo_Pagina> listaPermissaoGrupoPaginas { get; set; }

        public Permissao_Grupo_Etapa permissaoGrupoEtapa { get; set; }
        public List<Permissao_Grupo_Etapa> listaPermissaoGrupoEtapa { get; set; }
    }

}
