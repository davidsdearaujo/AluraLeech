using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluraBot.Browser
{
    internal static class Util
    {

        public static string RemoveAcentos(this string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public static List<string> ValorEntreTodos(this string text, string startString, string endString)
        {
            List<string> matched = new List<string>();
            int indexStart = 0, indexEnd = 0;
            bool exit = false;
            while (!exit)
            {
                indexStart = text.IndexOf(startString);
                indexEnd = text.IndexOf(endString);
                if (indexStart != -1 && indexEnd != -1)
                {
                    matched.Add(text.Substring(indexStart + startString.Length,
                        indexEnd - indexStart - startString.Length));
                    text = text.Substring(indexEnd + endString.Length);
                }
                else
                    exit = true;
            }
            return matched;
        }

        public static string ValorEntre(this string geral, string valorInicial, string valorFinal, int iniciarEm = 0)
        {
            try
            {
                var iRetorno = geral.IndexOf(valorInicial, iniciarEm) + valorInicial.Length;
                var fRetorno = geral.IndexOf(valorFinal, iRetorno);
                var cRetorno = fRetorno - iRetorno;
                return geral.Substring(iRetorno, cRetorno);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static T DefaultNull<T>(this object valor, T defaultValue)
        {
            return (valor != null ? (T)valor : defaultValue);
        }

        public static int ToInt(this string valor)
        {
            return int.Parse(valor);
        }

        public static string Formatar(this string texto)
        {
            return System.Net.WebUtility.HtmlDecode(texto);
        }
    }
}
