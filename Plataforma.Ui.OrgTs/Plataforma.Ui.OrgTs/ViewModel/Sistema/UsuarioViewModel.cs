using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.InfraEstrutura.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Plataforma.Ui.OrgTs.ViewModel.Sistema
{
    public class UsuarioViewModel
    {
        public Pessoa pessoa { get; set; }
        public List<Pessoa> listapessoa { get; set; }

        public Usuario usuario { get; set; }
        public List<Usuario> listausuario { get; set; }

        public Grupo grupo { get; set; }
        public List<Grupo> listagrupo { get; set; }

        public Grupo_Usuario grupo_usuario { get; set; }
        public List<Grupo_Usuario> listagrupo_usuario { get; set; }
                
        public Retorno_Permissao_Grupo_Usuario permissoesMenus { get; set; }
        public List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus { get; set; }

        public Usuario_Empresa usuarioEmpresa { get; set; }
        public List<Usuario_Empresa> listaUsuarioEmpresa { get; set; }

        public Usuario_Empresa dadosUsuarioEmpresa { get; set; }
        public List<Dados_Usuario_Empresa> listaDadosUsuarioEmpresa { get; set; }        

        [Required(ErrorMessage = "Campo razão social / nome é obrigatório"), MaxLength(100, ErrorMessage = "O campo razão social / nome não pode ultrapassar o tamanho de 100 caracteres")]
        public string razao_social_nome { get; set; }

        [Required(ErrorMessage = "Campo login é obrigatório"), MaxLength(100, ErrorMessage = "O campo login não pode ultrapassar o tamanho de 100 caracteres")]
        public string login { get; set; }

        [Required(ErrorMessage = "Campo e-mail é obrigatório"), MaxLength(100, ErrorMessage = "O campo e-mail não pode ultrapassar o tamanho de 100 caracteres")]
        public string email { get; set; }

        [Required(ErrorMessage = "Data de nascimento obrigatória")]
        public DateTime? data_nascimento_abertura { get; set; }

        [CustomAttributeNoGuidEmpty(ErrorMessage = "Selecione um grupo para este usuário")]
        public Guid id_grupo { get; set; }

        [Required(ErrorMessage = "Tipo de endereçamento obrigatório")]
        public int id_tipo_email { get; set; }

        public bool acesso_restrito { get; set; }

    }

    public class CadastroUsuarioAppViewModel
    {
        public Guid id_usuario { get; set; }

        public Guid id_pessoa_empresa { get; set; }

        [Required(ErrorMessage = "Campo empresa inicial é obrigatório"), Range(1, 9999999, ErrorMessage = "O campo empresa inicial não pode ultrapassar o tamanho de 7 caracteres")]
        public int? empresa_inicial { get; set; }

        [Required(ErrorMessage = "Campo empresa final é obrigatório"), Range(1, 9999999, ErrorMessage = "O campo empresa final não pode ultrapassar o tamanho de 7 caracteres")]
        public int? empresa_final { get; set; }

        [Required(ErrorMessage = "Campo usuário sig é obrigatório"), MaxLength(100, ErrorMessage = "O campo usuário sig não pode ultrapassar o tamanho de 100 caracteres")]
        public string usuario_sig { get; set; }

        public bool usuario_sig_admin { get; set; }

        [Required(ErrorMessage = "Campo usuário arrecadador é obrigatório"), MaxLength(100, ErrorMessage = "O campo usuário arrecadador não pode ultrapassar o tamanho de 100 caracteres")]
        public string usuario_arrecadador { get; set; }

        public bool exibir_caixa_extrato { get; set; }
        public bool exibir_saldo_conta { get; set; }
        public bool exibir_relatorio_uso_sig { get; set; }
        public bool exibir_dizimo { get; set; }
        public bool exibir_periodo_contabil { get; set; }

        public DateTime? data_inclusao { get; set; }
        public string usuario { get; set; }
        public bool excluido { get; set; }
    }
}
