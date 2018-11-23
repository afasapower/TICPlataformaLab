using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Plataforma.Domain.Entities.Sistema;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Plataforma.InfraEstrutura.Helpers
{
    public class Conteudo
    {     
                
        public static string TrocaColchetes(string conteudo, List<Conteudo> itensSubstituicao)
        {
            string retorno = string.Empty;
            try
            {
                Regex expressao = new Regex("(?<=\\{\\{)(.*?)(?=\\}\\})");
                MatchCollection resultados = expressao.Matches(conteudo);
                foreach (Match match in resultados)
                {
               

                    string itemEncontrado = RemoveHtmlTags(match.Value.Trim());

                    if (itensSubstituicao.Exists(x => x.chave.Trim().Contains(itemEncontrado)))
                    {
                        var registro = itensSubstituicao.Find(x => x.chave.Trim() == itemEncontrado);                       

                        if (registro != null)                        
                            conteudo = conteudo.Replace(Regex.Replace(match.Value.Trim(), "<.*?>", String.Empty), registro.valor.Trim());
                        else
                            conteudo = conteudo.Replace(itemEncontrado,"Não cadastrado");
                    }else
                        conteudo = conteudo.Replace(match.Value, "Atributo ("+ itemEncontrado + ") não cadastrado");
                }
                retorno = Regex.Replace(conteudo, "[{{}}]", "");
            }
            catch (Exception)
            {
                retorno = "Erro ao tentar substituir os itens.";
            }
            return retorno;
        }
        public string chave { get; set; }
        public string valor { get; set; }        

        public static string DataExtenso(string data)
        {
            string retorno = String.Empty;

            try
            {
                DateTime dataConversao = DateTime.Parse(data);
                retorno = String.Format("{0} de {1} de {2}", dataConversao.Day.ToString(), dataConversao.ToString("MMMM"), dataConversao.Year.ToString());
            }
            catch (Exception)
            {
                retorno = "Erro na conversão da data.";
            }
            return retorno;
        }


        public static string DecimalExtenso(decimal valor)
        {
            if (valor <= 0 | valor >= 1000000000000000)
                return "Valor não suportado pelo sistema.";
            else
            {
                string strValor = valor.ToString("000000000000000.00");
                string valor_por_extenso = string.Empty;

                for (int i = 0; i <= 15; i += 3)
                {
                    valor_por_extenso += escreva_parte(Convert.ToDecimal(strValor.Substring(i, 3)));
                    if (i == 0 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(0, 3)) == 1)
                            valor_por_extenso += " Trilhão" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(0, 3)) > 1)
                            valor_por_extenso += " Trilhões" + ((Convert.ToDecimal(strValor.Substring(3, 12)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 3 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(3, 3)) == 1)
                            valor_por_extenso += " Bilhão" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(3, 3)) > 1)
                            valor_por_extenso += " Bilhões" + ((Convert.ToDecimal(strValor.Substring(6, 9)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 6 & valor_por_extenso != string.Empty)
                    {
                        if (Convert.ToInt32(strValor.Substring(6, 3)) == 1)
                            valor_por_extenso += " Milhão" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                        else if (Convert.ToInt32(strValor.Substring(6, 3)) > 1)
                            valor_por_extenso += " Milhões" + ((Convert.ToDecimal(strValor.Substring(9, 6)) > 0) ? " e " : string.Empty);
                    }
                    else if (i == 9 & valor_por_extenso != string.Empty)
                        if (Convert.ToInt32(strValor.Substring(9, 3)) > 0)
                            valor_por_extenso += " Mil" + ((Convert.ToDecimal(strValor.Substring(12, 3)) > 0) ? " e " : string.Empty);

                    if (i == 12)
                    {
                        if (valor_por_extenso.Length > 8)
                            if (valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "Bilhão" | valor_por_extenso.Substring(valor_por_extenso.Length - 6, 6) == "Milhão")
                                valor_por_extenso += " de";
                            else
                                if (valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "Bilhões" | valor_por_extenso.Substring(valor_por_extenso.Length - 7, 7) == "Milhões" | valor_por_extenso.Substring(valor_por_extenso.Length - 8, 7) == "Trilhões")
                                valor_por_extenso += " de";
                            else
                                    if (valor_por_extenso.Substring(valor_por_extenso.Length - 8, 8) == "Trilhões")
                                valor_por_extenso += " de";

                        if (Convert.ToInt64(strValor.Substring(0, 15)) == 1)
                            valor_por_extenso += " real";
                        else if (Convert.ToInt64(strValor.Substring(0, 15)) > 1)
                            valor_por_extenso += " reais";

                        if (Convert.ToInt32(strValor.Substring(16, 2)) > 0 && valor_por_extenso != string.Empty)
                            valor_por_extenso += " e ";
                    }

                    if (i == 15)
                        if (Convert.ToInt32(strValor.Substring(16, 2)) == 1)
                            valor_por_extenso += " Centavo";
                        else if (Convert.ToInt32(strValor.Substring(16, 2)) > 1)
                            valor_por_extenso += " Centavos";
                }
                return valor_por_extenso;
            }
        }

        public static string escreva_parte(decimal valor)
        {
            if (valor <= 0)
                return string.Empty;
            else
            {
                string montagem = string.Empty;
                if (valor > 0 & valor < 1)
                {
                    valor *= 100;
                }
                string strValor = valor.ToString("000");
                int a = Convert.ToInt32(strValor.Substring(0, 1));
                int b = Convert.ToInt32(strValor.Substring(1, 1));
                int c = Convert.ToInt32(strValor.Substring(2, 1));

                List<string> centenas = new List<string>() { "", "", "Duzentos", "Trezentos", "Quatrocentos", "Quinhentos", "Seiscentos", "Setecentos", "Oitocentos", "Novecentos" };
                if (a == 1)
                    montagem += (b + c == 0) ? "Cem" : "Cento";                
                else
                    montagem += centenas[a];

                if (b == 1)
                {
                    List<string> casaDezAvinte = new List<string>() { "Dez", "Onze", "Doze", "Treze", "Quatorze", "Quinze", "Dezesseis", "Dezessete", "Dezoito", "Dezenove" };
                    montagem += (a > 0 ? " e " : string.Empty) + casaDezAvinte[c];          
                }else
                {
                    List<string> dezenas = new List<string>() { "", "", "Vinte", "Trinta", "Quarenta", "Cinquenta", "Sessenta", "Setenta", "Oitenta", "Noventa" };
                    montagem += (a > 0 ? " e " : string.Empty) + dezenas[b];
                }      

                if (strValor.Substring(1, 1) != "1" & c != 0 & montagem != string.Empty) montagem += " e ";

                if (strValor.Substring(1, 1) != "1")
                {
                    List<string> casaDez = new List<string>() { "", "Um", "Dois", "Três", "Quatro", "Cinco", "Seis", "Sete", "Oito", "Nove" };
                    montagem += casaDez[c];
                }
                return montagem;
            }
        }        

        public static string RemoveHtmlTags(string html)
        {
            if (String.IsNullOrEmpty(html)) return "";
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc.DocumentNode.InnerText;
        }
    }


    public class PtBrDateTimeBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            var value = valueProviderResult.FirstValue;
            DateTime outDate;
            var parsed = DateTime.TryParse(value, new CultureInfo("pt-BR").DateTimeFormat,
                DateTimeStyles.None, out outDate);

            var result = ModelBindingResult.Success(outDate);
            if (!parsed)
            {
                result = ModelBindingResult.Failed();
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Data inválida");
            }

            bindingContext.Result = result;

            return Task.FromResult(0);
        }       
    }   
}
