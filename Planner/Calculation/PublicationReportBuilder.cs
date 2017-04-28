using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;
using Domain.Models.Enums;
using Domain.Reports;
using System.ComponentModel.DataAnnotations;
using Domain.Helpers;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.IO;
using Calculation.Extensions;
using SpreadsheetLight;

namespace Calculation
{
    public static class PublicationReportBuilder
    {
        public static SLDocument FacultyReportBuild(string userName)
        {
            var folder = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            var path = Path.Combine(folder, "Blank_2017_Ind_plan.xlsx");

            SLDocument sl = new SLDocument(path, "НАВЧАЛЬНА РОБОТА");

            int currentRow = 5;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(x => x.UserName == userName);
                // sl.DeleteRow(currentRow, 1);

                if (user != null)
                {
                    foreach (var i in db.PlanTrainingJobs.ToList())
                    {
                        //sl.CopyRow(currentRow + 1, currentRow + 2, currentRow + 2, false);

                        sl.SetCellValue("B" + currentRow, i.OrderNumber);
                        sl.SetCellValue("C" + currentRow, i.Subject);
                        sl.SetCellValue("D" + currentRow, i.DSD);
                        sl.SetCellValue("E" + currentRow, i.Course);
                        sl.SetCellValue("F" + currentRow, i.CountStudent);
                        sl.SetCellValue("G" + currentRow, i.GroupCode);
                        sl.SetCellValue("H" + currentRow, i.PlannedLectures);
                        sl.SetCellValue("I" + currentRow, i.DoneLectures);
                        sl.SetCellValue("J" + currentRow, i.PlannedPract);
                        sl.SetCellValue("K" + currentRow, i.DonePract);
                        sl.SetCellValue("L" + currentRow, i.PlannedLaboratory);
                        sl.SetCellValue("M" + currentRow, i.DoneLaboratory);
                        sl.SetCellValue("N" + currentRow, i.PlannedSeminar);
                        sl.SetCellValue("O" + currentRow, i.DoneSeminar);
                        sl.SetCellValue("P" + currentRow, i.PlannedIndividual);
                        sl.SetCellValue("Q" + currentRow, i.DoneIndividual);
                        sl.SetCellValue("R" + currentRow, i.PlannedConsultation);
                        sl.SetCellValue("S" + currentRow, i.DoneConsultation);
                        sl.SetCellValue("T" + currentRow, i.PlannedExamConsultation);
                        sl.SetCellValue("U" + currentRow, i.DoneExamConsultation);
                        sl.SetCellValue("V" + currentRow, i.PlannedCheckControl);
                        sl.SetCellValue("W" + currentRow, i.DoneCheckControl);
                        sl.SetCellValue("X" + currentRow, i.PlannedCheckLectureControl);
                        sl.SetCellValue("Y" + currentRow, i.DoneCheckLectureControl);
                        sl.SetCellValue("Z" + currentRow, i.PlannedEAT);
                        sl.SetCellValue("AA" + currentRow, i.DoneEAT);
                        sl.SetCellValue("AB" + currentRow, i.PlannedCGS);
                        sl.SetCellValue("AC" + currentRow, i.DoneCGS);
                        sl.SetCellValue("AD" + currentRow, i.PlannedCoursework);
                        sl.SetCellValue("AE" + currentRow, i.DoneCoursework);
                        sl.SetCellValue("AF" + currentRow, i.PlannedOffsetting);
                        sl.SetCellValue("AG" + currentRow, i.DoneOffsetting);
                        sl.SetCellValue("AH" + currentRow, i.PlannedSemestrExam);
                        sl.SetCellValue("AI" + currentRow, i.DoneSemestrExam);
                        sl.SetCellValue("AJ" + currentRow, i.PlannedTrainingPract);
                        sl.SetCellValue("AK" + currentRow, i.DoneTrainingPract);
                        sl.SetCellValue("AL" + currentRow, i.PlannedStateExam);
                        sl.SetCellValue("AM" + currentRow, i.DoneStateExam);
                        sl.SetCellValue("AN" + currentRow, i.PlannedDiploma);
                        sl.SetCellValue("AO" + currentRow, i.DoneDiploma);
                        sl.SetCellValue("AP" + currentRow, i.PlannedPostgraduates);
                        sl.SetCellValue("AQ" + currentRow, i.DonePostgraduates);

                        currentRow++;
                    }


                    sl.SelectWorksheet("ТИТУЛ");

                    //sl.SetCellValue("B9", user.DepartmentUsers.FirstOrDefault().Department.Faculty.Name);
                    sl.SetCellValue("B9", "Test Faculty");
                    //sl.SetCellValue("B12", user.DepartmentUsers.FirstOrDefault().Department.Name);
                    sl.SetCellValue("B12", "Test department");
                    sl.SetCellValue("A24", user.FirstName + " " + user.LastName + " " + user.ThirdName);
                    sl.SetCellValue("A34", "2015 / 2016");
                    //sl.SetCellValue("B34", user.Position.Value.ToString());
                    sl.SetCellValue("B34", "Test position");
                    //sl.SetCellValue("C34", user.AcademicTitle.Value.ToString());
                    sl.SetCellValue("C34", "Test AcademicTitle");
                    //sl.SetCellValue("D34", user.Degree.Value.ToString());
                    sl.SetCellValue("D34", "Test Degree");
                    //sl.SetCellValue("E34", user.DepartmentUsers.FirstOrDefault().Rate.Value);
                    sl.SetCellValue("E34", "Test Rate");
                }

                sl.SelectWorksheet("МЕТОД+НАУК+ОРГАН");

                var methodicalWorks = db.PlanMethodicalWorks.ToList();
                currentRow = 3;
                for (int i = 0; i < methodicalWorks.Count; i++)
                {
                    int rowNum = currentRow + i;
                    sl.SetCellValue("A" + rowNum, i + 1);
                    sl.SetCellValue("B" + rowNum, methodicalWorks[i].Content);
                    sl.SetCellValue("C" + rowNum, methodicalWorks[i].Result);
                    sl.SetCellValue("D" + rowNum, methodicalWorks[i].DurationTime);
                    sl.SetCellValue("E" + rowNum, methodicalWorks[i].PlannedVolume);
                    sl.SetCellValue("F" + rowNum, methodicalWorks[i].ActualVolume);
                }

                //var scientificWorks = db.PlanScientificWorks.ToList();
                //currentRow = 17;
                //for (int i = 0; i < scientificWorks.Count; i++)
                //{
                //    int rowNum = currentRow + i;
                //    sl.SetCellValue("A" + rowNum, i + 1);
                //    sl.SetCellValue("B" + rowNum, scientificWorks[i].Content);
                //    sl.SetCellValue("C" + rowNum, scientificWorks[i].Result);
                //    sl.SetCellValue("D" + rowNum, scientificWorks[i].DurationTime);
                //    sl.SetCellValue("E" + rowNum, scientificWorks[i].PlannedVolume);
                //    sl.SetCellValue("F" + rowNum, scientificWorks[i].ActualVolume);
                //}

                var organizationalWorks = db.PlanManagments.ToList();
                currentRow = 31;
                for (int i = 0; i < organizationalWorks.Count; i++)
                {
                    int rowNum = currentRow + i;
                    sl.SetCellValue("A" + rowNum, i + 1);
                    sl.SetCellValue("B" + rowNum, organizationalWorks[i].Content);
                    sl.SetCellValue("C" + rowNum, organizationalWorks[i].Result);
                    sl.SetCellValue("D" + rowNum, organizationalWorks[i].DurationTime);
                    sl.SetCellValue("E" + rowNum, organizationalWorks[i].PlannedVolume);
                    sl.SetCellValue("F" + rowNum, organizationalWorks[i].ActualVolume);
                }

                sl.SelectWorksheet("ЗМІНИ ТА ВИСНОВКИ");

                var changes = db.PlanChanges.ToList();
                currentRow = 3;
                for (int i = 0; i < changes.Count; i++)
                {
                    int rowNum = currentRow + i;
                    sl.SetCellValue("A" + rowNum, changes[i].Semester);
                    sl.SetCellValue("B" + rowNum, changes[i].TypesfJobs);
                    sl.SetCellValue("C" + rowNum, changes[i].Changes);
                    sl.SetCellValue("D" + rowNum, changes[i].PlannedVolume);
                    sl.SetCellValue("E" + rowNum, changes[i].ActualVolume);
                    sl.SetCellValue("F" + rowNum, changes[i].Base);
                }

                var conclusions = db.PlanConclusions.ToList();
                currentRow = 15;
                for (int i = 0; i < conclusions.Count; i++)
                {
                    int rowNum = currentRow + i;
                    sl.SetCellValue("A" + rowNum, conclusions[i].Semester);
                    sl.SetCellValue("B" + rowNum, conclusions[i].Content);
                }

                var remarks = db.PlanRemarks.ToList();
                currentRow = 22;
                for (int i = 0; i < remarks.Count; i++)
                {
                    int rowNum = currentRow + i;
                    sl.SetCellValue("A" + rowNum, remarks[i].Date);
                    sl.SetCellValue("B" + rowNum, remarks[i].Remark);
                }
            }


