using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Load.Helpers
{
    public static class DebugHelper
    {
        public static void Ochko(Type type, string method, string kuku)
        {
            string msg = $" {type.Name}.{method} > Msg: {kuku}";
            Debug.WriteLine(msg);
        }
    }
}
