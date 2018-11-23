using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Plataforma.Ui.OrgTs.Controllers
{
    /// <summary>
    /// Controller responsavel pelo carregamento de dados comuns a diversas as controllers
    /// </summary>
    public class BaseController : Controller
    {
        public readonly IMapper _mapper;

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="mapper"></param>
        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna as configurações do arquivo de configuração.
        /// </summary>
        /// <returns></returns>
        private IConfigurationRoot Configuracoes()
        {

            var configuracao = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            return configuracao;
        }
    }
}
