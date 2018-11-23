using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("usuario_empresa")]
    public class Usuario_Empresa
    {
        public Guid id { get; set; }
        //
        public Guid id_usuario { get; set; }
        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }
        //
        public Guid id_pessoa_empresa { get; set; }
        [ForeignKey("id_pessoa_empresa")]
        public virtual Pessoa Pessoa_Empresa { get; set; }
        //
        public DateTime? data_inclusao { get; set; }
        public string usuario { get; set; }
        public bool excluido { get; set; }
    }
}