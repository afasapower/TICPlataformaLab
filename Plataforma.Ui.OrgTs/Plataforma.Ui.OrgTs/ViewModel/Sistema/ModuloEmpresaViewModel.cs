using Plataforma.Domain.Entities.NotMapped;
using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;

namespace Plataforma.Ui.OrgTs.ViewModel.Sistema
{
    public class ModuloEmpresaViewModel
    {
        public Retorno_Permissao_Grupo_Usuario permissoesMenus { get; set; }
        public List<Retorno_Permissao_Grupo_Usuario> listaPermissoesMenus { get; set; }

        public Modulo_Empresa moduloEmpresa { get; set; }
        public List<Modulo_Empresa> listaModuloEmpresa { get; set; }

        public Modulo modulo { get; set; }
        public List<Modulo> listaModulo { get; set; }

        public Pessoa pessoa { get; set; }
        public List<Pessoa> listaPessoa { get; set; }

        public Guid id_modulo_sistema { get; set; }
        public DateTime? data_validade { get; set; }
        public Guid id { get; set; }
        public string usuario { get; set; }
        public Guid id_pessoa_empresa { get; set; }

    }
}
