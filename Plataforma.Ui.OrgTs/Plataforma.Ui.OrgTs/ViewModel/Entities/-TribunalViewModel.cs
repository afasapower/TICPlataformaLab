using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Plataforma.Domain.Entities.NotMapped;
using Plataforma.InfraEstrutura.Helpers;

namespace Plataforma.Ui.OrgTs.ViewModel.Entities
{
    //public class TribunalViewModel
    //{
    //    public class Tribunal
    //    {
    //        public Guid id { get; set; }

    //        public Guid id_pessoa_empresa { get; set; }
    //        public virtual Pessoa Pessoa { get; set; }

    //        public Guid id_pessoa_diocese { get; set; }
    //        public virtual Pessoa Pessoa_Diocese { get; set; }

    //        public int qtd_detestemunha { get; set; }
    //        public int qtd_dia_contestacao { get; set; }
    //        public DateTime? data_inclusao { get; set; }
    //        public string usuario { get; set; }
    //        public bool excluido { get; set; }
    //    }

    //    public Retorno_Permissao_Grupo_Usuario permissoesMenus { get; set; }
    //    public List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus { get; set; }

    //    public Pessoa pessoa { get; set; }
    //    public List<Pessoa> listaPessoa { get; set; }

    //    public Titan.Repository.Domain.Entities.Tribunal tribunal { get; set; }

    //    public Diocese diocese { get; set; }
    //    public List<Diocese> listaDiocese { get; set; }

    //    public List<Provincia_Eclesial> listaProvincia { get; set; }
    //    public Provincia_Eclesial provincia { get; set; }

    //    public Endereco endereco { get; set; }
    //    public List<Endereco> listaEndereco { get; set; }

    //    public List<Tipo_Enderecamento> listaTipoEnderecamento { get; set; }
    //    public List<Estado> listaEstados { get; set; }
    //    public List<Cidade> listaCidades { get; set; }

    //    public Valor_Processo valores { get; set; }

    //}

    //public class TribunalEnderecoView
    //{
    //    [CustomAttributeNoGuidEmpty(ErrorMessage = "Campo cidade é obrigatório")]
    //    //[Required(ErrorMessage = "Campo cidade é obrigatório")]
    //    public Guid? id_cidade { get; set; }

    //    [Required(ErrorMessage = "Campo endereço é obrigatório"), MaxLength(100, ErrorMessage = "O campo endereço não pode ultrapassar o tamanho de 100 caracteres")]
    //    public string endereco { get; set; }

    //    [Required(ErrorMessage = "Campo bairro é obrigatório"), MaxLength(100, ErrorMessage = "O campo bairro não pode ultrapassar o tamanho de 100 caracteres")]
    //    public string bairro { get; set; }

    //    [MaxLength(300, ErrorMessage = "O campo complemento não pode ultrapassar o tamanho de 300 caracteres")]
    //    public string complemento { get; set; }

    //    [Required(ErrorMessage = "Campo cep é obrigatório"), Range(1, 99999999, ErrorMessage = "O campo cep não pode ultrapassar o tamanho de 8 caracteres")]
    //    public int? cep { get; set; }

    //    [MaxLength(300, ErrorMessage = "O campo observação não pode ultrapassar o tamanho de 300 caracteres")]
    //    public string observacao { get; set; }

    //    [Required(ErrorMessage = "Campo tipo de endereçamento é obrigatório")]
    //    public int? id_tipo_enderecamento { get; set; }

    //    public DateTime? data_inclusao { get; set; }
    //    public string usuario { get; set; }
    //    public bool excluido { get; set; }
    //}
}
