using System;

namespace Plataforma.Domain.Entities.NotMapped
{
    /// <summary>
    /// Entidade de controle das permissão, tipagem dos retornos das permissões
    /// </summary>
    public class Retorno_Permissao_Grupo_Usuario
    {
        public Guid id_usuario { get; set; }
        public Guid id_pagina { get; set; }
        public bool ler { get; set; }
        public bool incluir { get; set; }
        public bool atualizar { get; set; }
        public bool deletar { get; set; }
        public bool upload { get; set; }
        public bool download { get; set; }
        public bool outros { get; set; }
    }

    public class Retorno_Permissao_Grupo_Pagina
    {
        public Guid id { get; set; }
        public Guid id_modulo { get; set; }
        public string nome_modulo { get; set; }
        public Guid id_menu { get; set; }
        public string nome_menu { get; set; }
        public Guid id_menu_sub { get; set; }
        public string nome_menu_sub { get; set; }
        public Guid id_grupo { get; set; }
        public string nome_grupo { get; set; }
        public Guid id_pagina { get; set; }
        public string nome_pagina { get; set; }
    }

    public class Retorno_Permissao_Grupo_Menu
    {
        public Guid id_grupo { get; set; }
        public string nome_grupo { get; set; }
        public Guid id_modulo { get; set; }
        public string nome_modulo { get; set; }
        public Guid id_menu { get; set; }
        public string nome_menu { get; set; }
    }
}