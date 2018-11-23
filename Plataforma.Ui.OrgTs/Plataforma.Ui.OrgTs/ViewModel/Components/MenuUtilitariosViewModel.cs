using Plataforma.Domain.Entities.NotMapped;
using System.Collections.Generic;

namespace Plataforma.Ui.OrgTs.ViewModel.Components
{
    public class MenuUtilitariosViewModel
    {
        public string[,] menuUtilitario { get; set; }
        public Retorno_Permissao_Grupo_Usuario permissoesMenuUtilitarios { get; set; }
        public List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenuUtilitarios { get; set; }
    }
}
