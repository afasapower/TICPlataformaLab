using Plataforma.Domain.Entities.Sistema;
using System.Collections.Generic;

namespace Plataforma.Ui.OrgTs.ViewModel
{
    public class LayoutViewModel
    {
        public List<Modulo> menuModulos { get; set; }
        public List<Menu> menuAcao { get; set; }
        public List<Menu_Sub> menuSub { get; set; }
    }
}
