using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("agenda_atendimento")]
    public class Agenda_Atendimento
    {
        public Guid id { get; set; }
        public string protocolo { get; set; }
        //
        public Guid? id_pessoa_empresa { get; set; }
        [ForeignKey("id_pessoa_empresa")]
        public virtual Pessoa Pessoa_Empresa { get; set; }
        //
        public string nome_solicitante { get; set; }
        public DateTime? data { get; set; }
        public string hora { get; set; }
        public string observacao { get; set; }
        //
        public Guid? id_pessoa_responsavel { get; set; }
        [ForeignKey("id_pessoa_responsavel")]
        public virtual Pessoa Pessoa_Responsavel { get; set; }
        //
        public DateTime? data_inclusao { get; set; }
        public string usuario { get; set; }
        public bool excluido { get; set; }
    }
}