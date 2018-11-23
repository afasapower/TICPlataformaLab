using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("modulo")]
    public class Modulo
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        //
        public int id_situacao_cadastral { get; set; }
        [ForeignKey("id_situacao_cadastral")]
        public virtual Situacao_Cadastral Situacao_Cadastral { get; set; }
        //
        public string cor { get; set; }
        public string classe_css { get; set; }
        public int ordem { get; set; }
        public DateTime? data_inclusao { get; set; }
        public string usuario { get; set; }
        public bool excluido { get; set; }
    }
}