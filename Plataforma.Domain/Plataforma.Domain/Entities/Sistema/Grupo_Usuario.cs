using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("grupo_usuario")]
    public class Grupo_Usuario
    {
        public Guid id { get; set; }
        //
        public Guid id_grupo { get; set; }
        [ForeignKey("id_grupo")]
        public virtual Grupo Grupo { get; set; }
        //
        public Guid id_usuario { get; set; }
        [ForeignKey("id_usuario")]
        public virtual Usuario Usuario { get; set; }
        //
        public DateTime? data_inclusao { get; set; }
        public string usuario { get; set; }
        public bool excluido { get; set; }
    }
}