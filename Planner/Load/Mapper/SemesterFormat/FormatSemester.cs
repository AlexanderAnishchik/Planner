using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Load.Mapper.SemesterFormat
{
    public enum SemesterFormat : byte { DayFormat = 0, ExtraFormat = 1 }
    public enum SemesterType : byte { First = 1, Second = 2, Both = 3}
    public abstract class FormatSemester { }
}
