using System;

namespace Load.Reader
{
    public class ExReadParam
    {
        private string _pathToSouce;

        public string PathToSource
        {
            get { return _pathToSouce; }
            set
            {
                if (!String.IsNullOrEmpty(value))
                    _pathToSouce = value;
                else
                {
                    throw new Exception("ExReadParam: Incorrect path to source file!");
                }
            }
        }

        public int DayFormatHeaderLength { get; set; } = 5;
        public int ExtraFormatHeaderlength { get; set; } = 6;
        public int CountOfSheets { get; set; } = 2;
        public int DayFormatColsQuantity { get; set; } = 45;
        public int ExtraFormatColsQuantity { get; set; } = 31;
        public int DayFormatRowsQuantity { get; set; } = 134;
        public int ExtraFormatRowsQuantity { get; set; } = 67;
    }
}
