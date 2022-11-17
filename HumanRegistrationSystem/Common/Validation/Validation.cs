using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Validation
{
    public static class Validation
    {
        public static bool CheckIfNull(object responce)
        {
            return string.IsNullOrWhiteSpace(responce.ToString());
        }
    }
}
