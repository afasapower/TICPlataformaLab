using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Interfaces.Sistema;
using Plataforma.Services.Interfaces.Sistema;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Services.Services.Sistema
{
    /// <summary>
    /// Service de PessoaService
    /// </summary>
    public class PessoaService : ServiceBase<Pessoa>, IPessoaService
    {
        public IPessoaRepository PessoaRepository { get; private set; }

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="pessoaRepository"></param>
        /// <param name="context"></param>
        public PessoaService(IPessoaRepository pessoaRepository, IHttpContextAccessor context) : base(pessoaRepository, context)
        {
            PessoaRepository = pessoaRepository;
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
            return PessoaRepository.Count(id_Pessoa, id_empresa, tipo_pessoa);
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
            return PessoaRepository.GetList(id_Pessoa, id_empresa, tipo_pessoa, parametros_Busca_Grid);
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
            return PessoaRepository.GetListOptions(id_Pessoa,
                                                   id_empresa,
                                                   orgao_administrativo,
                                                   regional,
                                                   provincia_eclesial,
                                                   diocese,
                                                   tribunal,
                                                   banco,
                                                   empresa,
                                                   participante,
                                                   testemunha,
                                                   demandante,
                                                   demandado,
                                                   user,
                                                   paroquia,
                                                   parametros_Busca_Grid);
        }
    }
}