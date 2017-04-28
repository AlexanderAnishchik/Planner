using Domain;
using Domain.Models;
using EfEnumToLookup.LookupGenerator;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

namespace Planner
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        /// <summary>
        /// Add default user
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ApplicationDbContext context)
        {
            var enumToLookup = new EnumToLookup();
            enumToLookup.Apply(context);

            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // Create two role
            var role1 = new IdentityRole { Name = "Admin" };
            var role2 = new IdentityRole { Name = "User" };
            var Teacher = new IdentityRole { Name = "Teacher" };
            var TeacherModerator = new IdentityRole { Name = "TeacherModerator" };
            var HeadOfMethodologyDepartment = new IdentityRole { Name = "HeadOfMethodologyDepartment" };
            var HeadOfStudies = new IdentityRole { Name = "HeadOfStudies" };
            var HeadOfScientificSector = new IdentityRole { Name = "HeadOfScientificSector" };


            // Add role to db
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(Teacher);
            roleManager.Create(TeacherModerator);
            roleManager.Create(HeadOfMethodologyDepartment);
            roleManager.Create(HeadOfStudies);
            roleManager.Create(HeadOfScientificSector);

            // Create users
            var admin = new ApplicationUser { Email = "administrator@gmail.com", UserName = "administrator@gmail.com" };
            string password = "Administrator123123!!!";
            var result = userManager.Create(admin, password);

            // Success
            if (result.Succeeded)
            {
                // Add role for user
                userManager.AddToRole(admin.Id, role1.Name);
                //userManager.AddToRole(admin.Id, role2.Name);
            }
            InitFaculties(context);
            InitNMBDs(context);
            base.Seed(context);

        }


        private void InitNMBDs(ApplicationDbContext context)
        {
            context.NMBDs.Add(new NMBD() { Name = "SCOPUS" });
            context.NMBDs.Add(new NMBD() { Name = "Web of Science" });
            context.NMBDs.Add(new NMBD() { Name = "Index Copernicus" });
            context.NMBDs.Add(new NMBD() { Name = "Thomson" });
            context.NMBDs.Add(new NMBD() { Name = "РИНЦ" });

            base.Seed(context);
        }
        private void InitFaculties(ApplicationDbContext context)
        {
            List<Faculty> f = new List<Faculty>();
            f.Add(new Faculty()
            {
                Name = "Факультет консалтингу i мiжнародного бiзнесу",
                ShortName = "КіМБ",
                Departments = new List<Department>()
                {
#region Кафедра бухгалтерського облiку
                    new Department() {Name="Кафедра бухгалтерського облiку",Schedules=new List<Schedule>() {
                         new Schedule() {UserName="Андрющенко Віта Олександрівна",ApiId="352443" },
                         new Schedule() {UserName="Безкоровайна Лідія Василівна",ApiId="354064" },
                         new Schedule() {UserName="Бондаренко Тетяна Валеріївна",ApiId="353784" },
                         new Schedule() {UserName="Волошан Ірина Геннадіївна",ApiId="353015" },
                         new Schedule(){UserName="Глєбова Наталія Володимирівна",ApiId="334750" },
                         new Schedule(){UserName="Горяєва Марина Сергіївна",ApiId="352444" },
                         new Schedule(){UserName="Денисюк Ольга Василівна",ApiId="348086" },
                         new Schedule(){UserName="Дзьобко Ірина Петрівна",ApiId="297288" },
                         new Schedule(){UserName="Доценко Наталія Сергіївна",ApiId="357224" },
                         new Schedule(){UserName="Дуда Оксана Вікторівна",ApiId="306738" },
                         new Schedule(){UserName="Жовтопуп Микола Миколайович",ApiId="320069" },
                         new Schedule(){UserName="Зубчинська Надія Максимівна",ApiId="335075" },
                         new Schedule(){UserName="Клімов Андрій Віталійович",ApiId="356527" },
                         new Schedule(){UserName="Кудіна Ольга Миколаївна",ApiId="357794" },
                         new Schedule(){UserName="Курган Наталя Володимирівна",ApiId="357795" },
                         new Schedule(){UserName="Лабунська Світлана Віталіївна",ApiId="352517" },
                         new Schedule(){UserName="Леонова Юлія Олександрівна",ApiId="357796" },
                         new Schedule(){UserName="Литвиненко Аліна Олександрівна",ApiId="353012" },
                         new Schedule(){UserName="Маляревський Юрій Дмитрович",ApiId="358235" },
                         new Schedule(){UserName="Ольховська Віра Вікторівна",ApiId="357606"},
                         new Schedule(){UserName="Пасенко Наталія Сергіївна",ApiId="352742"},
                         new Schedule(){UserName="Пасічник Інна Юріївна",ApiId="340515"},
                         new Schedule(){UserName="Перепеча Тетяна Михайлівна",ApiId="321309"},
                         new Schedule(){UserName="Пилипенко Андрій Анатолійович",ApiId="352938"},
                         new Schedule(){UserName="Писарчук Оксана Володимирівна",ApiId="342902"},
                         new Schedule(){UserName="Прокопішина Олена Вікторівна",ApiId="354094"},
                         new Schedule(){UserName="Сатушева Карина Валеріївна",ApiId="307713"},
                         new Schedule(){UserName="Сахаров Павло Олександрович",ApiId="356255"},
                         new Schedule(){UserName="Сердечна Світлана Миколаївна",ApiId="357039"},
                         new Schedule(){UserName="Сєрікова Тетяна Миколаївна",ApiId="352357"},
                         new Schedule(){UserName="Сліпушенко Ганна Сергіївна",ApiId="335076"},
                         new Schedule(){UserName="Тирінов Андрій Вікторович",ApiId="304952"},
                         new Schedule(){UserName="Тютлікова Вікторія Валеріївна",ApiId="307716"},
                         new Schedule(){UserName="Ус Галина Олександрівна",ApiId="354342"},
                         new Schedule(){UserName="Фартушняк Ольга Вікторівна",ApiId="340516"},
                         new Schedule(){UserName="Цибулько Дмитро Іванович",ApiId="357240"},
                         new Schedule(){UserName="Цюрко Інна Анатоліївна",ApiId="352519"},
                         new Schedule(){UserName="Часовнікова Юлія Сергіївна",ApiId="352743"},
                         new Schedule(){UserName="Черноіванова Ганна Степанівна",ApiId="357239"},
                         new Schedule(){UserName="Чухлєбова Тетяна Олександрівна",ApiId="352807"},
                         new Schedule(){UserName="Шушлякова Оксана Валеріївна",ApiId="352847"},
                         new Schedule(){UserName="Ялдін Ігор Володимирович",ApiId="352406"}
                     } },
                    #endregion
                    #region Кафедра економiчного аналiзу
                    new Department() {Name="Кафедра економiчного аналiзу",Schedules=new List<Schedule>() {
                         new Schedule() {UserName="Азаренков Григорій Федорович",ApiId="352358" },
                         new Schedule() {UserName="Афанасьєва Лариса Миколаївна",ApiId="355147" },
                         new Schedule() {UserName="Гапон Юлія Володимирівна",ApiId="355149" },
                         new Schedule() {UserName="Іванієнко Віктор Васильович",ApiId="336082" },
                         new Schedule(){UserName="Іващенко Ганна Анатоліївна",ApiId="352744" },
                         new Schedule(){UserName="Корчагіна Галина Анатоліївна",ApiId="352808" },
                         new Schedule(){UserName="Котов Антон Миколайович",ApiId="357798" },
                         new Schedule(){UserName="Кузнецов Олег Віталійович",ApiId="352330" },
                         new Schedule(){UserName="Мішин Олександр Юрійович",ApiId="352745" },
                         new Schedule(){UserName="Москаленко Наталя Олександрівна",ApiId="352746" },
                         new Schedule(){UserName="Отенко Ірина Павлівна",ApiId="357752" },
                         new Schedule(){UserName="Перепелюкова Олена Василівна",ApiId="353101" },
                         new Schedule(){UserName="Петряєва Зоя Федорівна",ApiId="340527" },
                         new Schedule(){UserName="Преображенська Олена Сергіївна",ApiId="354093" },
                         new Schedule(){UserName="Савенко Ксенія Сергіївна",ApiId="352410" },
                         new Schedule(){UserName="Сапрун Галина Григорівна",ApiId="340528" },
                         new Schedule(){UserName="Фадєєва Юлія Володимирівна",ApiId="352809" },
                         new Schedule(){UserName="Чепелюк Маргарита Ігорівна",ApiId="355150" },
                         new Schedule(){UserName="Чечетова-Терашвілі Тетяна Михайлівна",ApiId="352409" },
                         new Schedule(){UserName="Яртим Ірина Анатоліївна",ApiId="355148"}
                     } },
#endregion
                    #region Кафедра вищої математики та оцiнки майна пiдприємств
                    new Department() {Name="Кафедра вищої математики та оцiнки майна пiдприємств",Schedules=new List<Schedule>() {
                         new Schedule(){UserName="Афанасьєва Лідія Михайлівна",ApiId="352756" },
                         new Schedule(){UserName="Воронін Анатолій Віталійович",ApiId="340524" },
                         new Schedule(){UserName="Гунько Ольга Володимирівна",ApiId="340523" },
                         new Schedule(){UserName="Денисова Тетяна Володимирівна",ApiId="352747" },
                         new Schedule(){UserName="Железнякова Еліна Юріївна",ApiId="340526" },
                         new Schedule(){UserName="Ковальова Катерина Олександрівна",ApiId="352524" },
                         new Schedule(){UserName="Лебедєв Степан Сергович",ApiId="341123" },
                         new Schedule(){UserName="Лебедєва Ірина Леонідівна",ApiId="335098" },
                         new Schedule(){UserName="Мінєнкова Олена Вадимівна",ApiId="352758" },
                         new Schedule(){UserName="Місюра Євгенія Юріївна",ApiId="357242" },
                         new Schedule(){UserName="Норік Лариса Олексіївна",ApiId="328705" },
                         new Schedule(){UserName="Рибалко Антоніна Павлівна",ApiId="312310" },
                         new Schedule(){UserName="Сенчуков Віктор Федорович",ApiId="352759" },
                         new Schedule(){UserName="Сілічова Тетяна Василівна",ApiId="336081" },
                         new Schedule(){UserName="Стєпанова Катерина Вадимівна",ApiId="348151" },
                         new Schedule(){UserName="Тижненко Олександр Григорович",ApiId="342921" },
                         new Schedule(){UserName="Шевченко Олександра Кирилівна",ApiId="357241" },
                         new Schedule(){UserName="Шупіков Олександр Миколайович",ApiId="277386" },
                     } },
                    #endregion
#region Кафедра контролю i аудиту
                    new Department() {Name="Кафедра контролю i аудиту",Schedules=new List<Schedule>() {
                         new Schedule(){UserName="Бондаренко Олена Олександрівна",ApiId="357243" },
                         new Schedule(){UserName="Бутенко Каріна Володимирівна",ApiId="342934" },
                         new Schedule(){UserName="Владімірова Наталія Петрівна",ApiId="345298"},
                         new Schedule(){UserName="Воінова Тетяна Сергіївна",ApiId="328633"},
                         new Schedule(){UserName="Дейнеко Євген Валерійович",ApiId="352810" },
                         new Schedule(){UserName="Дікань Лариса Василівна",ApiId="352946" },
                         new Schedule(){UserName="Калінкін Дмитро Володимирович",ApiId="355151" },
                         new Schedule(){UserName="Кожушко Ольга Вікторівна",ApiId="340677" },
                         new Schedule(){UserName="Крівцова Тетяна Олександрівна",ApiId="352850" },
                         new Schedule(){UserName="Лядова Юлія Олександрівна",ApiId="352848" },
                         new Schedule(){UserName="Мултанівська Тетяна Володимирівна",ApiId="352762" },
                         new Schedule(){UserName="Понікаров Валерій Дмитрович",ApiId="357751" },
                         new Schedule(){UserName="Шевченко Ірина Олександрівна",ApiId="357421" }
                     } },
                    #endregion
#region Кафедра філософії та політології
                    new Department() {Name="Кафедра філософії та політології",Schedules=new List<Schedule>() {
                         new Schedule(){UserName="Гетало Тетяна Євгенівна",ApiId="352763" },
                         new Schedule(){UserName="Жеребятнікова Ірина Володимирівна",ApiId="341352" },
                         new Schedule(){UserName="Коннова Ніна Олексіївна",ApiId="352764"},
                         new Schedule(){UserName="Коротков Дмитро Сергійович",ApiId="334804"},
                         new Schedule(){UserName="Косова Юлія Вікторівна",ApiId="352525" },
                         new Schedule(){UserName="Кузь Олег Миколайович",ApiId="352765" },
                         new Schedule(){UserName="Потоцька Юлія Іванівна",ApiId="340546" },
                         new Schedule(){UserName="Стрижко Валентина Опанасівна",ApiId="352766" },
                         new Schedule(){UserName="Чешко Валентин Федорович",ApiId="352811" }
                     } }
#endregion
                }
            });
            f.Add(new Faculty()
            {
                Name = "Факультет фінансовий",
                ShortName = "ФФ",
                Departments = new List<Department>()
                {
                    new Department() {Name="Кафедра фінансів" },
                    new Department() {Name="Кафедра управління фінансовими послугами" },
                    #region Кафедра банківськоі справи
                    new Department() {Name="Кафедра банківськоі справи",Schedules=new List<Schedule>() {
                         new Schedule() {UserName="Азізова Катерина Михайлівна",ApiId="315091" },
                         new Schedule() {UserName="Безродна Олена Сергіївна",ApiId="352486" },
                         new Schedule() {UserName="Біляєва Вікторія Юріївна",ApiId="347473" },
                         new Schedule() {UserName="Бойко Наталя Олександрівна",ApiId="348062" },
                         new Schedule(){UserName="Вовк Вікторія Яківна",ApiId="357773" },
                         new Schedule(){UserName="Гонтар Дар'я Дмитрівна",ApiId="355154" },
                         new Schedule(){UserName="Григоренко Вікторія Миколаївна",ApiId="355155" },
                         new Schedule(){UserName="Губарєва Ірина Олегівна",ApiId="352254" },
                         new Schedule(){UserName="Дзеніс Вікторія Олександрівна",ApiId="352769" },
                         new Schedule(){UserName="Єремейчук Раїса Арсентіївна",ApiId="352255" },
                         new Schedule(){UserName="Жуков Владлен Валерійович",ApiId="297058" },
                         new Schedule(){UserName="Жукова Ольга Кузьмівна",ApiId="352770" },
                         new Schedule(){UserName="Заднєпровська Ганна Ігорівна",ApiId="344230" },
                         new Schedule(){UserName="Зуєва Олександра Валерівна",ApiId="335213" },
                         new Schedule(){UserName="Киркач Світлана Миколаївна",ApiId="357034" },
                         new Schedule(){UserName="Колодізєв Олег Миколайович",ApiId="340556" },
                         new Schedule(){UserName="Лебідь Олеся Вікторівна",ApiId="340558" },
                         new Schedule(){UserName="Максімова Марина Валеріївна",ApiId="344227" },
                         new Schedule(){UserName="Огородня Євгенія Миколаївна",ApiId="344228" },
                         new Schedule(){UserName="Омельченко Оксана Ігорівна",ApiId="329009"},
                         new Schedule(){UserName="Рац Ольга Миколаївна",ApiId="336064"},
                         new Schedule(){UserName="Середіна Ганна Вячеславівна",ApiId="352815"},
                         new Schedule(){UserName="Тисячна Юнна Сергіївна",ApiId="352484"},
                         new Schedule(){UserName="Тищенко Вікторія Федорівна",ApiId="357036"},
                         new Schedule(){UserName="Ус Юлія Володимирівна",ApiId="345434"},
                         new Schedule(){UserName="Хмеленко Олексій Володимирович",ApiId="297059"},
                         new Schedule(){UserName="Холодна Юлія Євгеніївна",ApiId="277113"},
                         new Schedule(){UserName="Чмутова Ірина Миколаївна",ApiId="335107"},
                         new Schedule(){UserName="Яременко Оксана Романівна",ApiId="335120"},
                     } },
#endregion
                    new Department() {Name="Кафедра оподаткування" }
                }
            });
            f.Add(new Faculty()
            {
                Name = "Факультет менеджменту і маркетингу",
                ShortName = "МіМ",
                Departments = new List<Department>()
                {
                    new Department() {Name="Кафедра менеджменту" },
                    new Department() {Name="Кафедра менеджменту і бізнесу" },
                    new Department() {Name="Кафедра економіки, організації та планування діяльності підприємства" },
                    #region Кафедра економіки і маркетингу
                    new Department() {Name="Кафедра економіки і маркетингу",Schedules=new List<Schedule>() {
                         new Schedule() {UserName="Алдохіна Наталя Іванівна",ApiId="352449" },
                         new Schedule() {UserName="Бєлікова Надія Володимирівна",ApiId="352402" },
                         new Schedule() {UserName="Бихова Олена Михайлівна",ApiId="353045" },
                         new Schedule() {UserName="Борисенко Марина Анатоліївна",ApiId="344391"},
                         new Schedule(){UserName="Гаврилов Павло Юрійович",ApiId="354067" },
                         new Schedule(){UserName="Гронь Олександра Вікторівна",ApiId="352528" },
                         new Schedule(){UserName="Лисиця Надія Михайлівна",ApiId="354590" },
                         new Schedule(){UserName="Лях Інна Сергіївна",ApiId="352750" },
                         new Schedule(){UserName="Мавріду Вікторія Юріївна",ApiId="352875" },
                         new Schedule(){UserName="Мироненко Ірина Ігорівна",ApiId="352877" },
                         new Schedule(){UserName="Нагаївська Дар'я Юріївна",ApiId="354103" },
                         new Schedule(){UserName="Орлов Петро Аркадійович",ApiId="354097" },
                         new Schedule(){UserName="Притиченко Тамара Іванівна",ApiId="352774" },
                         new Schedule(){UserName="Птащенко Олена Валеріївна",ApiId="352961" },
                         new Schedule(){UserName="Родіонов Сергій Олександрович",ApiId="357257" },
                         new Schedule(){UserName="Рожко Віктор Іванович",ApiId="352450" },
                         new Schedule(){UserName="Рубан Вячеслав Валерійович",ApiId="334820" },
                         new Schedule(){UserName="Руденко Юлія Василівна",ApiId="353046" },
                         new Schedule(){UserName="Селезньова Катерина Вячеславівна",ApiId="354077" },
                         new Schedule(){UserName="Тер-Карапетянц Юлія Миколаївна",ApiId="352876"},
                         new Schedule(){UserName="Холодний Геннадій Олександрович",ApiId="354091"},
                         new Schedule(){UserName="Шиян Наталія Іванівна",ApiId="348302"},
                         new Schedule(){UserName="Щербак Валерія Геннадіївна",ApiId="357759"},
                         new Schedule(){UserName="Щетинін Валерій Михайлович",ApiId="354874"},

                     }  },
#endregion
                }
            });
            f.Add(new Faculty()
            {
                Name = "Факультет економічної інформатики",
                ShortName = "ЕІ",
                Departments = new List<Department>()
                {
                    #region Кафедра інформаційних систем
                    new Department() {Name="Кафедра інформаційних систем",Schedules=new List<Schedule>() {
                         new Schedule() {UserName="Алексієв Володимир Олегович",ApiId="352974" },
                         new Schedule() {UserName="Анохін Віктор Миколайович",ApiId="335091" },
                         new Schedule() {UserName="Арзубов Микола Олексійович",ApiId="357453" },
                         new Schedule() {UserName="Безсонов Олександр Олександрович",ApiId="358250" },
                         new Schedule(){UserName="Беседовський Олексій Миколайович",ApiId="354067" },
                         new Schedule(){UserName="Біккузін Кирило Валерійович",ApiId="353049" },
                         new Schedule(){UserName="Бурдаєв Володимир Петрович",ApiId="352259" },
                         new Schedule(){UserName="Воропай Наталя Ігорівна",ApiId="340168" },
                         new Schedule(){UserName="Гаврилова Алла Андріївна",ApiId="352978" },
                         new Schedule(){UserName="Гниря Аліна Вікторівна",ApiId="352545" },
                         new Schedule(){UserName="Голубничий Дмитро Юрійович",ApiId="352425" },
                         new Schedule(){UserName="Гриньов Денис Валерійович",ApiId="354072" },
                         new Schedule(){UserName="Дорохов Олександр Васильович",ApiId="342922" },
                         new Schedule(){UserName="Євсеєв Сергій Петрович",ApiId="340599" },
                         new Schedule(){UserName="Задачин Віктор Михайлович",ApiId="357259" },
                         new Schedule(){UserName="Зінчук Антон Владиславович",ApiId="354105" },
                         new Schedule(){UserName="Знахур Людмила Володимирівна",ApiId="344463" },
                         new Schedule(){UserName="Знахур Сергій Вікторович",ApiId="354076" },
                         new Schedule(){UserName="Золотарьова Ірина Олександрівна",ApiId="354077" },
                         new Schedule(){UserName="Коваленко Андрій Анатолійович",ApiId="357156"},
                         new Schedule(){UserName="Конюшенко Ірина Григорівна",ApiId="354082"},
                         new Schedule(){UserName="Король Ольга Григорівна",ApiId="353052"},
                         new Schedule(){UserName="Коц Григорій Павлович",ApiId="352373"},
                         new Schedule(){UserName="Лагутін Максим Ігорович",ApiId="352388"},
                         new Schedule(){UserName="Лосєв Михайло Юрійович",ApiId="340607"},
                         new Schedule(){UserName="Макарова Ганна Валеріївна",ApiId="354101"},
                         new Schedule(){UserName="Мінухін Сергій Володимирович",ApiId="352969"},
                         new Schedule(){UserName="Огурцов Віталій Вячеславович",ApiId="357267"},
                         new Schedule(){UserName="Парфьонов Юрій Едуардович",ApiId="354098"},
                         new Schedule(){UserName="Плеханова Ганна Олегівна",ApiId="354099"},
                         new Schedule(){UserName="Плоха Олена Борисівна",ApiId="354100" },
                         new Schedule(){UserName="Поляков Андрій Олександрович",ApiId="357262" },
                         new Schedule(){UserName="Пономаренко Володимир Степанович",ApiId="357123" },
                         new Schedule(){UserName="Руденко Олег Григорійович",ApiId="357771" },
                         new Schedule(){UserName="Савін Юрій Вікторович",ApiId="357270"},
                         new Schedule(){UserName="Северин Валерій Петрович",ApiId="357760"},
                         new Schedule(){UserName="Сидоренко Іван Григорович",ApiId="354089"},
                         new Schedule(){UserName="Скорін Юрій Іванович",ApiId="352775"},
                         new Schedule(){UserName="Тарасов Олександр Васильович",ApiId="354090"},
                         new Schedule(){UserName="Ткачов Віталій Миколайович",ApiId="357159"},
                         new Schedule(){UserName="Ушакова Ірина Олексіївна",ApiId="340598"},
                         new Schedule(){UserName="Федорченко Володимир Миколайович",ApiId="297080"},
                         new Schedule(){UserName="Федько Віктор Васильович",ApiId="357266"},
                         new Schedule(){UserName="Фесенко Микола Сергійович",ApiId="348370"},
                         new Schedule(){UserName="Холодкова Анна Валеріївна",ApiId="306111"},
                         new Schedule(){UserName="Шматко Олександр Віталійович",ApiId="352265"},
                         new Schedule(){UserName="Щербаков Олександр Всеволодович",ApiId="354088"},
                         new Schedule(){UserName="Щербаков Олександр Всеволодович",ApiId="354088"},
                     } },
                    #endregion
                    #region Кафедра економічної кібернетики
                    new Department() {Name="Кафедра економічної кібернетики",Schedules=new List<Schedule>() {
                         new Schedule() {UserName="Баликов Олексій Георгійович",ApiId="358182" },
                         new Schedule() {UserName="Булкін Станіслав Михайлович",ApiId="357207" },
                         new Schedule() {UserName="Гвоздицький Віталій Сергійович",ApiId="335260" },
                         new Schedule() {UserName="Гур'янова Лідія Семенівна",ApiId="357271" },
                         new Schedule(){UserName="Івахненко Ольга Володимирівна",ApiId="357273" },
                         new Schedule(){UserName="Клебанова Тамара Семенівна",ApiId="272038" },
                         new Schedule(){UserName="Коваленко Катерина Сергіївна",ApiId="357228" },
                         new Schedule(){UserName="Мілевський Станіслав Валерійович",ApiId="352776" },
                         new Schedule(){UserName="Мілов Олександр Володимирович",ApiId="357484" },
                         new Schedule(){UserName="Непомнящий Вячеслав Володимирович",ApiId="335261" },
                         new Schedule(){UserName="Панасенко Оксана Володимирівна",ApiId="273974" },
                         new Schedule(){UserName="Полякова Ольга Юріївна",ApiId="352426" },
                         new Schedule(){UserName="Прокопович Світлана Валеріївна",ApiId="341364" },
                         new Schedule(){UserName="Сергієнко Олена Андріанівна",ApiId="352427" },
                         new Schedule(){UserName="Степуріна Світлана Олександрівна",ApiId="273973" },
                         new Schedule(){UserName="Трунова Тетяна Миколаївна",ApiId="348470" },
                         new Schedule(){UserName="Федорук Тарас Володимирович",ApiId="345194" },
                         new Schedule(){UserName="Чаговець Любов Олексіївна",ApiId="357150" },
                         new Schedule(){UserName="Чернова Наталя Леонідівна", ApiId="334831" },
                         new Schedule(){UserName="Чуйко Ірина Михайлівна",ApiId="353462"},
                         new Schedule(){UserName="Ястребова Ганна Сергіївна",ApiId="342907"},
                         new Schedule(){UserName="Яценко Роман Миколайович",ApiId="334832"},

                     }  },
                    #endregion
                    #region Кафедра технології, екології та безпеки життєдіяльності
                    new Department() {Name="Кафедра технології, екології та безпеки життєдіяльності",Schedules=new List<Schedule>() {
                         new Schedule() {UserName="Барбашин Віталій Валерійович",ApiId="342923" },
                         new Schedule() {UserName="Белікова Тетяна Борисівна",ApiId="357275" },
                         new Schedule() {UserName="Борисенко Оксана Миколаївна",ApiId="352884" },
                         new Schedule() {UserName="Буц Юрій Васильович",ApiId="321333" },
                         new Schedule(){UserName="Івашура Андрій Анатолійович",ApiId="357801" },
                         new Schedule(){UserName="Кобзін Володимир Григорович",ApiId="357274" },
                         new Schedule(){UserName="Коваленко Григорій Дмитрович",ApiId="352404" },
                         new Schedule(){UserName="Логвінков Сергій Михайлович",ApiId="328721" },
                         new Schedule(){UserName="Михайлова Євгенія Олександрівна",ApiId="341394" },
                         new Schedule(){UserName="Попенко Галина Степанівна",ApiId="315421" },
                         new Schedule(){UserName="Протасенко Ольга Федорівна",ApiId="352458" },
                         new Schedule(){UserName="Северинов Олександр Володимирович",ApiId="352282" },
                         new Schedule(){UserName="Скородумова Ольга Борисівна",ApiId="352428" },
                         new Schedule(){UserName="Чубук Володимир Васильович",ApiId="357321" },

                     }  },
                    #endregion
                    #region Кафедра комп'ютерних систем і технологій
                    new Department() {Name="Кафедра комп'ютерних систем і технологій" ,Schedules=new List<Schedule>() {
                         new Schedule() {UserName="Андрющенко Тетяна Юріївна",ApiId="352777" },
                         new Schedule() {UserName="Бережна Олена Борисівна",ApiId="340615" },
                         new Schedule() {UserName="Бондар Ірина Олександрівна",ApiId="352886" },
                         new Schedule() {UserName="Браткевич Вячеслав Вячеславович",ApiId="354587" },
                         new Schedule(){UserName="Гаврилов Володимир Петрович",ApiId="352827" },
                         new Schedule(){UserName="Гіковатий Володимир Михайлович",ApiId="340613" },
                         new Schedule(){UserName="Грабовський Євген Миколайович",ApiId="352778" },
                         new Schedule(){UserName="Євсєєв Олексій Сергійович",ApiId="277293" },
                         new Schedule(){UserName="Завгородня Ольга Сергіївна",ApiId="357276" },
                         new Schedule(){UserName="Климнюк Віктор Євгенович",ApiId="357782" },
                         new Schedule(){UserName="Молчанов Віктор Петрович",ApiId="354591" },
                         new Schedule(){UserName="Назарова Світлана Олександрівна",ApiId="357277" },
                         new Schedule(){UserName="Оленич Мирослава Миколаївна",ApiId="352235" },
                         new Schedule(){UserName="Пандорін Олександр Костянтинович",ApiId="340616" },
                         new Schedule(){UserName="Потрашкова Людмила Володимирівна",ApiId="352752" },
                         new Schedule(){UserName="Пушкар Олександр Іванович",ApiId="357089" },
                         new Schedule(){UserName="Сисоєва Юлія Анатоліївна",ApiId="352459" },
                         new Schedule(){UserName="Сібілєв Костянтин Сергійович",ApiId="358388" },
                         new Schedule(){UserName="Фомічова Ольга Володимирівна",ApiId="355159" },
                     }},
                    #endregion
#region Кафедра інформатики та комп'ютерної техніки
                    new Department() {Name="Кафедра інформатики та комп'ютерної техніки",Schedules=new List<Schedule>() {
                         new Schedule() {UserName="Бринза Наталя Олександрівна",ApiId="342914" },
                         new Schedule() {UserName="Вільхівська Ольга Володимирівна",ApiId="340617" },
                         new Schedule() {UserName="Власенко Наталія Володимирівна",ApiId="344241" },
                         new Schedule() {UserName="Гороховатський Олексій Володимирович",ApiId="354588" },
                         new Schedule(){UserName="Затхей Володимир Анатолійович",ApiId="356247" },
                         new Schedule(){UserName="Огурцова Катерина Вікторівна",ApiId="342952" },
                         new Schedule(){UserName="Передрій Олена Олегівна",ApiId="315027" },
                         new Schedule(){UserName="Степанов Валерій Павлович",ApiId="297086" },
                         new Schedule(){UserName="Тесленко Олег Володимирович",ApiId="328049" },
                         new Schedule(){UserName="Удовенко Сергій Григорович",ApiId="357772" }

                     } },
                    #endregion
                    #region Кафедра статистики та економічного прогнозування
                    new Department() {Name="Кафедра статистики та економічного прогнозування",Schedules=new List<Schedule>() {
                         new Schedule() {UserName="Аксьонова Ірина Вікторівна",ApiId="352475" },
                         new Schedule() {UserName="Бровко Ольга Іванівна",ApiId="333865" },
                         new Schedule() {UserName="Гольтяєва Людмила Анатоліївна",ApiId="352548" },
                         new Schedule() {UserName="Гриневич Людмила Володимирівна",ApiId="357926" },
                         new Schedule(){UserName="Дериховська Вікторія Ігорівна",ApiId="354332" },
                         new Schedule(){UserName="Зірко Олена Володимирівна",ApiId="352547" },
                         new Schedule(){UserName="Карпенко Аліна Станіславівна",ApiId="352793" },
                         new Schedule(){UserName="Лиска Олексій Григорович",ApiId="352429" },
                         new Schedule(){UserName="Мілевська Тетяна Сергіївна",ApiId="356505" },
                         new Schedule(){UserName="Мілевський Станіслав Валерійович",ApiId="352374" },
                         new Schedule(){UserName="Молдавська Олена Владиславівна",ApiId="330708" },
                         new Schedule(){UserName="Наумов Ігор Григорович",ApiId="352430" },
                         new Schedule(){UserName="Погасій Сергій Сергійович",ApiId="352795" },
                         new Schedule(){UserName="Раєвнєва Олена Валентинівна",ApiId="354095" },
                         new Schedule(){UserName="Свидло Ганна Ігорівна",ApiId="354328" },
                         new Schedule(){UserName="Сєрова Ірина Анатоліївна",ApiId="354096" },
                         new Schedule(){UserName="Шликова Вікторія Олександрівна",ApiId="353831" },
                         new Schedule(){UserName="Стрижиченко Костянтин Анатолійович",ApiId="352385" }
                     }  }
#endregion
                }
            });
            f.Add(new Faculty()
            {
                Name = "Факультет економіки і права",
                ShortName = "ЕП",
                Departments = new List<Department>()
                {
                    new Department() {Name="Кафедра управління персоналом та економіки праці" },
                    new Department() {Name="Кафедра економіки підприємств і менеджменту" },
                    new Department() {Name="Кафедра соціології та психології управління" },
                    new Department() {Name="Кафедра правового регулювання економіки" },
                    #region Кафедра державного управління
                    new Department() {Name="Кафедра державного управління, публічного адміністрування та регіональної економіки",Schedules=new List<Schedule>() {
                         new Schedule() {UserName="Аведян Людмила Йосипівна",ApiId="354913" },
                         new Schedule() {UserName="Безродна Олена Сергіївна",ApiId="335708" },
                         new Schedule() {UserName="Болотова Олена Олегівна",ApiId="357076" },
                         new Schedule() {UserName="Виноградська Аліна Миколаївна",ApiId="343018" },
                         new Schedule(){UserName="Власенко Тетяна Анатоліївна",ApiId="354914" },
                         new Schedule(){UserName="Гавкалова Наталія Леонідівна",ApiId="354915" },
                         new Schedule(){UserName="Гвазава Нана Гурамівна",ApiId="348422" },
                         new Schedule(){UserName="Гіковата Надія Костянтинівна",ApiId="244217" },
                         new Schedule(){UserName="Гордієнко Лариса Юріївна",ApiId="354916" },
                         new Schedule(){UserName="Грузд Марина Володимирівна",ApiId="354917" },
                         new Schedule(){UserName="Єрмоленко Оксана Олександрівна",ApiId="352832" },
                         new Schedule(){UserName="Золенко Альона Сергіївна",ApiId="319432" },
                         new Schedule(){UserName="Кабанець Анатолій Григорович",ApiId="355486" },
                         new Schedule(){UserName="Кайнова Тетяна Володимирівна",ApiId="357286" },
                         new Schedule(){UserName="Кизим Микола Олександрович",ApiId="357766" },
                         new Schedule(){UserName="Кожанова Євгенія Пилипівна",ApiId="352892" },
                         new Schedule(){UserName="Коновалов Максим Ігорович",ApiId="354918" },
                         new Schedule(){UserName="Коновалов Євген Ігорович",ApiId="343023" },
                         new Schedule(){UserName="Липко Олег Юрійович",ApiId="348317" },
                         new Schedule(){UserName="Мельник Вікторія Іванівна",ApiId="354919"},
                         new Schedule(){UserName="Моложавий Віктор Іванович",ApiId="347591"},
                         new Schedule(){UserName="Петряєв Олексій Олександрович",ApiId="354920"},
                         new Schedule(){UserName="Соболев Вадим Григорович",ApiId="352895"},
                         new Schedule(){UserName="Чистякова Анастасія Вадимівна",ApiId="348374"},
                         new Schedule(){UserName="Шумська Ганна Миколаївна",ApiId="354921"},
                         new Schedule(){UserName="Шутєєва Ольга Юріївна",ApiId="335701"},
                         new Schedule(){UserName="Яндола Кристина Олександрівна",ApiId="343028"}
                     } },
#endregion
                    new Department() {Name="Кафедра управління соціальними комунікаціями" },
                      new Department() {Name="Кафедра педагогіки та іноземноі філології" },
                }
            });
            f.Add(new Faculty()
            {
                Name = "Факультет міжнародних економічних відносин",
                ShortName = "МЕВ",
                Departments = new List<Department>()
                {
                    new Department() {Name="Кафедра міжнародної економіки та менеджменту зовнішньоекономічної діяльності" },
                    new Department() {Name="Кафедра політичної економії" },
#region Кафедра іноземних мов та перекладу
                    new Department() {Name="Кафедра іноземних мов та перекладу",Schedules=new List<Schedule>() {
                         new Schedule() {UserName="Алексєєва Марія Ігорівна",ApiId="315221" },
                         new Schedule() {UserName="Безугла Ірина Валентинівна",ApiId="354065" },
                         new Schedule() {UserName="Гончарова Жанна Миколаївна",ApiId="341277" },
                         new Schedule() {UserName="Григор'єв Максим Вікторович",ApiId="354104" },
                         new Schedule(){UserName="Давидова Жанна Вадимівна",ApiId="336075" },
                         new Schedule(){UserName="Євдокімова-Лисогор Леся Анатоліївна",ApiId="354074" },
                         new Schedule(){UserName="Єрастова-Михалусь Інна Борисівна",ApiId="354074" },
                         new Schedule(){UserName="Іваніга Орина Валеріївна",ApiId="314908" },
                         new Schedule(){UserName="Ігнатенко Лариса Олександрівна",ApiId="340637" },
                         new Schedule(){UserName="Камишнікова Яна Сергіївна",ApiId="307719" },
                         new Schedule(){UserName="Кобринець Ольга Станіславівна",ApiId="307718" },
                         new Schedule(){UserName="Коваль Аліна Сергіївна",ApiId="352834" },
                         new Schedule(){UserName="Колбіна Тетяна Василівна",ApiId="290422" },
                         new Schedule(){UserName="Курінна Світлана Дмитрівна",ApiId="352908" },
                         new Schedule(){UserName="Лукашова Людмила Василівна",ApiId="319066" },
                         new Schedule(){UserName="Луніна Марина Леонідівна",ApiId="352735" },
                         new Schedule(){UserName="Лютвієва Ярослава Павлівна",ApiId="352511" },
                         new Schedule(){UserName="Михайлова Людмила Зіновіївна",ApiId="305968" },
                         new Schedule(){UserName="Нікішина Анжела Володимирівна",ApiId="335080" },
                         new Schedule(){UserName="Олексенко Олена Олексіївна",ApiId="352336"},
                         new Schedule(){UserName="Прус Наталія Олексіївна",ApiId="354754"},
                         new Schedule(){UserName="Решетняк Ірина Олексіївна",ApiId="283055"},
                         new Schedule(){UserName="Савицька Лариса Володимирівна",ApiId="354594"},
                        new Schedule(){UserName="Сальтевська Марина Юхимівна",ApiId="334839"},
                         new Schedule(){UserName="Тарасенко Сергій Євгенович",ApiId="353099"},
                         new Schedule(){UserName="Хачатрян Єва Левонівна",ApiId="355171"},
                         new Schedule(){UserName="Хоменко Вікторія Володимирівна",ApiId="352907"},
                         new Schedule(){UserName="Ципіна Діана Савеліївна",ApiId="355206"},
                         new Schedule(){UserName="Черниш Людмила Миколаївна",ApiId="329114"},
                         new Schedule(){UserName="Черниш Людмила Миколаївна",ApiId="329114"}
                     } },
#endregion
                    new Department() {Name="Кафедра туризму" },
                }
            });
            f.Add(new Faculty()
            {
                Name = "Факультет підготовки іноземних громадян",
                ShortName = "ІГ",
                Departments = new List<Department>()
                {
                    new Department() {Name="Кафедра українознавства і мовної підготовки іноземних громадян" },
                    new Department() {Name="Кафедра природничих наук та технології" },
                    new Department() {Name="Кафедра фізвиховання та спорту" }
                }
            });
            foreach (var item in f)
            {
                context.Faculties.Add(item);
            }
        }
    }
}