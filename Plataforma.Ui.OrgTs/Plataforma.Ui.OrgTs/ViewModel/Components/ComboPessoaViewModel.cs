using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;

namespace Plataforma.Ui.OrgTs.ViewModel.Components
{
    public class ComboPessoaViewModel
    {    

        public Guid id_pessoa_empresa { get; set; }
        public Guid id_usuario_logado { get; set; }

        public List<Pessoa> listaDadosPessoas { get; set; }
        public Guid id_pessoa { get; set; }

        public bool visivelCombo { get; set; }
        public bool gridInicial { get; set; }

        public Guid idPessoaEmpresa { get; set; }
        public Guid idPessoa { get; set; }

    }
}

