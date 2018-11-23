using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Plataforma.InfraEstrutura.Helpers
{
    [AttributeUsage(AttributeTargets.All)]
    public class Teste: Attribute
    {
        public IServiceCollection ServiceCollection { get; set; }
        public Teste()
        {

        }

       
        public Teste(IServiceCollection services)
        { 
            ServiceCollection = services;
        }
        public void Funcao(HttpRequest request)
        {

           
        }

       
    }
}
