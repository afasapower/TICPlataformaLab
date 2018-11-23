using System.Collections.Generic;

namespace Plataforma.Ui.OrgTs.ViewModel
{
    public class DataTablesParametrosViewModel
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }

        public BuscaColunas search { get; set; }
        public List<OrdenaColunas> order { get; set; }
        public List<ColunasDataTables> columns { get; set; }        
    }

    public enum OrdenacaoColunas
    {
        asc, desc
    }

    public class OrdenaColunas
    {
        public int column { get; set; }
        public OrdenacaoColunas dir { get; set; }
    }
    public class BuscaColunas
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class ColunasDataTables
    {
        public string data { get; set; }
        public string name { get; set; }
        //public bool searchable { get; set; }
        //public bool orderable { get; set; }
       // public BuscaColunas search { get; set; }
    }
}
