using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("laboratorio")]
    public class Laboratorio
    {
        [Key]
        public Guid id_laboratorio { get; set; }        
        public string nome { get; set; }
        public string unidade { get; set; }
        public bool fg_ativo { get; set; }
    }
}