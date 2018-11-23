using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("menu")]
    public class Menu
    {
        public Guid id { get; set; }
        //
        public Guid id_modulo { get; set; }
        [ForeignKey("id_modulo")]
        public virtual Modulo Modulo { get; set; }
        //
        public int id_situacao_cadastral { get; set; }
        [ForeignKey("id_situacao_cadastral")]
        public virtual Situacao_Cadastral Situacao_Cadastral { get; set; }
        //
        public string nome { get; set; }
        public string descricao { get; set; }
        public string nome_imagem { get; set; }
        public int? ordem { get; set; }
        public DateTime? data_inclusao { get; set; }
        public string usuario { get; set; }
        public bool excluido { get; set; }
    }
}