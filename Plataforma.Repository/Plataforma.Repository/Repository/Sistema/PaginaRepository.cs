using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System.Linq;
using System;
using System.Collections.Generic;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Repository.Sistema
{
    public class PaginaRepository : RepositoryBase<Pagina>, IPaginaRepository
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public PaginaRepository(PlataformaContext context) : base(context)
        {
        }

        /// <summary>
        /// Retorna o total de registro não excluidos
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Pagina in db.Pagina
                              where _Pagina.excluido == false
                              select _Pagina.id);

                return lQuery.ToList().Count();
            }
        }

        /// <summary>
        /// Retorna uma lista de todos as Paginas que não estão excluidas
        /// </summary>
        /// <param name="id_pagina"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Pagina> GetList(Guid id_pagina, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Pagina in db.Pagina
                              join _Situacao_Cadastral in db.Situacao_Cadastral on _Pagina.id_situacao_cadastral equals _Situacao_Cadastral.id
                              where ((id_pagina == Guid.Empty) || (_Pagina.id == id_pagina))
                              && _Pagina.excluido == false
                              orderby _Pagina.nome
                              select new
                              {
                                  id = _Pagina.id,
                                  nome = _Pagina.nome,
                                  id_situacao_cadastral = _Pagina.id_situacao_cadastral,
                                  nome_situacao = _Situacao_Cadastral.descricao
                              }).ToList(); //.Skip(page * length).Take(length).ToList();

                IList<Pagina> pagina = new List<Pagina>();

                foreach (var item in lQuery)
                {
                    Pagina _Pagina = new Pagina();

                    _Pagina.id = item.id;
                    _Pagina.nome = item.nome;
                    _Pagina.Situacao_Cadastral = new Situacao_Cadastral()
                    {
                        id = item.id_situacao_cadastral,
                        descricao = item.nome_situacao
                    };

                    pagina.Add(_Pagina);
                }

                return pagina.OrderBy(p => p.nome);
            }
        }
    }
}