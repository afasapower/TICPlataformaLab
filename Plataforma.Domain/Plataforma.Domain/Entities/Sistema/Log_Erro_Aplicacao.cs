using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("log_erro_aplicacao")]
    public class Log_Erro_Aplicacao
    {
        public Guid id { get; set; }
        //
        public Guid id_pessoa_empresa { get; set; }
        [ForeignKey("id_pessoa_empresa")]
        public virtual Pessoa Pessoa { get; set; }
        //
        public string origem { get; set; }
        public string mensagem { get; set; }
        public string objeto { get; set; }
        public string metodo { get; set; }
        public DateTime? data_inclusao { get; set; }
        public string usuario { get; set; }
    }
}