using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("permissao_usuario")]
    public class Permissao_Usuario
    {
        public Guid id { get; set; }
        //
        public Guid id_usuario { get; set; }
        [ForeignKey("id_usuario")]
        public virtual Usuario Usuarios { get; set; }
        //
        public Guid id_pagina { get; set; }
        [ForeignKey("id_pagina")]
        public virtual Pagina Pagina { get; set; }
        //
        public bool ler { get; set; }
        public bool incluir { get; set; }
        public bool atualizar { get; set; }
        public bool deletar { get; set; }
        public bool upload { get; set; }
        public bool download { get; set; }
        public bool outros { get; set; }
        public DateTime? data_inclusao { get; set; }
        public string usuario { get; set; }
        public bool excluido { get; set; }
    }
}