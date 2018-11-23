using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("grupo")]
    public class Grupo
    {
        public Guid id { get; set; }
        //
        public Guid id_pessoa_empresa { get; set; }
        [ForeignKey("id_pessoa_empresa")]
        public virtual Pessoa Pessoa { get; set; }
        //
        public string nome { get; set; }
        //
        public int id_situacao_cadastral { get; set; }
        [ForeignKey("id_situacao_cadastral")]
        public virtual Situacao_Cadastral Situacao_Cadastral { get; set; }
        //
        public DateTime? data_inclusao { get; set; }
        public string usuario { get; set; }
        public bool excluido { get; set; }
    }
}