using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Correios.Helpers
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
