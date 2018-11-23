using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.InfraEstrutura.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plataforma.Ui.OrgTs.ViewModel.Sistema
{
    public class ComputadorViewModel
    {
        public Retorno_Permissao_Grupo_Usuario permissoesMenus { get; set; }
        public List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus { get; set; }

        public Computador computador { get; set; }
        public List<Computador> listaComputador { get; set; }

        public Software software { get; set; }
        public List<Software> listaSoftware { get; set; }

        public Laboratorio laboratorio { get; set; }
        public List<Laboratorio> listaLaboratorio { get; set; }

    }

    public class CadastroComputadorViewModel
    {        
        public Guid id_computador { get; set; }

        [CustomAttributeNoGuidEmpty(ErrorMessage = "Campo Laboratorio é obrigatório")]
        public Guid id_laboratorio { get; set; }

        [CustomAttributeNoGuidEmpty(ErrorMessage = "Campo Software é obrigatório")]
        public Guid[] id_software { get; set; }

        [Required(ErrorMessage = "Campo numero_computador é obrigatório"), MaxLength(3, ErrorMessage = "O campo nome não pode ultrapassar o tamanho de 3 caracteres")]
        public string numero_computador { get; set; }
        public bool fg_ativo { get; set; }
    }
}
