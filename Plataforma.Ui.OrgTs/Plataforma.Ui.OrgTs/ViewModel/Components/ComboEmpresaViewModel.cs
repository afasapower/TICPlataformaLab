using System;
using System.Collections.Generic;
using Plataforma.Domain.Entities.NotMapped;

namespace Plataforma.Ui.OrgTs.ViewModel.Components
{
    public class ComboEmpresaViewModel
    {
        public List<Dados_Usuario_Empresa> listaDadosUsuarioEmpresa { get; set; }
        public Dados_Usuario_Empresa dadosUsuarioEmpresa { get; set; }

        public Guid id_pessoa_empresa { get; set; }
        public Guid id_usuario_logado { get; set; }

        public bool visivelCombo { get; set; }
        public bool gridInicial { get; set; }

        public Guid idPessoaEmpresa { get; set; }        

    }
}

