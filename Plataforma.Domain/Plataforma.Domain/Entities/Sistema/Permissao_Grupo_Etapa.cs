using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("permissao_grupo_etapa")]
    public class Permissao_Grupo_Etapa
    {
        public Guid id { get; set; }
        //
        public Guid id_grupo { get; set; }
        [ForeignKey("id_grupo")]
        public virtual Grupo Grupo { get; set; }

        public bool upload { get; set; }
        public bool download { get; set; }
        public bool outros { get; set; }
        public DateTime? data_inclusao { get; set; }
        public string usuario { get; set; }
        public bool excluido { get; set; }
    }
}
