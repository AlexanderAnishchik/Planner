using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Load.Mapper.RowFormat;

namespace Load.Mapper
{
    public class FormatMapper
    {
        public Task<List<DayFormatRow>> ToDayFormat(object[,] rawData)
        {
            if (rawData == null)
                throw new NullReferenceException("DayFormatMapper.Map - input data is null!");

            return Task.Run(() =>
            {
                int length = rawData.GetLength(0);
                List<DayFormatRow> mappedRows = new List<DayFormatRow>(length);

                for (int i = 0; i < length; i++)
                    mappedRows.Add((DayFormatRow) ToDayRow(rawData, i));

                return mappedRows;
            });
        }
        public Task<List<ExtraFormatRow>> ToExtraFormat(object[,] rawData)
        {
            if(rawData == null)
                throw new NullReferenceException("FormatMapper.ToExtraFormat - input data is null!");

            return Task.Run(() =>
            {
                int length = rawData.GetLength(0);
                List<ExtraFormatRow> mappedRows = new List<ExtraFormatRow>(length);

                for(int i=0;i<length; i++)
                    mappedRows.Add((ExtraFormatRow) ToExtraRow(rawData, i));

                return mappedRows;
            });
        }

        private DayFormatRow ToDayRow(object[,] row, int i)
        {
            DayFormatRow r = new DayFormatRow();
            String empty = String.Empty;

            // 45
            r.Faculty = row[i, 0] == null ? empty : row[i, 0].ToString();
            r.Specialty = row[i, 1] == null ? empty : row[i, 1].ToString();
            r.Specialize = row[i, 2] == null ? empty : row[i, 2].ToString();
            r.Course = row[i, 3] == null ? empty : row[i, 3].ToString();
            r.Degree = row[i, 4] == null ? empty : row[i, 4].ToString();
            r.StudentsCount = Convert.ToDouble(row[i, 5]);
            r.ForeignersCount = Convert.ToDouble(row[i, 6]);
            r.GroupsCipher = row[i, 7] == null ? empty : row[i, 7].ToString();
            r.QuantityOfGroupsA = Convert.ToDouble(row[i, 8]);
            r.RealQuantityOfGroups = Convert.ToDouble(row[i, 9]);
            r.QuantityOfGroupsB = Convert.ToDouble(row[i, 10]);
            r.QuantityOfThreads = Convert.ToDouble(row[i, 11]);
            r.ConflatedThreads = row[i, 12] == null ? empty : row[i, 12].ToString();
            r.Notes = row[i, 13] == null ? empty : row[i, 13].ToString();
            r.Subject = row[i, 14] == null ? empty : row[i, 14].ToString();
            r.QuantityOfCredits = Convert.ToDouble(row[i, 15]);
            r.Hours = Convert.ToDouble(row[i, 16]);
            r.Language = row[i, 17] == null ? empty : row[i, 17].ToString();
            r.QuantityOfWeeksFs = Convert.ToDouble(row[i, 18]);
            r.QuantityOfWeeksSs = Convert.ToDouble(row[i, 19]);

            r.First.TotalHours = Convert.ToDouble(row[i, 20]);
            r.First.Total = Convert.ToDouble(row[i, 21]);
            r.First.Lectures = Convert.ToDouble(row[i, 22]);
            r.First.Labs = Convert.ToDouble(row[i, 23]);
            r.First.Practices = Convert.ToDouble(row[i, 24]);
            r.First.IndependentWorks = Convert.ToDouble(row[i, 25]);
            r.First.Courses = row[i, 26] == null ? empty : row[i, 26].ToString();
            r.First.Exam = row[i, 27] == null ? empty : row[i, 27].ToString();
            r.First.Evaluation = row[i, 28] == null ? empty : row[i, 28].ToString();

            r.Second.TotalHours = Convert.ToDouble(row[i, 29]);
            r.Second.Total = Convert.ToDouble(row[i, 30]);
            r.Second.Lectures = Convert.ToDouble(row[i, 31]);
            r.Second.Labs = Convert.ToDouble(row[i, 32]);
            r.Second.Practices = Convert.ToDouble(row[i, 33]);
            r.Second.IndependentWorks = Convert.ToDouble(row[i, 34]);
            r.Second.Courses = row[i, 35] == null ? empty : row[i, 35].ToString();
            r.Second.Exam = row[i, 36] == null ? empty : row[i, 36].ToString();
            r.Second.Evaluation = row[i, 37] == null ? empty : row[i, 37].ToString();

            r.DepartmentCipher = row[i, 38] == null ? empty : row[i, 38].ToString();
            r.DepartmentCode = Convert.ToDouble(row[i, 39]);

            r.First.Coefficient = Math.Round(Convert.ToDouble(row[i, 40]), 2);
            r.Second.Coefficient = Math.Round(Convert.ToDouble(row[i, 41]), 2);

            r.Projects = Convert.ToDouble(row[i, 42]);
            r.Practices = Convert.ToDouble(row[i, 43]);
            r.QuantityOfMembers = Convert.ToDouble(row[i, 44]);

            return r;
        }
        private ExtraFormatRow ToExtraRow(object[,] row, int i)
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
