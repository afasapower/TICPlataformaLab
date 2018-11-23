using System;

namespace Plataforma.Domain.Entities.NotMapped
{
    public class Fu_Valida_Usuario
    {
        public Guid id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public Guid id_grupo { get; set; }
        public string nome_grupo { get; set; }
        public Guid id_pessoa_empresa { get; set; }
        public string nome_fantasia_apelido { get; set; }
        public string cnpj_cpf { get; set; }
    }
}