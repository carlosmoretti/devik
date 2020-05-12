using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeCantSpell.Hunspell;

namespace Model.Helpers
{
    public static class Extensions
    {
        public static bool IsEmpty(this string innertext)
        {
            var texto = innertext
                .Replace("\r\n", "")
                .Replace("\t", "")
                .Replace("&nbsp;", "")
                .Replace(" ", "");

            return String.IsNullOrEmpty(texto);
        }

        public static string ParseBOMChars(this string innertext)
        {
            var textos = innertext.Split(' ');
            var dictionary = WordList.CreateFromFiles("pt_BR.dic");
            StringBuilder sb = new StringBuilder();

            foreach(var item in textos)
            {
                if(item.Contains("�"))
                {
                    item.Replace("�", "");
                    var suggest = dictionary.Suggest(item);
                    sb.Append($"{suggest.FirstOrDefault()} ");
                } else
                    sb.Append($"{item} ");
            }

            return sb.ToString();
        }
    }
}
