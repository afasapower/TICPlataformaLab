using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;

namespace Plataforma.Repository.Repository.Sistema
{
    public class Log_Erro_AplicacaoRepository : RepositoryBase<Log_Erro_Aplicacao>, ILog_Erro_AplicacaoRepository
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public Log_Erro_AplicacaoRepository(PlataformaContext context) : base(context)
        {
        }
    }
}