            return sl;
        }

        #region Отчет по всем публикациям юзера
        public static List<PublicationForm11> CreateForm11(ApplicationUser user)
        {
            using (var db = new ApplicationDbContext())
            {
                var model = new List<PublicationForm11>();
                try
                {
                    var query = db.Publications
                    .Include("StoringType")
                    .Include("PublicationType")
                    .AsEnumerable()
                    .Where(x => x.IsPublished == true)
                    .Join(db.PublicationUsers, p => p.Id, pu => pu.PublicationId, (p, pu) => new { p, pu })
                    .Where(x => x.pu.UserId == user.Id)
                    .AsEnumerable()
                    .Select(x => new PublicationForm11()
                    {
                        Id = x.p.Id,
                        FilePath = x.p.FilePath,
                        Name = x.p.Name,
                        Pages = x.p.Pages.HasValue ? x.p.Pages.Value : x.p.Pages.Value,
                        Output = x.p.Output,
                        PublishedAt = x.p.PublishedAt.HasValue ? x.p.PublishedAt.Value : DateTime.MinValue,
                        StoringType =
                        ((DisplayAttribute)typeof(StoringTypeEnum)
                                .GetMember(x.p.StoringType.Value.ToString())[0]
                                .GetCustomAttributes(typeof(DisplayAttribute), false)[0]).Name,
                        PublicationType =
                        ((DisplayAttribute)typeof(PublicationTypeEnum)
                                .GetMember(x.p.PublicationType.Value.ToString())[0]
                                .GetCustomAttributes(typeof(DisplayAttribute), false)[0]).Name,
                        Collaborators = new List<Author>()

                    })
                    .ToList();

                    query.ForEach(x => x.Collaborators.AddRange(
                        db.PublicationUsers
                                .Where(p => p.PublicationId == x.Id)
                                .Where(u => u.UserId != user.Id)
                                .ToList()
                                .Select(a => new Author()
                                {
                                    UserId = a.UserId,
                                    CollaboratorId = a.CollaboratorId,
                                    Name = a.UserId != null ? $"{a.User.LastName} {a.User.FirstName} {a.User.ThirdName}"
                                                        : a.Collaborator.Name
                                })
                            .ToList()));

                    model = query;

                }
                catch (Exception)
                {
                    // ignored
                }
                return model;
            }
        }

