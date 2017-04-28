using System;
using Load.Mapper.RowFormat;

namespace Load.Mapper
{
    public class ExtraFormatMapper : AMapper
    {
        private ExtraFormatRow[] mappedData;

        public ExtraFormatMapper(object[,] rawData)
        {
            if (rawData != null)
            {
                _rawData = rawData;
                _rowsCount = _rawData.GetLength(0);
                _colsCount = _rawData.GetLength(1);

                mappedData = new ExtraFormatRow[_rowsCount];
            }
        }

        public override void Map()
        {
            ExtraFormatRow mappedObj;
            int length = mappedData.Length;

            for (int i = 0; i < length; i++)
            {
                mappedObj = (ExtraFormatRow)ConvertTo(ref _rawData, i);
                mappedData[i] = mappedObj;
            }
        }

        public override FormatRow[] GetMappedData()
        {
            return mappedData;
        }

        protected override FormatRow ConvertTo(ref object[,] row, int i)
        {
            ExtraFormatRow r = new ExtraFormatRow();
            String empty = String.Empty;

            r.DepartmentCode = Convert.ToDouble(row[i, 0]);
            r.DepartmentCipher = row[i, 1] == null ? empty : row[i, 1].ToString();
            r.Subject = row[i, 2] == null ? empty : row[i, 2].ToString();
            r.SpecialtyCipher = row[i, 3] == null ? empty : row[i, 3].ToString();
            r.Extra = row[i, 4] == null ? empty : row[i, 4].ToString();
            r.Course = row[i, 5] == null ? empty : row[i, 5].ToString();
            r.StudentsCount = Convert.ToDouble(row[i, 6]);
            r.QuantityOfGroups = Convert.ToDouble(row[i, 7]);
            r.QuantityOfThreads = Convert.ToDouble(row[i, 8]);
            r.ThreadNumber = Convert.ToDouble(row[i, 9]);
            r.MajorSpecialty = row[i, 10] == null ? empty : row[i, 10].ToString();
            r.TotalHours = Convert.ToDouble(row[i, 11]);
            r.Credits = Convert.ToDouble(row[i, 12]);

            r.First.Lectures = Convert.ToDouble(row[i, 13]);
            r.First.Practices = Convert.ToDouble(row[i, 14]);
            r.First.Labs = Convert.ToDouble(row[i, 15]);
            r.First.IndependentWorks = Convert.ToDouble(row[i, 16]);
            r.First.Exam = row[i, 17] == null ? empty : row[i, 17].ToString();
            r.First.Evaluation = row[i, 18] == null ? empty : row[i, 18].ToString();
            r.First.Projects = row[i, 19] == null ? empty : row[i, 19].ToString();
            r.First.Test = Convert.ToDouble(row[i, 20]);
            r.First.LimitOnProjects = Convert.ToDouble(row[i, 21]);

            r.Second.Lectures = Convert.ToDouble(row[i, 22]);
            r.Second.Practices = Convert.ToDouble(row[i, 23]);
            r.Second.Labs = Convert.ToDouble(row[i, 24]);
            r.Second.IndependentWorks = Convert.ToDouble(row[i, 25]);
            r.Second.Exam = row[i, 26] == null ? empty : row[i, 26].ToString();
            r.Second.Evaluation = row[i, 27] == null ? empty : row[i, 27].ToString();
            r.Second.Projects = row[i, 28] == null ? empty : row[i, 28].ToString(); //kr kp di
            r.Second.Test = Convert.ToDouble(row[i, 29]);
            r.Second.LimitOnProjects = Convert.ToDouble(row[i, 30]);

            return r;
        }

    }
}
