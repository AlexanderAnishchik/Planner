using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Load.Mapper.RowFormat;

namespace Load.Services
{

    public class UniqueDataResolver
    {
        private readonly List<DayFormatRow> _dayFormatRows;
        private readonly List<ExtraFormatRow> _extraFormatRows;

        public UniqueDataResolver(List<DayFormatRow> dayFormatRows, List<ExtraFormatRow> extraFormatRows)
        {
            _dayFormatRows = dayFormatRows;
            _extraFormatRows = extraFormatRows;
        }

        public List<string> GetUniqueSubjects()
        {
            List<string> daySubjects = _dayFormatRows
                .Select(s => s.Subject)
                .ToList();

            List<string> extraSubjects = _extraFormatRows
                .Select(s => s.Subject)
                .ToList();

            return daySubjects
                .Concat(extraSubjects)
                .Distinct()
                .ToList();
        }
        public List<string> GetUniqueSpecialties()
        {
            List<string> daySpecialties = _dayFormatRows
                .Select(sp => sp.Specialty).
                ToList();

            List<string> extraSpecialties = _extraFormatRows.
                Select(sp => sp.SpecialtyCipher).
                ToList();

            return daySpecialties
                .Concat(extraSpecialties)
                .Distinct()
                .ToList();
        }      
        public List<string> GetUniqueSpecializes()
        {
            return _dayFormatRows
                .Select(s => s.Specialize)
                .Distinct()
                .ToList();
        }
        public List<string> GetUniqueCourses()
        {
            List<string> dayCourses = _dayFormatRows
                .Select(c => c.Course)
                .ToList();

            List<string> extraCourses = _extraFormatRows
                .Select(c => c.Course)
                .ToList();

            return dayCourses
                .Concat(extraCourses)
                .Distinct()
                .ToList();
        }
        public List<string> GetUniqueFaculties()
        {
            return _dayFormatRows
                .Select(f => f.Faculty)
                .Distinct()
                .ToList();
        }
        public List<double> GetUniqueDepartments()
        {
            List<double> _dayDepartments = _dayFormatRows
                .Select(d => d.DepartmentCode)
                .ToList();

            List<double> _extraDepartments = _extraFormatRows
                .Select(d => d.DepartmentCode)
                .ToList();

            return _dayDepartments
                .Concat(_extraDepartments)
                .Distinct()
                .ToList();
        }
    }
}
