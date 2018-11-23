using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Plataforma.InfraEstrutura.Helpers
{
    public class Formularios
    {
        public static object CapturaModelErros(ModelStateDictionary data, bool excecao = false)
        {
            if (!excecao)
                return from e in data
                       where e.Value.Errors.Count > 0
                       select new
                       {
                           Excecao = false,
                           Name = e.Key,
                           Errors = e.Value.Errors.Select(x => x.ErrorMessage).Concat(e.Value.Errors.Where(x => x.Exception != null).Select(x => x.Exception.Message)).ToList<string>()
                       };
            else
                return new { Excecao = true, Name = "", Errors = data.Values.SelectMany(x => x.Errors) };
        }        

        public static List<Formularios> CaptaValoresEditorTexto(List<string> valores)
        {
            List<Formularios> itensEditorTexto = new List<Formularios>();
            try
            {
                int ponteiro = 0;
                for (int d = 0; d < valores.Count; d += 3)
                {
                    Guid id = new Guid(valores[ponteiro]);
                    ponteiro++;
                    string titulo = valores[ponteiro];
                    ponteiro++;
                    string documento = valores[ponteiro];
                    ponteiro++;
                    itensEditorTexto.Add(new Formularios()
                    {
                        idEditorTexto = id,
                        tituloEditorTexto = titulo,
                        documentoEditorTexto = documento
                    });
                }
            }
            catch (System.Exception)
            {
                throw;
            }
            return itensEditorTexto;
        }
        public Guid idEditorTexto { get; set; }
        public string tituloEditorTexto { get; set; }
        public string documentoEditorTexto { get; set; }

    }


    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    sealed public class CustomAttributeNoGuidEmpty : ValidationAttribute
    {

        // Returns:
        //     true if an empty string is allowed; otherwise, false. The default value is false.
        public bool AllowEmptyStrings { get; set; }

        public override bool IsValid(Object value)
        {
            bool result = true;
            if (value != null)
            {
                result = ((Guid)value == Guid.Empty ? false : true);
            }
            else
            {
                result = false;
            }
            return result;
        }
    }


    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    sealed public class ListHasElements : ValidationAttribute
    {        

        public override bool IsValid(Object mylist)
        {            
            if (mylist == null)
                return false;

            return true;
        }
    }



}
