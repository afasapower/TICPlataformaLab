using Plataforma.Domain.Entities.NotMapped;
using System.Collections.Generic;

namespace Plataforma.Ui.OrgTs.ViewComponents
{
    public class BtMenuDetalhes
    {
        public List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenuUtilitarios { get; set; }
        public List<MenuDetalhes> ListaMenuDetalhes { get; set; }
    }

    public class MenuDetalhes
    {
        public string Tipo { get; set; }
        public object Id { get; set; }
        public bool Visivel { get; set; }
        public string Url { get; set; }
        public MenuDetalhesConfirmacao Confirmacao { get; set; }
    }

    public class MenuDetalhesConfirmacao
    {
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public string Tipo { get; set; }
        public string TituloBotaoSim { get; set; }
    }
}