        public static SLDocument PrintReportForm11(List<PublicationForm11> model, ApplicationUser user)
        {
            var folder = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            var path = Path.Combine(folder, "Form11.xlsx");
            SLDocument sl = new SLDocument(path, "Report");
            SLStyle styleWhite = sl.GetCellStyle("A4");
            styleWhite.SetWrapText(true);

            for (int i = 0; i < model.Count; i++)
            {

                sl.SetCellValue($"A{i + 4}", i + 1);
                sl.SetCellStyle($"A{i + 4}", styleWhite);

                sl.SetCellValue($"B{i + 4}", model[i].Name);
                sl.SetCellStyle($"B{i + 4}", styleWhite);

                sl.SetCellValue($"C{i + 4}", model[i].PublicationType);
                sl.SetCellStyle($"C{i + 4}", styleWhite);

                sl.SetCellValue($"D{i + 4}", model[i].Output);
                sl.SetCellStyle($"D{i + 4}", styleWhite);

                sl.SetCellValue($"E{i + 4}", model[i].PublishedAt.ToShortDateString());
                sl.SetCellStyle($"E{i + 4}", styleWhite);

                sl.SetCellValue($"F{i + 4}", model[i].Pages);
                sl.SetCellStyle($"F{i + 4}", styleWhite);

                sl.SetCellValue($"G{i + 4}", model[i].Collaborators.Count + 1);
                sl.SetCellStyle($"G{i + 4}", styleWhite);

                var value = "";
                if (model[i].Collaborators.Count > 0)
                {
                    value = model[i].Collaborators[0].Name;
                    foreach (var lab in model[i].Collaborators.Skip(1))
                        value += ", " + lab.Name;
                }
                else value = "Нет соавторов";

                sl.SetCellValue($"H{i + 4}", value);
                sl.SetCellStyle($"H{i + 4}", styleWhite);

            }

            var title = $"Наукові публікації {user.LastName} {user.FirstName.FirstOrDefault()}. {user.ThirdName.FirstOrDefault()}.";

            sl.SetCellValue("C2", title);

            return sl;

/*
            using (ExcelPackage pck = new ExcelPackage())
            {
                var datasource = CreateForm11(user);
                //Create the worksheet 
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Публикации " +
                    $"{user.LastName} {user.FirstName.Substring(0, 1)}. {user.ThirdName.Substring(0, 1)}.");

                var frmt = ws.Cells;
                frmt.Style.ShrinkToFit = false;
                frmt.Style.Indent = 5;
                frmt.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.Black);
                ws.DefaultColWidth = 200;

                ws.Column(1).AutoFit(35, 50);
                ws.Column(2).AutoFit(35, 50);
                ws.Column(3).AutoFit(35, 50);
                ws.Column(4).AutoFit(35, 50);
                ws.Column(5).AutoFit(35, 50);

                ws.Cells[1, 1].Value = "Назва";
                ws.Cells[1, 2].Value = "Характер роботи";
                ws.Cells[1, 3].Value = "Вихідні дані";
                ws.Cells[1, 4].Value = "Обсяг (стор.)";
                ws.Cells[1, 5].Value = "Співавтори";

                for (int i = 0; i < datasource.Count(); i++)
                {
                    ws.Cells[i + 2, 1].Value = datasource.ElementAt(i).Name;
                    ws.Cells[i + 2, 2].Value = datasource.ElementAt(i).StoringType;
                    ws.Cells[i + 2, 3].Value = datasource.ElementAt(i).Output;
                    ws.Cells[i + 2, 4].Value = datasource.ElementAt(i).Pages;
                    if (datasource.ElementAt(i).Collaborators.Count > 0)
                    {
                        ws.Cells[i + 2, 5].Value = datasource.ElementAt(i).Collaborators[0].Name;
                        foreach (var lab in datasource.ElementAt(i).Collaborators.Skip(1))
                            ws.Cells[i + 2, 5].Value += ", " + lab.Name;
                    }
                    else ws.Cells[i + 2, 5].Value = "Нет соавторов";


                }

                using (ExcelRange rng = ws.Cells[1, 1, 1, 5])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;        //Set Pattern for the background to Solid 
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(0, 200, 218, 230));  //Set color to DarkGray 
                    rng.Style.Font.Color.SetColor(Color.Black);
                }
                //return pck.GetAsByteArray();
                //pck.SaveAs(new FileInfo(filepath));
            }
*/
        }
        #endregion

