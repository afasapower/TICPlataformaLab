using Plataforma.Domain.Entities.Sistema;
using Plataforma.Repository.Context;
using Plataforma.Repository.Interfaces.Sistema;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.Repository.Repository.Sistema
{
    public class Agenda_AtendimentoRepository : RepositoryBase<Agenda_Atendimento>, IAgenda_AtendimentoRepository
    {
        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="context"></param>
        public Agenda_AtendimentoRepository(PlataformaContext context) : base(context)
        {
        }

        /// <summary>
        /// Retorna o total de registro não excluidos de acordo com os parametros
        /// </summary>
        /// <param name="paramentros_Pessoa"></param>
        /// <param name="datainicial"></param>
        /// <param name="datafinal"></param>
        /// <returns></returns>
        public int Count(Parametros_Pessoa paramentros_Pessoa, string datainicial = "", string datafinal = "")
        {
            DateTime thisDay = DateTime.Today;
            var ldata_sistema = thisDay.ToString();

            if (datainicial == "" || datafinal == "")
            {
                datainicial = ldata_sistema;
                datafinal = ldata_sistema;
            }

            using (var db = new PlataformaContext())
            {
                var lQuery = (from _Agenda_Atendimento in db.Agenda_Atendimento.AsNoTracking()
                              select _Agenda_Atendimento.id);

                return lQuery.ToList().Count();
            }
        }

        /// <summary>
        /// Retorna uma lista de todos os Agendamentos que não estão excluidas de acordo com os parametros
        /// </summary>
        /// <param name="paramentros_Pessoa"></param>
        /// <param name="parametros_Busca_Grid"></param>
        /// <param name="datainicial"></param>
        /// <param name="datafinal"></param>
        /// <returns></returns>
        public IEnumerable<Agenda_Atendimento> GetList(Parametros_Pessoa paramentros_Pessoa, Parametros_Busca_Grid parametros_Busca_Grid, string datainicial = "", string datafinal = "")

        {
            using (var db = new PlataformaContext())
            {
                DateTime thisDay = DateTime.Today;
                var ldata_sistema = thisDay.ToString("d");

                if (datainicial == "" || datafinal == "")
                {
                    datainicial = ldata_sistema;
                    datafinal = ldata_sistema;
                }

                var lQuery = (from _Agenda_Atendimento in db.Agenda_Atendimento
                              // Pessoa Responsavel
                              join _Pessoa in db.Pessoa on _Agenda_Atendimento.id_pessoa_responsavel equals _Pessoa.id                              
                              where
                              (_Agenda_Atendimento.data >= Convert.ToDateTime(datainicial) && _Agenda_Atendimento.data <= Convert.ToDateTime(datafinal))
                              && _Agenda_Atendimento.excluido == false
                              orderby _Agenda_Atendimento.data, _Agenda_Atendimento.hora
                              select new
                              {
                                  id = _Agenda_Atendimento.id,
                                  protocolo = _Agenda_Atendimento.protocolo,
                                  id_pessoa_empresa = _Agenda_Atendimento.id_pessoa_empresa,
                                  nome_solicitante = _Agenda_Atendimento.nome_solicitante,

                                  // Dados do Responsavel a atender
                                  id_pessoa_Responsavel = _Agenda_Atendimento.id_pessoa_responsavel,
                                  razao_social_nome_Responsavel = _Pessoa.razao_social_nome,
                                  nome_fantasia_apelido_Responsavel = _Pessoa.nome_fantasia_apelido,
                                  cnpj_cpf_Responsavel = _Pessoa.cnpj_cpf,

                                  data = _Agenda_Atendimento.data,
                                  hora = _Agenda_Atendimento.hora,
                                  observacao = _Agenda_Atendimento.observacao,
                                  usuario = _Agenda_Atendimento.usuario,
                                  data_inclusao = _Agenda_Atendimento.data_inclusao
                              }).ToList();//.Skip(page * length).Take(length).ToList();

                IList<Agenda_Atendimento> agenda_atendimento = new List<Agenda_Atendimento>();

                foreach (var item in lQuery)
                {
                    Agenda_Atendimento _Agenda_Atendimento = new Agenda_Atendimento()
                    {
                        id = item.id,
                        protocolo = item.protocolo,
                        id_pessoa_empresa = item.id_pessoa_empresa,
                        nome_solicitante = item.nome_solicitante,
                        data = item.data,
                        hora = item.hora,
                        observacao = item.observacao,
                        // Pessoa Responsavel
                        Pessoa_Responsavel = new Pessoa()
                        {
                            id = (Guid)item.id_pessoa_Responsavel,
                            razao_social_nome = item.razao_social_nome_Responsavel,
                            nome_fantasia_apelido = item.nome_fantasia_apelido_Responsavel,
                            cnpj_cpf = item.cnpj_cpf_Responsavel,
                        },

                        usuario = item.usuario,
                        data_inclusao = item.data_inclusao
                    };

                    agenda_atendimento.Add(_Agenda_Atendimento);
                }

                return agenda_atendimento;
            }
        }

        /// <summary>
        /// Valida o Agendamento da Pessoa conforme os parametros
        /// </summary>
        /// <param name="id_empresa"></param>
        /// <param name="data"></param>
        /// <param name="hora"></param>
        /// <returns></returns>
        public bool ValidaAgendamento(Parametros_Pessoa paramentros_Pessoa, DateTime data, DateTime hora)
        {
            bool existe = false;

            using (var db = new PlataformaContext())
            {
                //var hora_agenda = hora.AddMinutes(20);
                var lQuery = (from _Agenda_Atendimento in db.Agenda_Atendimento
                              where _Agenda_Atendimento.id_pessoa_empresa == paramentros_Pessoa.id_pessoa_empresa
                              && _Agenda_Atendimento.data == data
                              && _Agenda_Atendimento.hora == Convert.ToString(hora.TimeOfDay)
                              && _Agenda_Atendimento.excluido == false
                              select _Agenda_Atendimento.id).ToList();

                if (lQuery.Count > 0)
                {
                    existe = true;
                }
                else
                {
                    existe = false;
                }
            }

            return existe;
        }

        /// <summary>
        /// Faz um insert na Tabela de Log com as mensagens
        /// </summary>
        /// <param name="mensagem"></param>
        public void AddtesteLog(string mensagem)
        {
            using (var db = new PlataformaContext())
            {
                //db.Database.ExecuteSqlCommand("INSERT INTO public.log_erro_aplicacao(id_pessoa_empresa, origem, mensagem, objeto, metodo, data_inclusao, usuario) VALUES('38c0d641-5ed1-4cb1-8f79-1d4317951ff2', 'teste', '" + mensagem + "', 'teste', 'teste', CURRENT_TIMESTAMP, 'Daniel');");
                Db.SaveChanges();
            }
        }
    }
}
