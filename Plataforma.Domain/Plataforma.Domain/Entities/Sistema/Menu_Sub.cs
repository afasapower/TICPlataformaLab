using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("menu_sub")]
    public class Menu_Sub
    {
        public Guid id { get; set; }
        //
        public Guid id_menu { get; set; }
        [ForeignKey("id_menu")]
        public virtual Menu Menu { get; set; }
        //
        public int id_situacao_cadastral { get; set; }
        [ForeignKey("id_situacao_cadastral")]
        public virtual Situacao_Cadastral Situacao_Cadastral { get; set; }
        //
        public Guid? id_pagina { get; set; }
        [ForeignKey("id_pagina")]
        public virtual Pagina Pagina { get; set; }
        //
        public string nome { get; set; }
        public string descricao { get; set; }
        public string url { get; set; }
        public Guid? parent { get; set; }
        public string nome_imagem { get; set; }
        public int? ordem { get; set; }
        public string tipo_pagina { get; set; }
        public int numero_grid { get; set; }
        public DateTime? data_inclusao { get; set; }
        public string usuario { get; set; }
        public bool excluido { get; set; }
    }
}