using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Plataforma.Domain.Entities.NotMapped.Busca_Generica;

namespace Plataforma.InfraEstrutura.Helpers
{
    public static class Listas
    {
        public static List<T> ProcessaLista<T>(List<T> lista, IFormCollection requisitaForm)
        {
            var capturaFormData = requisitaForm;
            int proximo = Convert.ToInt32(capturaFormData["start"].ToString());
            var quatidadePagina = Convert.ToInt32(capturaFormData["length"].ToString());
            Microsoft.Extensions.Primitives.StringValues tempOrder = new[] { "" };

            if (capturaFormData.TryGetValue("order[0][column]", out tempOrder))
            {
                var indiceColuna = capturaFormData["order[0][column]"].ToString();
                var ordenacao = capturaFormData["order[0][dir]"].ToString();
                tempOrder = new[] { "" };
                if (capturaFormData.TryGetValue($"columns[{indiceColuna}][data]", out tempOrder))
                {
                    var nomeColuna = capturaFormData[$"columns[{indiceColuna}][data]"].ToString();

                    if (quatidadePagina > 0)
                    {
                        var propriedades = typeof(T).GetProperties();
                        PropertyInfo propriedadeInfo = null;
                        foreach (var item in propriedades)
                        {
                            if (item.Name.ToLower().Equals(nomeColuna.ToLower()))
                            {
                                propriedadeInfo = item;
                                break;
                            }
                        }

                        if (ordenacao == "asc")
                            return lista.OrderBy(x => x.GetType().GetProperty(propriedadeInfo.Name).GetValue(x, null)).ToList();
                        else
                            return lista.OrderByDescending(x => x.GetType().GetProperty(propriedadeInfo.Name).GetValue(x, null)).ToList();
                    }
                    else
                        return lista;

                }
            }
            return lista;
        }

        public static Parametros_Busca_Grid CapturaGetLista(IFormCollection requisitaForm)
        {
            // Declaração da classe de parametros
            Parametros_Busca_Grid parametros = new Parametros_Busca_Grid();

            var capturaFormData = requisitaForm;            
            int draw = Convert.ToInt32(capturaFormData["draw"].ToString());
            int start = Convert.ToInt32(capturaFormData["start"].ToString());
            int page = Convert.ToInt32(capturaFormData["page"].ToString());
            int length = Convert.ToInt32(capturaFormData["length"].ToString());
            string search = capturaFormData["search"].ToString();

            // Parametros básicos
            parametros.draw = draw;
            parametros.start = start;
            parametros.page = page;
            parametros.length = length;
            parametros.search = search;

            // Declaração de variavel primitiva de ordenação
            Microsoft.Extensions.Primitives.StringValues ordenacaoTemporaria = new[] { "" };
            
            // Verifica se existe o valor inicial na string column
            if (capturaFormData.TryGetValue("order[0][column]", out ordenacaoTemporaria))
            {
                // Retorna o indice da coluna que foi clicada 
                string indiceColuna = capturaFormData["order[0][column]"].ToString();
                if(!String.IsNullOrEmpty(indiceColuna))
                {
                    parametros.column = Convert.ToInt32(indiceColuna);
                }

                // Retorna o tipo de ordenação que será feita (asc ou desc)
                string ordenacao = capturaFormData["order[0][dir]"].ToString();
                parametros.dir = ordenacao;

                // Retorna informações sobre as colunas 
                parametros.columns = new Dados_Colunas();
                ordenacaoTemporaria = new[] { "" };
                if (capturaFormData.TryGetValue($"columns[{indiceColuna}][data]", out ordenacaoTemporaria))
                {
                   parametros.columns.data = capturaFormData[$"columns[{indiceColuna}][data]"].ToString();                    
                }

                if (capturaFormData.TryGetValue($"columns[{indiceColuna}][name]", out ordenacaoTemporaria))
                {
                    parametros.columns.name = capturaFormData[$"columns[{indiceColuna}][name]"].ToString();
                }

                if (capturaFormData.TryGetValue($"columns[{indiceColuna}][orderable]", out ordenacaoTemporaria))
                {
                    string orderable = capturaFormData[$"columns[{indiceColuna}][orderable]"].ToString();
                    if(!String.IsNullOrEmpty(orderable))
                        parametros.columns.orderable = Convert.ToBoolean(orderable);
                }

                if (capturaFormData.TryGetValue($"columns[{indiceColuna}][searchable]", out ordenacaoTemporaria))
                {
                    string searchable = capturaFormData[$"columns[{indiceColuna}][searchable]"].ToString();
                    if (!String.IsNullOrEmpty(searchable))
                        parametros.columns.searchable = Convert.ToBoolean(searchable);
                }

                parametros.columns.search = new Busca_Colunas();
                if (capturaFormData.TryGetValue($"columns[{indiceColuna}][search][value]", out ordenacaoTemporaria))
                {
                    parametros.columns.search.value = capturaFormData[$"columns[{indiceColuna}][search][value]"].ToString();                               
                }

                if (capturaFormData.TryGetValue($"columns[{indiceColuna}][search][regex]", out ordenacaoTemporaria))
                {
                    parametros.columns.search.regex = capturaFormData[$"columns[{indiceColuna}][search][regex]"].ToString();
                }
            }            
            //-----------------------------------
            return parametros; 
        }
    }

    
}