using System;

namespace Plataforma.InfraEstrutura.Helpers
{
    /// <summary>
    /// Implementa métodos responsáveis por atribuir valores após realização de verificação.
    /// </summary>
    public class Atribuicoes
    {
        /// <summary>
        /// Atribui um valor ao campo id se o campo id existir em um objeto dinâmico.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static dynamic AtribuirId(dynamic obj, string name, object id)
        {
            var existeId = Verificacoes.VerificaPropriedadeExiste(obj, "Id");

            if (existeId)
            {
                obj.Id = id;
            }

            return obj;
        }

        /// <summary>
        /// Atribui um valor ao campo excluido quando esse existir.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static dynamic AtribuirBoolExcluido(dynamic obj, string name, bool value)
        {
            var existeExcluido = Verificacoes.VerificaPropriedadeExiste(obj, name);
            var existeDataExclusao = Verificacoes.VerificaPropriedadeExiste(obj, "data_exclusao");

            if (existeExcluido)
            {
                obj.Excluido = value;               
            }
            if (existeDataExclusao) {

                obj.DataExclusao = DateTime.Now;
            }

            return obj;
        }

        /// <summary>
        /// Atribui um valor ao campo id se o campo id existir em um objeto dinâmico.
        /// </summary>
        /// <param name="obj"></param>       
        /// <param name="id"></param>
        /// <returns></returns>
        public static dynamic AtribuirUsuarioInclusaoId(dynamic obj, string usuarioIdInclusao)
        {
            var existePropriedade = Verificacoes.VerificaPropriedadeExiste(obj, "usuario");

            if (existePropriedade)
            {
                obj.UsuarioIdInclusao = usuarioIdInclusao;
            }

            return obj;
        }       

        //public static object AtribuirIdFiltro<TEntity>(Plataforma.Domain.Filters.IFilter<TEntity> filter, string v) where TEntity : class
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// Se existir o atributo Id e o usuário logado for uma empresa o campo é preenchido com o Id do
        /// usuário que estiver logado.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IdUsuarioLogado"></param>
        /// <returns></returns>
        public static dynamic AtribuirIdFiltro(dynamic obj, string IdUsuarioLogado) {

            var existePropriedade = Verificacoes.VerificaPropriedadeExiste(obj, "id");

            if (existePropriedade)
            {
                if (IdUsuarioLogado != null) {

                    obj.Id = IdUsuarioLogado;
                }                
            }

            return obj;
        }

        /// <summary>
        /// Se existir o atributo Id e o usuário logado for um lojista o campo é preenchido com o Id do
        /// usuário que estiver logado.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="IdUsuarioLogado"></param>
        /// <returns></returns>
        public static dynamic AtribuirIdEntidade(dynamic obj, string IdUsuarioLogado)
        {

            var existePropriedade = Verificacoes.VerificaPropriedadeExiste(obj, "id");

            if (existePropriedade)
            {
                if (IdUsuarioLogado != null)
                {
                    obj.Id = IdUsuarioLogado;
                }
            }

            return obj;
        }

    }
}
