using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;

namespace Plataforma.Ui.OrgTs.ViewModel.Sistema
{
    public class SoftwareViewModel
    {
        public Software software { get; set; }
        public List<Software> listaSoftware { get; set; }

        public Retorno_Permissao_Grupo_Usuario permissoesMenus { get; set; }
        public List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus { get; set; }
    }

    public class CadastroSoftwareViewModel
    {
        public Guid id_software { get; set; }

        [Required(ErrorMessage = "Campo fabricante é obrigatório"), MaxLength(40, ErrorMessage = "O campo fabricante não pode ultrapassar o tamanho de 40 caracteres")]
        public string fabricante { get; set; }

        public bool open_source { get; set; }

        [Required(ErrorMessage = "Campo versao_software é obrigatório"), MaxLength(40, ErrorMessage = "O campo versao_software não pode ultrapassar o tamanho de 40 caracteres")]
        public string versao_software { get; set; }

        [Required(ErrorMessage = "Campo data_aquisicao é obrigatório"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}"), DataType(DataType.DateTime, ErrorMessage = "Data em formato inválido")]
        public DateTime? data_aquisicao { get; set; }

        [Required(ErrorMessage = "Campo data_vencimento é obrigatório"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}"), DataType(DataType.DateTime, ErrorMessage = "Data em formato inválido")]
        public DateTime? data_vencimento { get; set; }

        [Required(ErrorMessage = "Campo nome_software é obrigatório"), MaxLength(40, ErrorMessage = "O campo nome_software não pode ultrapassar o tamanho de 40 caracteres")]
        public string nome_software { get; set; }

        public bool fg_ativo { get; set; }
    }
}
