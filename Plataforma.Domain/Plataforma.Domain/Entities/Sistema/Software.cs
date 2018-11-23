using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("software")]
    public class Software
    {
        [Key]
        public Guid id_software { get; set; }

        public string fabricante { get; set; }
        public bool open_source { get; set; }
        public string versao_software { get; set; }
        public DateTime? data_aquisicao { get; set; }
        public DateTime? data_vencimento { get; set; }
        public string nome_software { get; set; }
        public bool fg_ativo { get; set; }
    }
}