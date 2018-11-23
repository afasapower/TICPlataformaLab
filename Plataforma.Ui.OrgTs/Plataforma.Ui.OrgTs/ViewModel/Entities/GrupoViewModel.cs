using System;
using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;

namespace Plataforma.Ui.OrgTs.ViewModel.Entities
{
    public class GrupoViewModel
    {
        public Guid id { get; set; }
        public Guid id_pessoa_empresa { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        // [Required]
        public string nome { get; set; }

        public int id_situacao_cadastral { get; set; }
        public virtual Situacao_Cadastral Situacao_Cadastral { get; set; }
  
        public DateTime? data_inclusao { get; set; }
        public string usuario { get; set; }
        public bool excluido { get; set; }
    }
}