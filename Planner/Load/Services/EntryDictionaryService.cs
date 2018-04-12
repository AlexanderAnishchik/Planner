using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Domain.Models;
using Load.Helpers;
using Load.Mapper.RowFormat;

namespace Load.Services
{
    public class EntryDictionaryService
    {
        #region Data

        private readonly UniqueDataResolver _uniqueDataResolver;

       // private Dictionary<string, Faculty> _facultiesDictionary = new Dictionary<string, Faculty>();
        private Dictionary<double, Department> _departmentsDictionary = new Dictionary<double, Department>();
        private Dictionary<string, Subject> _subjectsDictionary = new Dictionary<string, Subject>();
        private Dictionary<string, Specialty> _specialtiesDictionary = new Dictionary<string, Specialty>();
        private Dictionary<string, Specialize> _specializesDictionary = new Dictionary<string, Specialize>();
        private Dictionary<string, Course> _coursesDictionary = new Dictionary<string, Course>();

        //public Dictionary<string, Faculty> Faculties => _facultiesDictionary;
        public Dictionary<double, Department> Departments => _departmentsDictionary;
        public Dictionary<string, Subject> Subjects => _subjectsDictionary;
        public Dictionary<string, Specialty> Specialties => _specialtiesDictionary;
        public Dictionary<string, Specialize> Specializes => _specializesDictionary;
        public Dictionary<string, Course> Courses => _coursesDictionary;

        #endregion

        public EntryDictionaryService(UniqueDataResolver resolver)
        {
            _uniqueDataResolver = resolver;

            if(IsUploaded())
                Initialize();
        }

        public void Initialize()
        {
            using (var context = new ApplicationDbContext())
            {
                _subjectsDictionary = context.Subjects.ToDictionary(k => k.Name, v => v);
                _specialtiesDictionary = context.Specialties.ToDictionary(k => k.Code, v => v);
                _specializesDictionary = context.Specializes.ToDictionary(k => k.Cipher, v => v);
                _coursesDictionary = context.Courses.ToDictionary(k => k.Literal, v => v);
                //_facultiesDictionary = context.Faculties.DistinctBy(k => k.ShortName).ToDictionary(k => k.ShortName, v => v);
                _departmentsDictionary = context.Departments.DistinctBy(k => k.Code).ToDictionary(k => k.Code, v => v);
            }
        }

        public void InitialPush()
        {
            List<string> subjects = _uniqueDataResolver.GetUniqueSubjects();
            List<string> specialties = _uniqueDataResolver.GetUniqueSpecialties();
            List<string> specializes = _uniqueDataResolver.GetUniqueSpecializes();
            List<string> courses = _uniqueDataResolver.GetUniqueCourses();

            List<Subject> dboSubjects = new List<Subject>(subjects.Count);
            foreach (var sb in subjects)
                dboSubjects.Add(new Subject() {Name = sb});

            List<Specialty> dboSpecialties = new List<Specialty>(specialties.Count);
            foreach (var spt in specialties)
                dboSpecialties.Add(new Specialty() { Code = spt, Description = String.Empty });

            List<Specialize> dboSpecializes = new List<Specialize>(specializes.Count);
            foreach (var spl in specializes)
                dboSpecializes.Add(new Specialize() {Cipher = spl});

            List<Course> dboCourses = new List<Course>(courses.Count);
            foreach (var cs in courses)
                dboCourses.Add(new Course() {Literal = cs});

            using (var db = new ApplicationDbContext())
            {
                foreach ( var sb in dboSubjects)
                    db.Entry(sb).State = EntityState.Added;

                foreach ( var spt in dboSpecialties)
                    db.Entry(spt).State = EntityState.Added;

                foreach ( var spl in dboSpecializes)
                    db.Entry(spl).State = EntityState.Added;

                foreach ( var cs in dboCourses)
                    db.Entry(cs).State = EntityState.Added;

                db.SaveChanges();
            }

            Initialize();
        }
       
        public bool FindNew()
        {
            bool founded = false;

            List<string> fileSubjects = _uniqueDataResolver.GetUniqueSubjects();
            List<string> fileSpecialties = _uniqueDataResolver.GetUniqueSpecialties();
            List<string> fileSpecializes = _uniqueDataResolver.GetUniqueSpecializes();
            List<string> fileCourses = _uniqueDataResolver.GetUniqueCourses();

            List<string> dboSubjects, dboSpecialties,dboSpecializes,dboCourses;

            using (var context = new ApplicationDbContext())
            {
                dboSubjects = context.Subjects.Select(s => s.Name).ToList();
                dboSpecialties = context.Specialties.Select(s => s.Code).ToList();
                dboSpecializes = context.Specializes.Select(s => s.Cipher).ToList();
                dboCourses = context.Courses.Select(s => s.Literal).ToList();
            }

            var newSubjects = dboSubjects.Except(fileSubjects).ToList();
            var newSpecialties = dboSpecialties.Except(fileSpecialties).ToList();
            var newSpecializes = dboSpecializes.Except(fileSpecializes).ToList();
            var newCourses = dboCourses.Except(fileCourses).ToList();

            List<Subject> newDbSubjects = new List<Subject>();
            if (newSubjects.Any())
            {
                founded = true;
                foreach (string nsb in newSubjects)
                    newDbSubjects.Add(new Subject() {Name = nsb});
            }

            List<Specialty> newDbSpecialties = new List<Specialty>();
            if (newSpecialties.Any())
            {
                founded = true;
                foreach (string nspt in newSpecialties)
                    newDbSpecialties.Add(new Specialty() {Code = nspt, Description = String.Empty});
            }

            List<Specialize> newDbSpecializes = new List<Specialize>();
            if (newSpecializes.Any())
            {
                founded = true;
                foreach (string nspl in newSpecializes)
                    newDbSpecializes.Add(new Specialize() {Cipher = nspl});
            }

            List<Course> newDbCourses = new List<Course>();
            if (newCourses.Any())
            {
                founded = true;
                foreach (string ncs in newCourses)
                    newDbCourses.Add(new Course() {Literal = ncs});
            }

            try
            {
                using (var db = new ApplicationDbContext())
                {
                    if (newDbSubjects.Any())
                        foreach (var nsb in newDbSubjects)
                            db.Entry(nsb).State = EntityState.Added;

                    if (newDbSpecialties.Any())
                        foreach (var nspt in newDbSpecialties)
                            db.Entry(nspt).State = EntityState.Added;

                    if (newDbSpecializes.Any())
                        foreach (var nspl in newDbSpecializes)
                            db.Entry(nspl).State = EntityState.Added;

                    if (newDbCourses.Any())
                        foreach (var ncs in newDbCourses)
                            db.Entry(ncs).State = EntityState.Added;

                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                DebugHelper.Ochko(typeof(EntryDictionaryService),"FindNew",ex.Message);
            }

            return founded;
        }

        public void Update()
        {
            Initialize();
        }

        public bool IsInitialized()
        {
            return //_facultiesDictionary.Count > 0
                   //&&
                   _specialtiesDictionary.Count > 0
                   && _specializesDictionary.Count > 0
                   && _coursesDictionary.Count > 0
                   && _departmentsDictionary.Count > 0;
        }

        public bool IsUploaded()
        {
            bool isUploaded;
            using (var context = new ApplicationDbContext())
                isUploaded = context.LoadingLists.Any();

            return isUploaded;
        }
    }
}