        #region Отчет по кафедре за период
        public static List<PublicationOnDepartment> CreateDeparmentReport(string depId, DateTime? start = null, DateTime? end = null)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var model = new List<PublicationOnDepartment>();
                try
                {
                    //all users in selected department
                    var users = db.DepartmentUsers
                        .Where(x => x.DepartmentId == depId)
                        .Join(db.Users, du => du.UserId, u => u.Id, (du, u) => new { du, u })
                        .Select(u => u.u.Id)
                        .ToList();

                    //all publications of users in department
                    var publications = db.Publications
                        .Include("PublicationType")
                        .Include("ResearchDoneType")
                        .AsEnumerable()
                        .Join(db.PublicationUsers, p => p.Id, pu => pu.PublicationId, (p, pu) => new { p, pu })
                        .Where(x => users.Any(u => u == x.pu.UserId));
                    if (start != null && end != null)
                    {
                        publications = publications.Where(z => z.p.PublishedAt.Value >= start && z.p.PublishedAt.Value <= end);
                    }
                    publications = publications.OrderBy(x => x.p.PublishedAt)
                        .DistinctBy(x => x.p.Id);
                    //add PublicationNMBDs
                    var result = publications.Join(db.PublicationNMBDs, p => p.p.Id, pn => pn.PublicationId, (p, pn) => new { p, pn })
                        .AsEnumerable()
                        .Select(x => new PublicationOnDepartment()
                        {
                            Id = x.p.p.Id,
                            ImpactFactorNMBD = x.p.p.ImpactFactorNMBD,
                            NMBD = x.pn.NMBD.Name,
                            CitationNumberNMBD = x.p.p.CitationNumberNMBD,
                            Pages = x.p.p.Pages.Value,
                            IsOverseas = x.p.p.IsOverseas,
                            Name = x.p.p.Name,
                            Output = x.p.p.Output,
                            OwnerId = x.p.p.OwnerId,
                            PublicationType = ((DisplayAttribute)typeof(PublicationTypeEnum)
                                .GetMember(x.p.p.PublicationType.Value.ToString())[0]
                                .GetCustomAttributes(typeof(DisplayAttribute), false)[0]).Name,

                            ResearchDoneType = ((DisplayAttribute)typeof(ResearchDoneTypeEnum)
                                .GetMember(x.p.p.ResearchDoneType.Value.ToString())[0]
                                .GetCustomAttributes(typeof(DisplayAttribute), false)[0]).Name,
                            Collaborators = new List<Author>(),
                            Start = start,
                            End = end

                        })
                        .ToList();

                    //add collabs
                    result.ForEach(x => x.Collaborators.AddRange(
                        db.PublicationUsers
                                .Where(p => p.PublicationId == x.Id)
                                .ToList()
                                .Select(a => new Author()
                                {
                                    UserId = a.UserId,
                                    CollaboratorId = a.CollaboratorId,
                                    Name = a.UserId != null ? $"{a.User.LastName} {a.User.FirstName} {a.User.ThirdName}"
                                                        : a.Collaborator.Name
                                })
                            .ToList()));

                    //add department names
                    result.ForEach(x => x.DepartmentName =
                        db.DepartmentUsers
                            .Include("Department")
                            .FirstOrDefault(d => d.UserId == x.OwnerId).Department.Name);

                    model = result;

                }
                catch (Exception)
                {
                    // ignored
                }
                return model;
            }
        }

        public static SLDocument PrintDepartmentReport(List<PublicationOnDepartment> model)
        {
            var folder = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            var path = Path.Combine(folder, "DepartmentReport.xlsx");
            SLDocument sl = new SLDocument(path, "Report");
            SLStyle styleWhite = sl.GetCellStyle("A6");
            styleWhite.SetWrapText(true);
            SLStyle styleGreen = sl.GetCellStyle("H6");
            styleGreen.SetWrapText(true);
            for (int i = 0; i < model.Count; i++)
            {

                sl.SetCellValue($"A{i + 6}", i + 1);
                sl.SetCellStyle($"A{i + 6}", styleWhite);

                var authors = model[i].Collaborators[0].Name;
                foreach (var aut in model[i].Collaborators.Skip(1))
                {
                    authors += ", " + aut.Name;
                }
                sl.SetCellValue($"B{i + 6}", authors);
                sl.SetCellStyle($"B{i + 6}", styleWhite);

                sl.SetCellValue($"C{i + 6}", model[i].Name);
                sl.SetCellStyle($"C{i + 6}", styleWhite);

                sl.SetCellValue($"D{i + 6}", model[i].PublicationType);
                sl.SetCellStyle($"D{i + 6}", styleWhite);

                sl.SetCellValue($"E{i + 6}", model[i].Output);
                sl.SetCellStyle($"E{i + 6}", styleWhite);

                sl.SetCellValue($"F{i + 6}", model[i].Pages);
                sl.SetCellStyle($"F{i + 6}", styleWhite);

                sl.SetCellValue($"G{i + 6}", model[i].Pages);
                sl.SetCellStyle($"G{i + 6}", styleWhite);

                sl.SetCellValue($"H{i + 6}", model[i].IsOverseas ? "Так" : "Нi");
                sl.SetCellStyle($"H{i + 6}", styleGreen);

                sl.SetCellValue($"I{i + 6}", model[i].NMBD);
                sl.SetCellStyle($"I{i + 6}", styleGreen);

                sl.SetCellValue($"J{i + 6}", model[i].CitationNumberNMBD);
                sl.SetCellStyle($"J{i + 6}", styleGreen);

                sl.SetCellValue($"K{i + 6}", model[i].ImpactFactorNMBD);
                sl.SetCellStyle($"K{i + 6}", styleGreen);

                sl.SetCellValue($"L{i + 6}", model[i].ResearchDoneType);
                sl.SetCellStyle($"L{i + 6}", styleWhite);

                sl.SetCellValue($"M{i + 6}", model[i].DepartmentName);
                sl.SetCellStyle($"M{i + 6}", styleGreen);
            }
            var title = sl.GetCellValueAsString("C5");
            title += " кафедри " + model[0].DepartmentName;
            if (model[0].Start != null && model[0].End != null)
            {
                var period = model[0].Start.Value.ToShortDateString().Replace('/', '.')
                    + " - "
                    + model[0].End.Value.ToShortDateString().Replace('/', '.');
                title += " за перiод " + period;
            }

            sl.SetCellValue("C2", title);

            return sl;
        }

        #endregion

        #region Отчет за полугодие
        public static ScientificPublishingModel ScientificPublishing(string depId, int year, int half)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {

                var departmentName = db.Departments.Where(x => x.Id == depId).Select(x => x.Name).FirstOrDefault();
                //all users in selected department
                var users = db.DepartmentUsers
                    .Where(x => x.DepartmentId == depId)
                    .Join(db.Users, du => du.UserId, u => u.Id, (du, u) => new { du, u })
                    .Select(u => u.u.Id)
                    .ToList();

                var plans = db.ScientificPublishings
                    .Where(x => users.Any(u => u == x.UserId))
                    .Where(x => x.Year == year)
                    .ToList();

                var plan = plans.Aggregate((prev, cur) => new ScientificPublishing
                {
                    Abstracts = prev.Abstracts + cur.Abstracts,
                    AllPublications = prev.AllPublications + cur.AllPublications,
                    ArticlesInProfessionalPublications = prev.ArticlesInProfessionalPublications + cur.ArticlesInProfessionalPublications,
                    ArticlesThesesInNmbd = prev.ArticlesThesesInNmbd + prev.ArticlesThesesInNmbd,
                    Monographs = prev.Monographs + cur.Monographs,
                    MonographsForeignJournals = prev.MonographsForeignJournals + cur.MonographsForeignJournals,
                    MonographsNationalPublications = prev.MonographsNationalPublications + cur.MonographsNationalPublications,
                    ScientificArticlesInForeignLanguages = prev.ScientificArticlesInForeignLanguages + cur.ScientificArticlesInForeignLanguages,
                    ScientificPublicationsInForeignJournals = prev.ScientificPublicationsInForeignJournals + cur.ScientificPublicationsInForeignJournals,
                    ScientificPublicationsInScopus = prev.ScientificPublicationsInScopus + cur.ScientificPublicationsInScopus,
                    Year = prev.Year
                });
                var dates = new Dates(year);
                var period = String.Empty;
                if (half == 1)
                    period = "за І півріччя " + year + " року";
                else
                    period = "за ІІ півріччя " + year + " року";
                //all publications of users in department
                var publications = db.Publications
                    .Include("PublicationType")
                    .AsEnumerable()
                    .Join(db.PublicationUsers, p => p.Id, pu => pu.PublicationId, (p, pu) => new { p, pu })
                    .Where(x => users.Any(u => u == x.pu.UserId))
                    .OrderBy(x => x.p.PublishedAt)
                    .Where(x =>
                    {
                        if (half == 1)
                        {
                            return x.p.PublishedAt > dates.StartStudy && x.p.PublishedAt < dates.EndFirstHalf;
                        }
                        if (half == 2)
                        {
                            return x.p.PublishedAt > dates.EndFirstHalf && x.p.PublishedAt < dates.EndSecondHalf;
                        }
                        return false;
                    })
                    .DistinctBy(x => x.p.Id)
                    .Join(db.PublicationNMBDs, p => p.p.Id, pn => pn.PublicationId, (p, pn) => new { p, pn })
                    .ToList();

                var fact = new ScientificPublishing()
                {
                    AllPublications = publications.Count,

                    Abstracts = publications
                                .Where(x => x.p.p.PublicationType.Value == PublicationTypeEnum.Abstracts).Count(),

                    ArticlesInProfessionalPublications = publications
                                .Where(x => x.p.p.PublicationType.Value == PublicationTypeEnum.Article).Count(),

                    ArticlesThesesInNmbd = publications
                                .Where(x => x.p.p.PublicationNMBDs.Count() > 0
                                && x.p.p.PublicationType.Value == PublicationTypeEnum.Article).Count(),

                    MonographsForeignJournals = publications
                                .Where(x => x.p.p.IsOverseas
                                && (x.p.p.PublicationType.Value == PublicationTypeEnum.Monograph
                                || x.p.p.PublicationType.Value == PublicationTypeEnum.CollectiveMonograph
                                )).Count(),

                    Monographs = publications
                                .Where(x => x.p.p.PublicationType.Value == PublicationTypeEnum.Monograph
                                || x.p.p.PublicationType.Value == PublicationTypeEnum.CollectiveMonograph).Count(),

                    MonographsNationalPublications = publications
                                .Where(x => !x.p.p.IsOverseas
                                && (x.p.p.PublicationType.Value == PublicationTypeEnum.Monograph
                                || x.p.p.PublicationType.Value == PublicationTypeEnum.CollectiveMonograph
                                )).Count(),

                    ScientificArticlesInForeignLanguages = publications
                                .Where(x => x.p.p.IsOverseas
                                && x.p.p.PublicationType.Value == PublicationTypeEnum.Article).Count(),

                    ScientificPublicationsInForeignJournals = publications
                                .Where(x => x.p.p.IsOverseas).Count(),

                    ScientificPublicationsInScopus = publications
                                .Where(x => x.p.p.PublicationNMBDs.Count() > 0)
                                .Join(db.NMBDs, pnm => pnm.pn.NMBDId, nmbd => nmbd.Id, (pnm, nmbd) => new { pnm, nmbd })
                                .Where(x => x.nmbd.Name == "SCOPUS").Count()
                };

                var model = new ScientificPublishingModel()
                {
                    AllPublications = MakeTuple(plan.AllPublications, fact.AllPublications),

                    Abstracts = MakeTuple(plan.Abstracts, fact.Abstracts),

                    ArticlesInProfessionalPublications = MakeTuple(plan.ArticlesInProfessionalPublications, fact.ArticlesInProfessionalPublications),

                    ArticlesThesesInNmbd = MakeTuple(plan.ArticlesThesesInNmbd, fact.ArticlesThesesInNmbd),

                    MonographsForeignJournals = MakeTuple(plan.MonographsForeignJournals, fact.MonographsForeignJournals),

                    Monographs = MakeTuple(plan.Monographs, fact.Monographs),

                    MonographsNationalPublications = MakeTuple(plan.MonographsNationalPublications, fact.MonographsNationalPublications),

                    ScientificArticlesInForeignLanguages = MakeTuple(plan.ScientificArticlesInForeignLanguages, fact.ScientificArticlesInForeignLanguages),

                    ScientificPublicationsInForeignJournals = MakeTuple(plan.ScientificPublicationsInForeignJournals, fact.ScientificPublicationsInForeignJournals),

                    ScientificPublicationsInScopus = MakeTuple(plan.ScientificPublicationsInScopus, fact.ScientificPublicationsInScopus),
                    DepartmentName = departmentName,
                    Period = period
                };


                return model;
            }
        }
        public static SLDocument PrintHalfReport(ScientificPublishingModel model)
        {
            var folder = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            var path = Path.Combine(folder, "Dodatki.xlsx");
            SLDocument sl = new SLDocument(path, "Report");
            #region Науково-видавнича
            //Видання монографій (кількість):
            sl.SetCellValue("D16", model.Monographs.Item1);
            sl.SetCellValue("E16", model.Monographs.Item2);
            sl.SetCellValue("F16", model.Monographs.Item3);
            //у вітчизняний виданнях
            sl.SetCellValue("D17", model.MonographsNationalPublications.Item1);
            sl.SetCellValue("E17", model.MonographsNationalPublications.Item2);
            sl.SetCellValue("F17", model.MonographsNationalPublications.Item3);
            //у зарубіжних виданнях
            sl.SetCellValue("D18", model.MonographsForeignJournals.Item1);
            sl.SetCellValue("E18", model.MonographsForeignJournals.Item2);
            sl.SetCellValue("F18", model.MonographsForeignJournals.Item3);
            //ВСЬОГО ПУБЛІКАЦІЙ: в тому числі:
            sl.SetCellValue("D19", model.AllPublications.Item1);
            sl.SetCellValue("E19", model.AllPublications.Item2);
            sl.SetCellValue("F19", model.AllPublications.Item3);
            //наукові публікаціі в Scopus (кількість):
            sl.SetCellValue("D20", model.ScientificPublicationsInScopus.Item1);
            sl.SetCellValue("E20", model.ScientificPublicationsInScopus.Item2);
            sl.SetCellValue("F20", model.ScientificPublicationsInScopus.Item3);
            //публікацій (статі, тези), у виданнях, що входять до міжнародних науково- метричних баз даних (кількість):
            sl.SetCellValue("D21", model.ArticlesThesesInNmbd.Item1);
            sl.SetCellValue("E21", model.ArticlesThesesInNmbd.Item2);
            sl.SetCellValue("F21", model.ArticlesThesesInNmbd.Item3);
            //наукові публікації у зарубіжних виданнях (кількість).
            sl.SetCellValue("D22", model.ScientificPublicationsInForeignJournals.Item1);
            sl.SetCellValue("E22", model.ScientificPublicationsInForeignJournals.Item2);
            sl.SetCellValue("F22", model.ScientificPublicationsInForeignJournals.Item3);
            //статті у фахових видання (кількість):
            sl.SetCellValue("D23", model.ArticlesInProfessionalPublications.Item1);
            sl.SetCellValue("E23", model.ArticlesInProfessionalPublications.Item2);
            sl.SetCellValue("F23", model.ArticlesInProfessionalPublications.Item3);
            //публікація наукових статей іноземною мовою (кількість):
            sl.SetCellValue("D24", model.ScientificArticlesInForeignLanguages.Item1);
            sl.SetCellValue("E24", model.ScientificArticlesInForeignLanguages.Item2);
            sl.SetCellValue("F24", model.ScientificArticlesInForeignLanguages.Item3);
            //тези доповідей (кількість).
            sl.SetCellValue("D25", model.Abstracts.Item1);
            sl.SetCellValue("E25", model.Abstracts.Item2);
            sl.SetCellValue("F25", model.Abstracts.Item3);
            #endregion
            var title = sl.GetCellValueAsString("C5");
            sl.SetCellValue("C5", title + " " + model.DepartmentName + " " + model.Period);
            var period = String.Empty;
            if (model.Period.Contains("ІІ"))
                period = "II період(01.09 - 15.01)";
            else
                period = "I період (01.01-30.06)";
            sl.SetCellValue("C5", title + " " + model.DepartmentName + " " + model.Period);
            sl.SetCellValue("D7", period);

            return sl;
        }

        private static Tuple<int, int, string> MakeTuple(int plan, int fact)
        {
            return new Tuple<int, int, string>(plan, fact, plan != 0 ?
                    ((double)fact / (double)plan * 100).ToString("F2") + "%" : "0.00%");
        }
        #endregion

    }
}
