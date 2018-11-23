using Microsoft.AspNetCore.Http;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Interfaces.Sistema;
using Plataforma.Services.Interfaces.Sistema;

namespace Plataforma.Services.Services.Sistema
{
    /// <summary>
    /// Service de Log_Erro_AplicacaoService
    /// </summary>
    public class Log_Erro_AplicacaoService : ServiceBase<Log_Erro_Aplicacao>, ILog_Erro_AplicacaoService
    {
        public ILog_Erro_AplicacaoRepository Log_Erro_AplicacaoRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="log_Erro_AplicacaoRepository"></param>
        /// <param name="context"></param>
        public Log_Erro_AplicacaoService(ILog_Erro_AplicacaoRepository log_Erro_AplicacaoRepository, IHttpContextAccessor context) : base(log_Erro_AplicacaoRepository, context)
        {
            Log_Erro_AplicacaoRepository = log_Erro_AplicacaoRepository;
        }
    }
}
