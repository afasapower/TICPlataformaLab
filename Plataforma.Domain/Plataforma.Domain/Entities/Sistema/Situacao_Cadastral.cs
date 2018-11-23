using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("situacao_cadastral")]
    public class Situacao_Cadastral
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public DateTime? data_inclusao { get; set; }
        public string usuario { get; set; }
        public bool excluido { get; set; }
    }
}