using System;
using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Load.Reader
{
    public enum EntryFormatType : int { Day = 1, Extra = 2 }

    public class InteropReader : IDisposable
    {
        private Application _app = new Application();
        private Workbook _workBook;
        private Worksheet _workSheet;
        private Range _range;
        private Dictionary<int, object[,]> _sheetDataDictionary;

        private int _rowsCount;
        private int _colsCount;

        public Dictionary<EntryFormatType, object[,]> Extract(string pathToFile)
        {
            ExReadParam readParam = new ExReadParam
            {
                PathToSource = pathToFile,
                DayFormatHeaderLength = 5,
                ExtraFormatHeaderlength = 6,
                DayFormatColsQuantity = 45,
                ExtraFormatColsQuantity = 31,
                DayFormatRowsQuantity = 134,
                ExtraFormatRowsQuantity = 67,
                CountOfSheets = 2
            };

            _workBook =
                _app.Workbooks.Open(readParam.PathToSource);

            if (_workBook.Sheets.Count == readParam.CountOfSheets)
            {
                _sheetDataDictionary = new Dictionary<int, object[,]>(readParam.CountOfSheets);

                for (int sheetNum = 1; sheetNum < readParam.CountOfSheets + 1; sheetNum++)
                {
                    _workSheet = (Worksheet)_workBook.Sheets[sheetNum];
                    _range = _workSheet.UsedRange;

                    object[,] data = (object[,])
                        _range.get_Value(XlRangeValueDataType.xlRangeValueDefault);

                    _sheetDataDictionary.Add(sheetNum, data);
                }
            }
            else
                throw new Exception("InteropReader: Count of lists in the book is not equal 2!");

            object[,] dayFormatObjects =
                Parse(EntryFormatType.Day, readParam);
            object[,] extraFormatObjects =
                Parse(EntryFormatType.Extra, readParam);

            return new Dictionary<EntryFormatType, object[,]>
            {
                [EntryFormatType.Day] = dayFormatObjects,
                [EntryFormatType.Extra] = extraFormatObjects
            };
        }
        public Task<Dictionary<EntryFormatType, object[,]>> ExtractAsync(string pathToFile)
        {
            return Task.Run( () =>       
            {
                ExReadParam readParam = new ExReadParam
                {
                    PathToSource = pathToFile,
                    DayFormatHeaderLength = 5,
                    ExtraFormatHeaderlength = 6,
                    DayFormatColsQuantity = 45,
                    ExtraFormatColsQuantity = 31,
                    DayFormatRowsQuantity = 134,
                    ExtraFormatRowsQuantity = 67,
                    CountOfSheets = 2
                };

                _workBook =
                    _app.Workbooks.Open(readParam.PathToSource);

                if (_workBook.Sheets.Count == readParam.CountOfSheets)
                {
                    _sheetDataDictionary = new Dictionary<int, object[,]>(readParam.CountOfSheets);

                    for (int sheetNum = 1; sheetNum < readParam.CountOfSheets + 1; sheetNum++)
                    {
                        _workSheet = (Worksheet) _workBook.Sheets[sheetNum];
                        _range = _workSheet.UsedRange;

                        object[,] data = (object[,])
                            _range.get_Value(XlRangeValueDataType.xlRangeValueDefault);

                        _sheetDataDictionary.Add(sheetNum, data);
                    }
                }
                else
                    throw new Exception("InteropReader: Count of lists in the book is not equal 2!");

                object[,] dayFormatObjects = Parse(EntryFormatType.Day, readParam);
                object[,] extraFormatObjects = Parse(EntryFormatType.Extra, readParam);

                return new Dictionary<EntryFormatType, object[,]>
                {
                    [EntryFormatType.Day] = dayFormatObjects,
                    [EntryFormatType.Extra] = extraFormatObjects
                };
            });
        }

        private object[,] Parse(EntryFormatType type, ExReadParam param)
        {
            object[,] sheetData = type == EntryFormatType.Day
                ? _sheetDataDictionary[(int)EntryFormatType.Day]
                : _sheetDataDictionary[(int)EntryFormatType.Extra];

            int headerLength = type == EntryFormatType.Day
                ? param.DayFormatHeaderLength
                : param.ExtraFormatHeaderlength;

            _colsCount = type == EntryFormatType.Day
                ? param.DayFormatColsQuantity
                : param.ExtraFormatColsQuantity;

            _rowsCount = type == EntryFormatType.Day
                ? param.DayFormatRowsQuantity
                : param.ExtraFormatRowsQuantity;

            object[,] rangedData =
                new object[_rowsCount, _colsCount];

            for (int i = 1 + headerLength; i < _rowsCount + headerLength + 1; i++)
            {
                for (int j = 1; j < _colsCount; j++)
                    rangedData[i - headerLength - 1, j - 1] = sheetData[i, j];
            }

            return rangedData;
        }

        void IDisposable.Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Marshal.FinalReleaseComObject(_range);
            Marshal.FinalReleaseComObject(_workSheet);

            _workBook.Close(true, Type.Missing, Type.Missing);
            Marshal.FinalReleaseComObject(_workBook);

            _app.Quit();
            Marshal.FinalReleaseComObject(_app);
            _app = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
