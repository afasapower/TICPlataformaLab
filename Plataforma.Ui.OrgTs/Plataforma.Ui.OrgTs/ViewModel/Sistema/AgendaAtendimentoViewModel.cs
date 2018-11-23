using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using System.Collections.Generic;

namespace Plataforma.Ui.OrgTs.ViewModel.Sistema
{
    public class AgendaAtendimentoViewModel
    {
        public Agenda_Atendimento agendaAtendimento { get; set; }
        public List<Agenda_Atendimento> listaAgendaAtendimento { get; set; }

        public Pessoa pessoa { get; set; }
        public List<Pessoa> listaPessoa { get; set; }

        public Retorno_Permissao_Grupo_Usuario permissoesMenus { get; set; }
        public List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus { get; set; }
    }
}
