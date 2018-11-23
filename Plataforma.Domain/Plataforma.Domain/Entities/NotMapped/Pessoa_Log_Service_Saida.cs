using System;

namespace Plataforma.Domain.Entities.NotMapped
{
    public class Pessoa_Log_Service_Saida
    {
        public Guid id_pessoa { get; set; }
        public string nome_servidor { get; set; }
        public string endereco { get; set; }
        public string bairro { get; set; }
        public string complemento { get; set; }
        public int cep { get; set; }
        public string nome_cidade { get; set; }
        public string nome_estado { get; set; }
        public string sigla { get; set; }
        public DateTime? data_inclusao { get; set; }
        public string uso_cpu { get; set; }
        public string uso_memoria { get; set; }
        public bool situacao_banco { get; set; }
        public string situacao_servidor { get; set; }
    }

    /// <summary>
    /// Entidade não mapeada, Retorno com as informações de Consumo e total de usuário logados no servidor
    /// </summary>
    public class Consumo_Servidor_Saida
    {
        public int horas { get; set; }
        public int total_usuarios_logados { get; set; }
        public decimal uso_cpu { get; set; }
        public decimal uso_memoria { get; set; }
    }

    /// <summary>
    /// Entidade não mapeada, Retorno com as informações de Disponibilidade do Servidor
    /// </summary>
    public class Disponibilidade_Servidor_Saida
    {
        public Guid id_pessoa { get; set; }
        public string razao_social_nome { get; set; }
        public string situacao_banco { get; set; }
        public int total_conexao { get; set; }
    }
}