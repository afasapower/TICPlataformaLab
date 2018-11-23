using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;

namespace Plataforma.Ui.OrgTs.ViewModel.Components
{
    public class UploadArquivosViewModel
    {             
        public string NomeParamentroArquivo { get; set; } = "arquivo";
        public string NomeParametroTituloArquivo { get; set; } = "titulo";
        public string NomeParametroTipoArquivo { get; set; } = "id_tipo_arquivo";
        public string NomeParametroObservacaoArquivo { get; set; } = "observacao";

        public string TituloLabel { get; set; } = "Nome do Arquivo";

        public int ColunaGridMax { get; set; } = 8;
        public int QuantidadeArquivo { get; set; } = 1;

        public List<string> Extensoes { get; set; } = new List<string>() { ".xls", ".xlsx", ".jpg", ".png", ".doc", ".docx", ".pdf", ".rtf", ".odt" };


        public List<AttributosUploadArquivos> ListaArquivosSalvos { get; set; }

    }

    public class AttributosUploadArquivos
    {
        public Guid? id_tipo_arquivo { get; set; }       

        public string tituloArquivo { get; set; }
        public string valorArquivo { get; set; }
        public string descricaoArquivo { get; set; }
       
    }
}
