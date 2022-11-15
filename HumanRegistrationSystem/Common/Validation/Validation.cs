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
            if (string.IsNullOrWhiteSpace(responce.ToString()))  return true; 
            return false;
        }
    }
}
