using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System;
using System.Linq;
using System.Collections.Generic;
using Plataforma.Domain.Entities.NotMapped;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Repository.Sistema
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public UsuarioRepository(PlataformaContext context) : base(context)
        {
        }

        /// <summary>
        /// Conta total de registros por empresa
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <returns></returns>
        public int Count(Guid id_empresa)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Usuario in db.Usuario
                              where _Usuario.id_pessoa_empresa == id_empresa
                              && _Usuario.excluido == false
                              select _Usuario.id);

                return lQuery.ToList().Count();
            }
        }

        /// <summary>
        /// Valida se o usuário existe dentro de uma empresa
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="puser"></param>
        /// <returns></returns>
        public Boolean ValidarUsuario(Guid id_empresa, string puser)
        {
            using (var db = new PlataformaContext())
            {
                var achou = false;
                var lQuery = (from _Usuario in db.Usuario
                              where _Usuario.id_pessoa_empresa == id_empresa
                              && _Usuario.login == puser && _Usuario.excluido == false
                              select _Usuario).ToList();

                if (lQuery.Count > 0)
                {
                    achou = true;
                }

                return achou;
            }
        }

        /// <summary>
        /// Autenticação do usuário no banco
        /// </summary>
        /// <param name="puser"></param>
        /// <param name="ppass"></param>
        /// <returns></returns>
        public IEnumerable<Usuario> Autenticar(string puser, string ppass)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Usuario in db.Usuario
                              join _Pessoa in db.Pessoa on _Usuario.id_pessoa equals _Pessoa.id
                              //Não retornar o grupo, pois pode ter mais de um grupo para o usuário
                              //join _Grupo_Usuario in db.Grupo_Usuario on _Usuario.id equals _Grupo_Usuario.id_usuario
                              where _Usuario.login == puser
                              && _Usuario.senha == ppass
                              && _Usuario.excluido == false
                              select new
                              {
                                  _Usuario.id,
                                  _Usuario.id_pessoa_empresa,
                                  _Usuario.id_pessoa,
                                  _Pessoa.razao_social_nome,
                                  _Pessoa.nome_fantasia_apelido,
                                  _Usuario.login,
                                  _Usuario.data_expiracao_senha,
                              }).ToList();

                List<Usuario> Usuario = new List<Usuario>();

                foreach (var item in lQuery)
                {
                    Usuario usuario = new Usuario();

                    usuario.id = item.id;
                    usuario.id_pessoa_empresa = item.id_pessoa_empresa;
                    usuario.id_pessoa = item.id_pessoa;
                    usuario.login = item.login;
                    usuario.data_expiracao_senha = item.data_expiracao_senha;

                    // Retorna dados da empresa do usuário
                    usuario.Pessoa = new Pessoa()
                    {
                        id = item.id_pessoa,
                        razao_social_nome = item.razao_social_nome,
                        nome_fantasia_apelido = item.nome_fantasia_apelido
                    };

                    Usuario.Add(usuario);
                }

                return Usuario;
            }
        }

        /// <summary>
        /// Valida as permissões do Usuário/Grupos nas páginas
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="id_usuario"></param>
        /// <param name="id_pagina"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Retorno_Permissao_Grupo_Usuario> ValidaPermissao(Guid id_empresa, Guid id_usuario, Guid id_pagina, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            // Declaração da lista com as permissções de acesso do usuário
            IList<Retorno_Permissao_Grupo_Usuario> retorno_permissao_grupo_usuario = new List<Retorno_Permissao_Grupo_Usuario>();

            //
            // 1) Valida as permissão dos grupos
            //

            // Lista os Grupos que o Usuario Pertence
            Grupo_UsuarioRepository _Grupo_UsuarioRepository = new Grupo_UsuarioRepository(null);
            var lGrupos_Usuario = _Grupo_UsuarioRepository.GetList(id_usuario, Guid.Empty, parametros_Busca_Grid);

            // Trata as permissões do grupo
            foreach (var _grupos in lGrupos_Usuario)
            {
                // Verifica se tem alguma permissão na lista
                if (retorno_permissao_grupo_usuario.Select(p => p.id_usuario == id_usuario && p.id_pagina == id_pagina).Count() == 0)
                {
                    // Busca as permissões do Grupo na página
                    Permissao_GrupoRepository _Permissao_GrupoRepository = new Permissao_GrupoRepository(null);//Refazer metodo para parametro
                    {
                        var lPermissao_Grupo = _Permissao_GrupoRepository.GetList(id_empresa, _grupos.id_grupo, id_pagina, parametros_Busca_Grid);

                        // Inclui na lista as permissões
                        foreach (var item in lPermissao_Grupo)
                        {
                            retorno_permissao_grupo_usuario.Add(new Retorno_Permissao_Grupo_Usuario()
                            {
                                id_usuario = id_usuario,
                                id_pagina = id_pagina,
                                ler = item.ler,
                                incluir = item.incluir,
                                atualizar = item.atualizar,
                                deletar = item.deletar,
                                upload = item.upload,
                                download = item.download,
                                outros = item.outros
                            });
                        }
                    }
                }
                else
                {
                    // Busca as permissões do Grupo na página
                    Permissao_GrupoRepository _Permissao_GrupoRepository = new Permissao_GrupoRepository(null);
                    {
                        var lPermissao_Grupo = _Permissao_GrupoRepository.GetList(id_empresa, _grupos.id_grupo, id_pagina, parametros_Busca_Grid);

                        // Atualiza a lista Existente
                        foreach (var item in lPermissao_Grupo)
                        {
                            // Realiza comparação das permissões do grupo e mantém as permissões True
                            foreach (var iPermissao in retorno_permissao_grupo_usuario)
                            {
                                if (id_pagina == item.id_pagina)
                                {
                                    iPermissao.ler = (item.ler != false ? item.ler : iPermissao.ler);
                                    iPermissao.incluir = (item.incluir != false ? item.incluir : iPermissao.incluir);
                                    iPermissao.atualizar = (item.atualizar != false ? item.atualizar : iPermissao.atualizar);
                                    iPermissao.deletar = (item.deletar != false ? item.deletar : iPermissao.deletar);
                                    iPermissao.upload = (item.upload != false ? item.upload : iPermissao.upload);
                                    iPermissao.download = (item.download != false ? item.download : iPermissao.download);
                                    iPermissao.outros = (item.outros != false ? item.outros : iPermissao.outros);
                                }
                            }
                        }
                    }
                }
            }

            //
            // 2) Valida as permissão do usuário
            //

            // Busca as permissões do Usuário na página
            Permissao_UsuarioRepository _Permissao_UsuarioRepository = new Permissao_UsuarioRepository(null);
            {
                var lPermissao_Usuario = _Permissao_UsuarioRepository.GetList(id_empresa, id_usuario, id_pagina, parametros_Busca_Grid);

                // Atualiza a lista Existente
                foreach (var item in lPermissao_Usuario)
                {
                    // Realiza comparação das permissões do grupo e mantém as permissões do usuário em True
                    foreach (var iPermissao in retorno_permissao_grupo_usuario)
                    {
                        if (id_pagina == item.id_pagina)
                        {
                            iPermissao.ler = item.ler;
                            iPermissao.incluir = item.incluir;
                            iPermissao.atualizar = item.atualizar;
                            iPermissao.deletar = item.deletar;
                            iPermissao.upload = item.upload;
                            iPermissao.download = item.download;
                            iPermissao.outros = item.outros;
                        }
                    }
                }
            }

            return retorno_permissao_grupo_usuario;
        }

        /// <summary>
        /// Lista todos os usuário de uma empresa
        /// </summary>
        /// <param name="id_pessoa_empresa"></param>
        /// <param name="id_usuario"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Usuario> GetList(Guid id_pessoa_empresa, Guid id_usuario, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Usuario in db.Usuario
                              join _Pessoa in db.Pessoa on _Usuario.id_pessoa equals _Pessoa.id
                              where ((id_usuario == Guid.Empty) || (_Usuario.id == id_usuario))
                              && _Usuario.id_pessoa_empresa == id_pessoa_empresa
                              && _Usuario.excluido == false
                              orderby _Usuario.login
                              select new
                              {
                                  id = _Usuario.id,
                                  id_pessoa_empresa = _Usuario.id_pessoa_empresa,
                                  id_pessoa = _Usuario.id_pessoa,
                                  razao_social_nome = _Pessoa.razao_social_nome,
                                  nome_fantasia_apelido = _Pessoa.nome_fantasia_apelido,
                                  nome = _Pessoa.nome_fantasia_apelido,
                                  login = _Usuario.login,
                                  senha = _Usuario.senha,
                                  data_expiracao_senha = _Usuario.data_expiracao_senha,
                                  data_inclusao = _Usuario.data_inclusao
                              }).ToList(); //.Skip(page * length).Take(length).ToList();

                IList<Usuario> usuario = new List<Usuario>();

                foreach (var item in lQuery)
                {
                    Usuario _Usuario = new Usuario();

                    _Usuario.id = item.id;
                    _Usuario.id_pessoa_empresa = item.id_pessoa_empresa;
                    _Usuario.Pessoa = new Pessoa()
                    {
                        id = item.id_pessoa,
                        razao_social_nome = item.razao_social_nome,
                        nome_fantasia_apelido = item.nome_fantasia_apelido
                    };

                    _Usuario.login = item.login;
                    _Usuario.senha = item.senha;
                    _Usuario.data_expiracao_senha = item.data_expiracao_senha;
                    _Usuario.data_inclusao = item.data_inclusao;

                    usuario.Add(_Usuario);
                }

                return usuario.OrderBy(u => u.login);
            }
        }


        public IEnumerable<Usuario> GetListGrid(Guid id_pessoa_empresa, Guid id_usuario, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Usuario in db.Usuario
                              join _Pessoa in db.Pessoa on _Usuario.id_pessoa equals _Pessoa.id
                              join _Usuario_Empresa in db.Usuario_Empresa on _Usuario.id equals _Usuario_Empresa.id_usuario
                              where _Usuario_Empresa.id_pessoa_empresa == id_pessoa_empresa
                              && ((id_usuario == Guid.Empty) || (_Usuario.id == id_usuario))
                              && _Usuario_Empresa.excluido == false
                              && _Pessoa.excluido == false
                              && _Usuario.excluido == false
                              orderby _Usuario.login
                              select new
                              {
                                  id = _Usuario.id,
                                  id_pessoa_empresa = _Usuario.id_pessoa_empresa,
                                  id_pessoa = _Usuario.id_pessoa,
                                  razao_social_nome = _Pessoa.razao_social_nome,
                                  nome_fantasia_apelido = _Pessoa.nome_fantasia_apelido,
                                  nome = _Pessoa.nome_fantasia_apelido,
                                  login = _Usuario.login,
                                  senha = _Usuario.senha,
                                  data_expiracao_senha = _Usuario.data_expiracao_senha,
                                  data_inclusao = _Usuario.data_inclusao
                              }).ToList();

                IList<Usuario> usuario = new List<Usuario>();

                foreach (var item in lQuery)
                {
                    Usuario _Usuario = new Usuario();

                    _Usuario.id = item.id;
                    _Usuario.id_pessoa_empresa = item.id_pessoa_empresa;
                    _Usuario.Pessoa = new Pessoa()
                    {
                        id = item.id_pessoa,
                        razao_social_nome = item.razao_social_nome,
                        nome_fantasia_apelido = item.nome_fantasia_apelido
                    };

                    _Usuario.login = item.login;
                    _Usuario.senha = item.senha;
                    _Usuario.data_expiracao_senha = item.data_expiracao_senha;
                    _Usuario.data_inclusao = item.data_inclusao;

                    usuario.Add(_Usuario);
                }

                return usuario.OrderBy(u => u.login);
            }
        }
    }
}