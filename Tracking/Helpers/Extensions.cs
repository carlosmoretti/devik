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
    }
}
