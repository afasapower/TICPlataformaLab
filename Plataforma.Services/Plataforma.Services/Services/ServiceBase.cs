using Plataforma.Services.Interfaces;
using System;
using System.Collections.Generic;
using Plataforma.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using Plataforma.InfraEstrutura.Helpers;
using System.Linq;
using System.Security.Claims;

namespace Plataforma.Services
{
    /// <summary>
    /// Classe que implementa os serviços genéricos.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class ServiceBase<TEntity> : IServiceBase<TEntity> where TEntity : class
    {
        protected IRepositoryBase<TEntity> RepositoryBase;
        protected IHttpContextAccessor Context { get; set; }

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="repositoryBase"></param>
        public ServiceBase(IRepositoryBase<TEntity> repositoryBase, IHttpContextAccessor context)
        {
            RepositoryBase = repositoryBase;
            Context = context;
        }

        /// <summary>
        /// Retorna os dados do arquivo de configuracao.
        /// </summary>
        /// <returns></returns>
        protected IConfigurationRoot Configuracao()
        {
            var configuracao = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();

            return configuracao;
        }

        /// <summary>
        /// Adiciona um novo registro.
        /// </summary>
        /// <param name="obj"></param>
        public void Add(TEntity obj)
        {
            //var add = Atribuicoes.AtribuirIdEntidade(obj, GetId_EmpresaUsuarioLogado());
            //RepositoryBase.Add(Atribuicoes.AtribuirUsuarioInclusaoId(add, GetIdUsuarioLogado()));

            RepositoryBase.Add(obj);
        }

        /// <summary>
        /// Busca todos os registros.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
            return RepositoryBase.GetAll();
        }

        /// <summary>
        /// Retorna um registro específico.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(object id)
        {
            return RepositoryBase.GetById(id);
        }

        /// <summary>
        /// Remove um registro.
        /// </summary>
        /// <param name="obj"></param>
        public void Remove(TEntity obj)
        {
            RepositoryBase.Remove(obj);
        }

        /// <summary>
        /// Remove varios registros
        /// </summary>
        /// <param name="predicate"></param>
        public void Remove(Func<TEntity, bool> predicate)
        {
            RepositoryBase.Remove(predicate);
        }

        /// <summary>
        /// Atualiza um registro.
        /// </summary>
        /// <param name="obj"></param>
        public void Update(TEntity obj)
        {
            //var atualizar = Atribuicoes.AtribuirId(obj, "Id", id);
            //atualizar = Atribuicoes.AtribuirUsuarioInclusaoId(obj, GetIdUsuarioLogado());

            RepositoryBase.Update(obj);
        }

        /// <summary>
        /// Remove logicamente um registro.
        /// </summary>
        /// <param name="obj"></param>
        public void RemoverLogico(TEntity obj)
        {
            var atualizar = Atribuicoes.AtribuirBoolExcluido(obj, "Excluido", true);
            RepositoryBase.Update(atualizar);
        }

        /// <summary>
        /// Retorna o id do usuario logado
        /// </summary>
        /// <returns></returns>
        public string GetIdUsuarioLogado()
        {
            var contador = Context.HttpContext.User.Claims.Count();

            if (contador > 0)
            {

                return Context.HttpContext.User.Claims.Where<Claim>(c => c.Type == ClaimTypes.Sid).FirstOrDefault().Value;
            }

            return null;
        }

        /// <summary>
        /// Retorna o id_empresa do usuario logado.
        /// </summary>
        /// <returns></returns>
        public string GetId_EmpresaUsuarioLogado()
        {
            var contador = Context.HttpContext.User.Claims.Count();

            if (contador > 0)
            {
                var id_empresa = Context.HttpContext.User.Claims.Where<Claim>(c => c.Type == "id_empresa").FirstOrDefault().Value;
                return (id_empresa == "") ? null : id_empresa;
            }

            return null;
        }
    }
}