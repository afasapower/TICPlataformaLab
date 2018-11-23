using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;

namespace Plataforma.Ui.OrgTs.ViewModel.Sistema
{
    public class LaboratorioViewModel
    {
        public Laboratorio laboratorio { get; set; }
        public List<Laboratorio> listaLaboratorio { get; set; }

        public Retorno_Permissao_Grupo_Usuario permissoesMenus { get; set; }
        public List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus { get; set; }
    }

    public class CadastroLaboratorioViewModel
    {
        public Guid id_laboratorio { get; set; }

        [Required(ErrorMessage = "Campo nome é obrigatório"), MaxLength(40, ErrorMessage = "O campo nome não pode ultrapassar o tamanho de 40 caracteres")]
        public string nome { get; set; }

        [Required(ErrorMessage = "Campo unidade é obrigatório"), MaxLength(3, ErrorMessage = "O campo nome não pode ultrapassar o tamanho de 3 caracteres")]
        public string unidade { get; set; }

        public bool fg_ativo { get; set; }
    }
}
