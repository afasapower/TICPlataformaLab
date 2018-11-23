using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("computador")]
    public class Computador
    {
        [Key]
        public Guid id_computador { get; set; }

        public Guid id_laboratorio { get; set; }
        [ForeignKey("id_laboratorio")]
        public virtual Laboratorio Laboratorio { get; set; }

        public string numero_computador { get; set; }
        public bool fg_ativo { get; set; }
    }
}