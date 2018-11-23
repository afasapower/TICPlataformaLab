using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma.Domain.Entities.Sistema
{
    [Table("pessoa")]
    public class Pessoa
    {
        public Guid id { get; set; }
        //
        public Guid? id_pessoa_empresa { get; set; }
        [ForeignKey("id_pessoa_empresa")]
        public virtual Pessoa Pessoa_Empresa { get; set; }
        //
        public string razao_social_nome { get; set; }
        public string nome_fantasia_apelido { get; set; }
        public string cnpj_cpf { get; set; }
        public string inscricao_estadual_rg { get; set; }
        public string orgao_emissor_rg { get; set; }
        public DateTime? data_emissao_rg { get; set; }
        public string inscricao_municipal { get; set; }
        public string site { get; set; }
        public DateTime? data_nascimento_abertura { get; set; }
        public string logotipo { get; set; }
        public string caminho_logotipo { get; set; }
        public bool orgao_administrativo { get; set; }
        public bool regional { get; set; }
        public bool provincia_eclesial { get; set; }
        public bool diocese { get; set; }
        public bool tribunal { get; set; }
        public bool banco { get; set; }
        public bool empresa { get; set; }
        public bool participante { get; set; }
        public bool testemunha { get; set; }
        public bool demandante { get; set; }
        public bool demandado { get; set; }
        public bool paroquia { get; set; }
        //
        // usuario dos sistema
        public bool user { get; set; }

        public DateTime? data_inclusao { get; set; }
        public string usuario { get; set; }
        public bool excluido { get; set; }
    }
}
