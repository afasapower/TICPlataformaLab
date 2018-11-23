using System;

namespace Plataforma.Domain.Entities.NotMapped
{
    public class Busca_Generica
    {
        /// <summary>
        /// Classe que define as informações transacionadas entre as camadas.
        /// </summary>
        public class Parametros_Pessoa
        {
            public Guid id { get; set; }
            public Guid? id_pessoa { get; set; }
            public Guid id_pessoa_empresa { get; set; }
            public string acao { get; set; }
            public string usuario { get; set; }
        }

        /// <summary>
        /// Classe para controle de navegação.
        /// </summary>
        public class Parametros_Busca_Navegacao
        {
            public Guid id { get; set; }
            public Guid id_pessoa_empresa { get; set; }
            public Guid id_usuario_logado { get; set; }
            public Guid id_empresa { get; set; }
            public Guid id_modulo { get; set; }
            public Guid id_pagina { get; set; }
            public Guid id_menu { get; set; }
            public Guid id_pessoa { get; set; }
            public Guid iditem { get; set; }
            public Guid combo_valor { get; set; }
            public string acao { get; set; }
            public string usuario { get; set; }
        }

        /// <summary>
        /// Classe para controle de registro exibidos na Grid.
        /// </summary>
        public class Parametros_Busca_Grid
        {
            public int length { get; set; }
            public string search { get; set; }
            public int draw { get; set; }
            public int page { get; set; }
            public int start { get; set; }

            public int column { get; set; }
            public string dir { get; set; }
            public Dados_Colunas columns { get; set; }
        }

        /// <summary>
        /// Classe que transaciona valores de busca da Grid.
        /// </summary>
        public class Busca_Colunas
        {
            public string value { get; set; }
            public string regex { get; set; }
        }

        /// <summary>
        /// Classe para controle de ordenação da grid.
        /// </summary>
        public class Dados_Colunas
        {
            public string data { get; set; }
            public string name { get; set; }
            public bool searchable { get; set; }
            public bool orderable { get; set; }
            public Busca_Colunas search { get; set; }
        }
    }
}