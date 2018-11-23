using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("usuario")]
    public class Usuario
    {
        public Guid id { get; set; }
        //
        public Guid id_pessoa_empresa { get; set; }
        [ForeignKey("id_pessoa_empresa")]
        public virtual Pessoa Empresa { get; set; }
        //
        public Guid id_pessoa { get; set; }
        [ForeignKey("id_pessoa")]
        public virtual Pessoa Pessoa { get; set; }
        //
        public string login { get; set; }        
        public string senha { get; set; }
        public DateTime data_expiracao_senha { get; set; }       
        //public string confirmar_senha { get; set; }
        public bool acesso_restrito { get; set; }        
        public DateTime data_inclusao { get; set; }
        public string usuario { get; set; }
        public bool excluido { get; set; }
    }
}
