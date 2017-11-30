using ImportUserTools.UserMigration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportUserTools
{
    class Program
    {
        static void Main(string[] args)
        {
            ImportManager.UpdateDbFormExcel().Wait();
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
