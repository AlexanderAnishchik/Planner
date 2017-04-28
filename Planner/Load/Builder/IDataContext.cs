using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Load.Builder
{
    public interface IDataContext {}

    public class EntryQueryContext : IDataContext
    {
        public string Loading { get; set; }
        public byte Semester { get; set; }
    }
}
