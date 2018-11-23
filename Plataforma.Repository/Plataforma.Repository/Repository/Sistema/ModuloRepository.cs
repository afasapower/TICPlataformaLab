using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Repository.Sistema
{
    public class ModuloRepository : RepositoryBase<Modulo>, IModuloRepository
    {
        public PlataformaContext Context;//Refazer metodo para parametro

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public ModuloRepository(PlataformaContext context) : base(context)
        {
            Context = context;//Refazer metodo para parametro
        }

        /// <summary>
        /// Lista o Total de Registro na tabela
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Modulo in db.Modulo
                              where _Modulo.excluido == false
                              select _Modulo.id);

                return lQuery.ToList().Count();
            }
        }

        /// <summary>
        /// Lista os Módulos
        /// </summary>
        /// <param name="length"></param>
        /// <param name="search"></param>
        /// <param name="draw"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public IEnumerable<Modulo> GetList(Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Modulo in db.Modulo
                              join _Situacao_Cadastral in db.Situacao_Cadastral on _Modulo.id_situacao_cadastral equals _Situacao_Cadastral.id
                              orderby _Modulo.ordem, _Modulo.id, _Modulo.nome
                              select new
                              {
                                  id = _Modulo.id,
                                  nome = _Modulo.nome,
                                  id_situacao_cadastral = _Modulo.id_situacao_cadastral,
                                  nome_situacao = _Situacao_Cadastral.descricao,
                                  cor = _Modulo.cor,
                                  classe_css = _Modulo.classe_css,
                                  ordem = _Modulo.ordem
                              }).ToList(); //.Skip(page * length).Take(length).ToList();

                IList<Modulo> modulo = new List<Modulo>();

                foreach (var item in lQuery)
                {
                    Modulo _Modulo = new Modulo();

                    _Modulo.id = item.id;
                    _Modulo.nome = item.nome;
                    _Modulo.cor = item.cor;
                    _Modulo.classe_css = item.classe_css;
                    _Modulo.ordem = item.ordem;
                    _Modulo.Situacao_Cadastral = new Situacao_Cadastral()
                    {
                        id = item.id_situacao_cadastral,
                        descricao = item.nome_situacao
                    };

                    modulo.Add(_Modulo);
                }

                return modulo.OrderBy(m => m.nome);
            }
        }

        /// <summary>
        /// Lista os Módulos disponíveis para o usuário
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        public IEnumerable<Modulo> GetList_Modulo_Menu(Guid id_usuario)
        {
            using (var db = new PlataformaContext())
            {
                DateTime data_sistema = DateTime.Now;

                var lQuery = (from _modulo in db.Modulo
                              // Modulos da Empresa
                              join _Modulo_Empresa in db.Modulo_Empresa on _modulo.id equals _Modulo_Empresa.id_modulo
                              // Usuario Empresa Ativo
                              join _Usuario_Empresa_Ativo in db.Usuario_Empresa_Ativo on _Modulo_Empresa.id_pessoa_empresa equals _Usuario_Empresa_Ativo.id_pessoa_empresa
                              // Usuário
                              join _Usuario in db.Usuario on _Usuario_Empresa_Ativo.id_usuario equals _Usuario.id
                              where _Usuario.excluido == false
                              //Condições
                              where _Usuario.id == id_usuario
                              && _Usuario.excluido == false
                              && _modulo.excluido == false
                              && _Modulo_Empresa.excluido == false
                              && _Modulo_Empresa.data_validade > data_sistema

                              // Ordenação
                              orderby _modulo.ordem, _modulo.id, _modulo.nome
                              select new
                              {
                                  id = _modulo.id,
                                  nome = _modulo.nome,
                                  id_situacao_cadastral = _modulo.id_situacao_cadastral,
                                  cor = _modulo.cor,
                                  classe_css = _modulo.classe_css,
                                  ordem = _modulo.ordem,
                                  data_inclusao = _modulo.data_inclusao,
                                  usuario = _modulo.usuario,
                                  excluido = _modulo.excluido

                              }).ToList().Distinct();

                MenuRepository _MenuRepository = new MenuRepository(Context);//Refazer metodo para parametro
                IList<Modulo> _Modulo = new List<Modulo>();

                foreach (var item in lQuery)
                {
                    var Retorno_Menu = _MenuRepository.GetList(item.id, Guid.Empty, id_usuario, null);
                    var Group_Retorno_Menu = (from x in Retorno_Menu select new { x.id_modulo }).Distinct();


                    var Join_Modulo_Menu = (from _lQuery in lQuery
                                            join _Group_Retorno_Menu in Group_Retorno_Menu on _lQuery.id equals _Group_Retorno_Menu.id_modulo
                                            select new
                                            {
                                                id = _lQuery.id,
                                                nome = _lQuery.nome,
                                                id_situacao_cadastral = _lQuery.id_situacao_cadastral,
                                                cor = _lQuery.cor,
                                                classe_css = _lQuery.classe_css,
                                                ordem = _lQuery.ordem,
                                                data_inclusao = _lQuery.data_inclusao,
                                                usuario = _lQuery.usuario,
                                                excluido = _lQuery.excluido

                                            }).ToList().Distinct();

                    foreach (var item1 in Join_Modulo_Menu)
                    {
                        Modulo modulo = new Modulo();

                        modulo.id = item1.id;
                        modulo.nome = item1.nome;
                        modulo.id_situacao_cadastral = item1.id_situacao_cadastral;
                        modulo.cor = item1.cor;
                        modulo.classe_css = item1.classe_css;
                        modulo.ordem = item1.ordem;
                        modulo.data_inclusao = item1.data_inclusao;
                        modulo.usuario = item1.usuario;
                        modulo.excluido = item1.excluido;

                        _Modulo.Add(modulo);
                    }
                }

                return _Modulo;
            }
        }              
    }
}
