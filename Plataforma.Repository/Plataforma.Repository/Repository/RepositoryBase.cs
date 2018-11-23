using Microsoft.EntityFrameworkCore;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Plataforma.Repository.Repository
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected PlataformaContext Db;

        public RepositoryBase(PlataformaContext context)
        {
            Db = context;
        }

        /// <summary>
        /// Método que insere um registro.
        /// </summary>
        /// <param name="obj"></param>
        public TEntity Add(TEntity obj)
        {
            using (var db = new PlataformaContext())
            {
                db.Set<TEntity>().Add(obj);
                db.SaveChanges();
                return obj;
            }
        }

        /// <summary>
        /// Método que busca um objeto pelo seu Id.
        /// </summary>
        /// <param name="id"></param>TEntity
        /// <returns></returns>
        public TEntity GetById(object id)
        {
            using (var db = new PlataformaContext())
            {
                db.Set<TEntity>().AsNoTracking();
                return db.Set<TEntity>().Find(id);
            }
        }

        /// <summary>
        /// Método que retorna uma lista com todos registros de uma tabela.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll()
        {
            using (var db = new PlataformaContext())
            {
                return db.Set<TEntity>().AsNoTracking().ToList();
            }
        }

        /// <summary>
        /// Realiza a atualização de um registro.
        /// </summary>
        /// <param name="obj"></param>
        public void Update(TEntity obj)
        {
            using (var db = new PlataformaContext())
            {
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Remove um registro do DB.
        /// </summary>
        /// <param name="obj"></param>
        public void Remove(TEntity obj)
        {
            using (var db = new PlataformaContext())
            {
                db.Set<TEntity>().Remove(obj);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Remove varios registros
        /// </summary>
        /// <param name="predicate"></param>
        public void Remove(Func<TEntity, bool> predicate)
        {
            using (var db = new PlataformaContext())
            {
                db.Set<TEntity>().Where(predicate).ToList()
                    .ForEach(del => db.Set<TEntity>().Remove(del));
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Retorna uma lista de acordo com o predicado informado.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Db.Set<TEntity>().AsNoTracking().Where(predicate.Compile()).ToList();
        }

    }
}