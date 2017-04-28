
using Domain;
using Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportUserTools.UserMigration
{
    static class ImportManager
    {
        public async static Task<Boolean> UpdateDbFormExcel()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            userManager.UserValidator = new UserValidator<ApplicationUser>(userManager) { AllowOnlyAlphanumericUserNames = false };
            Console.WriteLine("Database opened:");
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + "ExcelFolder", "users.xlsx");
            SLDocument sl = new SLDocument(path, "Лист1");
            SLWorksheetStatistics stats = sl.GetWorksheetStatistics();
            Console.WriteLine("Document users opened:");

            for (int row = 2; row < stats.EndRowIndex; row++)
            {
                try
                {
                    var Id = sl.GetCellValueAsString(row, 1);
                    var lastName = sl.GetCellValueAsString(row, 2);
                    var firstName = sl.GetCellValueAsString(row, 3);
                    var thirdName = sl.GetCellValueAsString(row, 4);
                    //кафедра
                    var department = sl.GetCellValueAsString(row, 5);
                    var basicOrCompatible = sl.GetCellValueAsString(row, 8);
                    var phone = sl.GetCellValueAsString(row, 9);
                    var email = sl.GetCellValueAsString(row, 10);
                    var document = sl.GetCellValueAsString(row, 13);
                    if (userManager.Users.Where(x => x.Email == email).Any())
                    {
                        Console.WriteLine("User {0} already exist",email);
                        continue;
                    }
                    var depId = db.Departments.Where(x => x.Name.Contains(department)).Select(x => x.Id).FirstOrDefault();
                    ApplicationUser newUser = new ApplicationUser();
                    Console.WriteLine("User- {0}",email);
                    newUser.FirstName = firstName;
                    newUser.LastName = lastName;
                    newUser.ThirdName = thirdName;
                    newUser.PhoneNumber = phone;
                    newUser.Document = document;
                    newUser.BasicOrCompatible = basicOrCompatible;
                    newUser.Email = email;
                    newUser.UserName = email;
                    //newUser.DepartmentUsers = new List<DepartmentUser>();
                    //newUser.DepartmentUsers.Add(new DepartmentUser()
                    //{
                    //    DepartmentId = depId

                    //});
                    var result = await userManager.CreateAsync(newUser,"HNEU1111!");
                    userManager.AddToRole(newUser.Id, "User");
                    db.SaveChanges();
                    if (result.Succeeded)
                        Console.WriteLine("User added ");
                    else
                        Console.WriteLine("User not added");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return false;
        }

    }
}
