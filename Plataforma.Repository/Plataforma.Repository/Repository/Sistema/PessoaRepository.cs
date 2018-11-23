using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System;
using System.Collections.Generic;
using System.Linq;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Repository.Sistema
{
    public class PessoaRepository : RepositoryBase<Pessoa>, IPessoaRepository
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public PessoaRepository(PlataformaContext context) : base(context)
        {
        }

        /// <summary>
        /// Lista o total de registro da tabela
        /// </summary>
        /// <param name="id_Pessoa"></param>
        /// <param name="id_empresa"></param>
        /// <param name="tipo_pessoa"></param>
        /// <returns></returns>
        public int Count(Guid id_Pessoa, Guid id_empresa, string tipo_pessoa)
        {
            using (var db = new Context.PlataformaContext())
            {
                var lQuery = (from p in db.Pessoa
                              where ((tipo_pessoa == "O") && (p.orgao_administrativo == true))
                               && ((tipo_pessoa == "R") && (p.regional == true))
                               && ((tipo_pessoa == "P") && (p.provincia_eclesial == true))
                               && ((tipo_pessoa == "D") && (p.diocese == true))
                               && ((tipo_pessoa == "T") && (p.tribunal == true))
                               && ((tipo_pessoa == "B") && (p.banco == true))
                               && ((tipo_pessoa == "E") && (p.empresa == true))
                               && ((tipo_pessoa == "PA") && (p.participante == true))
                               && ((tipo_pessoa == "TE") && (p.testemunha == true))
                               && ((tipo_pessoa == "DE") && (p.demandante == true))
                               && ((tipo_pessoa == "DA") && (p.demandado == true))
                               && ((tipo_pessoa == "UR") && (p.user == true))
                               && ((tipo_pessoa == "PR") && (p.paroquia == true))
                               && p.excluido == false
                               && ((id_Pessoa == Guid.Empty) || p.id == id_Pessoa)
                               && ((id_empresa == Guid.Empty) || p.id_pessoa_empresa == id_empresa)
                              select p.id);

                return lQuery.ToList().Count();
            }
        }

        /// <summary>
        /// Listagem de pessoas, sempre passar um tipo: O - Orgão Administrativo / R - Regional / P - Provincia Eclesial / D - Diocese / T - Tribunal / B - Banco/ E - Empresa / PA - PARTICIPANTES / TE - Testemunha / DE - Demandante / DA - Demandado / UR - USER / PR - PAROQUIA
        /// </summary>
        /// <param name="id_Pessoa"></param>
        /// <param name="id_empresa"></param>
        /// <param name="tipo_pessoa"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Pessoa> GetList(Guid id_Pessoa, Guid id_empresa, string tipo_pessoa, Parametros_Busca_Grid parametros_Busca_Grid)
        {
            using (var db = new Context.PlataformaContext())
            {
                var lQuery = (from p in db.Pessoa
                              where p.excluido == false
                               && ((id_Pessoa == Guid.Empty) || (p.id == id_Pessoa))
                               && ((id_empresa == Guid.Empty) || (p.id_pessoa_empresa == id_empresa))
                               && (((tipo_pessoa == "O") && (p.orgao_administrativo == true))
                               || ((tipo_pessoa == "R") && (p.regional == true))
                               || ((tipo_pessoa == "P") && (p.provincia_eclesial == true))
                               || ((tipo_pessoa == "D") && (p.diocese == true))
                               || ((tipo_pessoa == "T") && (p.tribunal == true))
                               || ((tipo_pessoa == "B") && (p.banco == true))
                               || ((tipo_pessoa == "E") && (p.empresa == true))
                               || ((tipo_pessoa == "PA") && (p.participante == true))
                               || ((tipo_pessoa == "TE") && (p.testemunha == true))
                               || ((tipo_pessoa == "DE") && (p.demandante == true))
                               || ((tipo_pessoa == "DA") && (p.demandado == true))
                               || ((tipo_pessoa == "UR") && (p.user == true))
                               || ((tipo_pessoa == "PR") && (p.paroquia == true)))
                              orderby p.id, p.razao_social_nome
                              select new
                              {
                                  id = p.id,
                                  //id_pessoa_empresa = p.id_pessoa_empresa,
                                  razao_social_nome = p.razao_social_nome,
                                  nome_fantasia_apelido = p.nome_fantasia_apelido,
                                  cnpj_cpf = p.cnpj_cpf,
                                  inscricao_estadual_rg = p.inscricao_estadual_rg,
                                  orgao_emissor_rg = p.orgao_emissor_rg,
                                  data_emissao_rg = p.data_emissao_rg,
                                  inscricao_municipal = p.inscricao_municipal,
                                  site = p.site,
                                  data_nascimento_abertura = p.data_nascimento_abertura,
                                  logotipo = p.logotipo,
                                  empresa = p.empresa,
                                  orgao_administrativo = p.orgao_administrativo,
                                  regional = p.regional,
                                  provincia_eclesial = p.provincia_eclesial,
                                  diocese = p.diocese,
                                  tribunal = p.tribunal,
                                  banco = p.banco,
                                  data_inclusao = p.data_inclusao.GetValueOrDefault().ToString("dd/MM/yyyy"),
                                  id_pessoa_empresa = p.id_pessoa_empresa,
                                  usuario = p.usuario
                              }).ToList(); //Skip(page * length).Take(length).ToList().OrderBy(b => b.id);

                IList<Pessoa> pessoa = new List<Pessoa>();

                foreach (var item in lQuery)
                {
                    Pessoa _pessoa = new Pessoa();

                    _pessoa.id = item.id;
                    //_pessoa.id_pessoa_empresa = item.id_pessoa_empresa;
                    _pessoa.razao_social_nome = item.razao_social_nome;
                    _pessoa.nome_fantasia_apelido = item.nome_fantasia_apelido;
                    _pessoa.cnpj_cpf = item.cnpj_cpf;
                    _pessoa.inscricao_estadual_rg = item.inscricao_estadual_rg;
                    _pessoa.orgao_emissor_rg = item.orgao_emissor_rg;
                    _pessoa.data_emissao_rg = item.data_emissao_rg;
                    _pessoa.inscricao_municipal = item.inscricao_municipal;
                    _pessoa.site = item.site;
                    _pessoa.data_nascimento_abertura = item.data_nascimento_abertura;
                    _pessoa.logotipo = item.logotipo;
                    _pessoa.empresa = item.empresa;
                    _pessoa.orgao_administrativo = item.orgao_administrativo;
                    _pessoa.regional = item.regional;
                    _pessoa.provincia_eclesial = item.provincia_eclesial;
                    _pessoa.diocese = item.diocese;
                    _pessoa.tribunal = item.tribunal;
                    _pessoa.banco = item.banco;
                    _pessoa.data_inclusao = DateTime.Parse(item.data_inclusao).Date;
                    _pessoa.usuario = item.usuario;
                    _pessoa.id_pessoa_empresa = item.id_pessoa_empresa;
                    pessoa.Add(_pessoa);
                }
                return pessoa;
            }
        }

        /// <summary>
        /// Listagem de pessos com opção de retornar varios tipo em uma lista
        /// </summary>
        /// <param name="id_Pessoa"></param>
        /// <param name="id_empresa"></param>
        /// <param name="orgao_administrativo"></param>
        /// <param name="regional"></param>
        /// <param name="provincia_eclesial"></param>
        /// <param name="diocese"></param>
        /// <param name="tribunal"></param>
        /// <param name="banco"></param>
        /// <param name="empresa"></param>
        /// <param name="participante"></param>
        /// <param name="testemunha"></param>
        /// <param name="demandante"></param>
        /// <param name="demandado"></param>
        /// <param name="user"></param>
        /// <param name="paroquia"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <returns></returns>
        public IEnumerable<Pessoa> GetListOptions(Guid id_Pessoa,
                                                  Guid id_empresa,
                                                  bool orgao_administrativo = false,
                                                  bool regional = false,
                                                  bool provincia_eclesial = false,
                                                  bool diocese = false,
                                                  bool tribunal = false,
                                                  bool banco = false,
                                                  bool empresa = false,
                                                  bool participante = false,
                                                  bool testemunha = false,
                                                  bool demandante = false,
                                                  bool demandado = false,
                                                  bool user = false,
                                                  bool paroquia = false,
                                                  Parametros_Busca_Grid parametros_Busca_Grid = null)
        {
            using (var db = new Context.PlataformaContext())
            {
                IEnumerable<Pessoa> lQry_Orgao_Administrativo = new List<Pessoa>();
                IEnumerable<Pessoa> lQry_Regional = new List<Pessoa>();
                IEnumerable<Pessoa> lQry_Provincia_Eclesial = new List<Pessoa>();
                IEnumerable<Pessoa> lQry_Diocese = new List<Pessoa>();
                IEnumerable<Pessoa> lQry_Tribunal = new List<Pessoa>();
                IEnumerable<Pessoa> lQry_Banco = new List<Pessoa>();
                IEnumerable<Pessoa> lQry_Empresa = new List<Pessoa>();
                IEnumerable<Pessoa> lQry_Participante = new List<Pessoa>();
                IEnumerable<Pessoa> lQry_Testemunha = new List<Pessoa>();
                IEnumerable<Pessoa> lQry_Demandante = new List<Pessoa>();
                IEnumerable<Pessoa> lQry_Demandado = new List<Pessoa>();
                IEnumerable<Pessoa> lQry_User = new List<Pessoa>();
                IEnumerable<Pessoa> lQry_Paroquia = new List<Pessoa>();

                if (orgao_administrativo)
                {
                    lQry_Orgao_Administrativo = GetList(id_Pessoa, id_empresa, "O", parametros_Busca_Grid);
                }

                if (regional)
                {
                    lQry_Regional = GetList(id_Pessoa, id_empresa, "R", parametros_Busca_Grid);
                }

                if (provincia_eclesial)
                {
                    lQry_Provincia_Eclesial = GetList(id_Pessoa, id_empresa, "P", parametros_Busca_Grid);
                }

                if (diocese)
                {
                    lQry_Diocese = GetList(id_Pessoa, id_empresa, "D", parametros_Busca_Grid);
                }

                if (tribunal)
                {
                    lQry_Tribunal = GetList(id_Pessoa, id_empresa, "T", parametros_Busca_Grid);
                }

                if (banco)
                {
                    lQry_Banco = GetList(id_Pessoa, id_empresa, "B", parametros_Busca_Grid);
                }

                if (empresa)
                {
                    lQry_Empresa = GetList(id_Pessoa, id_empresa, "E", parametros_Busca_Grid);
                }

                if (participante)
                {
                    lQry_Participante = GetList(id_Pessoa, id_empresa, "PA", parametros_Busca_Grid);
                }

                if (testemunha)
                {
                    lQry_Testemunha = GetList(id_Pessoa, id_empresa, "TE", parametros_Busca_Grid);
                }

                if (demandante)
                {
                    lQry_Demandante = GetList(id_Pessoa, id_empresa, "DE", parametros_Busca_Grid);
                }

                if (demandado)
                {
                    lQry_Demandado = GetList(id_Pessoa, id_empresa, "DA", parametros_Busca_Grid);
                }

                if (user)
                {
                    lQry_User = GetList(id_Pessoa, id_empresa, "UR", parametros_Busca_Grid).ToList();
                }

                if (paroquia)
                {
                    lQry_Paroquia = GetList(id_Pessoa, id_empresa, "PR", parametros_Busca_Grid).ToList();
                }

                var lQuerys = lQry_Orgao_Administrativo.Union(lQry_Regional).Union(lQry_Provincia_Eclesial).Union(lQry_Diocese).Union(lQry_Tribunal).Union(lQry_Banco).Union(lQry_Empresa).Union(lQry_Participante).Union(lQry_Testemunha).Union(lQry_Demandante).Union(lQry_Demandado).Union(lQry_User).Union(lQry_Paroquia).Distinct();

                IList<Pessoa> pessoa = new List<Pessoa>();

                foreach (var item in lQuerys)
                {
                    Pessoa _pessoa = new Pessoa();

                    _pessoa.id = item.id;
                    _pessoa.razao_social_nome = item.razao_social_nome;
                    _pessoa.nome_fantasia_apelido = item.nome_fantasia_apelido;
                    _pessoa.cnpj_cpf = item.cnpj_cpf;
                    _pessoa.inscricao_estadual_rg = item.inscricao_estadual_rg;
                    _pessoa.orgao_emissor_rg = item.orgao_emissor_rg;
                    _pessoa.data_emissao_rg = item.data_emissao_rg;
                    _pessoa.inscricao_municipal = item.inscricao_municipal;
                    _pessoa.site = item.site;
                    _pessoa.data_nascimento_abertura = item.data_nascimento_abertura;
                    _pessoa.logotipo = item.logotipo;
                    pessoa.Add(_pessoa);
                }

                var retorno_pessoa = (from _pessoas in pessoa
                                      select new
                                      {
                                          id = _pessoas.id,
                                          id_pessoa_empresa = _pessoas.id_pessoa_empresa,
                                          razao_social_nome = _pessoas.razao_social_nome,
                                          nome_fantasia_apelido = _pessoas.nome_fantasia_apelido,
                                          cnpj_cpf = _pessoas.cnpj_cpf
                                      }).Distinct();

                IList<Pessoa> _retorno_pessoa = new List<Pessoa>();

                foreach (var item in retorno_pessoa)
                {
                    Pessoa _pessoa = new Pessoa();

                    _pessoa.id = item.id;
                    _pessoa.id_pessoa_empresa = item.id_pessoa_empresa;
                    _pessoa.razao_social_nome = item.razao_social_nome;
                    _pessoa.nome_fantasia_apelido = item.nome_fantasia_apelido;
                    _pessoa.cnpj_cpf = item.cnpj_cpf;

                    _retorno_pessoa.Add(_pessoa);
                }

                return _retorno_pessoa;
            }
        }
    }
}