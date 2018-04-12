using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Load.Mapper.SemesterFormat;

namespace Load.Services.Types
{
    public class DayFormatCalculationRules
    {
        private readonly List<string> _notesValue = new List<string>
        {
            "ДА",
            "ДЕ",
            "П",
            "Т",
            "КР",
            "КП"
        };

        public bool Note(string s)
        {
            return _notesValue.Contains(s.ToUpper());
        }

        public double GetSemesterConsults(double critOne, string note, double t) // round
        {
            double res;
            string n = note.ToUpper();
            if (t > 0 && (n == "Н" || n == "В"))
            {
                double r = critOne * 0.02;
                res = Math.Ceiling(r);
            }
            else
                res = 0;
            return res;
        }

        public double GetExamConsultsFs(string es, double countOfThreads, double critOne)
        {
            double res;
            String e = es.ToUpper();
            if (e == "ДЕ")
                res = 2 * countOfThreads;
            else if (e == "ЕКЗ")
                res = 2 * critOne;
            else res = 0;
            return res;
        }
        public double GetExamConsultsSs(string ex, double critOne)
        {
            double res;
            string e = ex.ToUpper();
            if (e == "ЕКЗ" || e == "ДЕ")
                res = critOne * 2;
            else
                res = 0;
            return res;
        }
        public double GetCheckingTests(double qSt, string note, double qCr, double qH, double tH, double t) //round
        {
            double res;
            string n = note.ToUpper();

            if ((n == "Н" || n == "В") && t > 0)
            {
                double t1 = (tH / (qH / qCr)) * 0.05;
                double t2 = Math.Round(t1, 2) * qSt;
                res = Math.Ceiling(t2);
            }
            else
                res = 0;

            return res;
        }

        public double GetProjects(String st, double coOFstud, double kr_kp)
        {
            double res;
            String s = st.ToUpper();
            if (s == "КР" || s == "КП")
                res = coOFstud * kr_kp;
            else
                res = 0;
            return res;
        }
        public double GetEvaluation(String zalik, double critOne)
        {
            double res;
            String s = zalik.ToUpper();
            if (s == "ЗАЛІК")
                res = critOne * 2;
            else
                res = 0;
            return res;
        }
        public double GetExam(String ex, double coOfstud)
        {
            double res;
            String s = ex.ToUpper();
            if (s == "ЕКЗ")
            {
                double f = coOfstud * 0.25;
                res = Math.Ceiling(f);
            }
            else
                res = 0;
            return res;
        }
        public double GetPracticePreparation(double tH, double total, string note, double qStud, double practice)
        {
            double r1, r2;
            string n = note.ToUpper();

            if (tH > 0 && total == 0 && n == "П")
                r1 = Math.Ceiling(qStud * practice);
            else
                r1 = 0;

            if (tH > 0 && n == "НДП")
                r2 = qStud * 2;
            else
                r2 = 0;
            double res = r1 + r2;

            return res;
        }

        public double GetExamParticipation(String coursePr, String exam, double quanOfDek, double coOfstud)
        {
            double r1;
            String c = coursePr.ToUpper();
            if (c == "ДР")
                r1 = quanOfDek * 0.5 * coOfstud;
            else
                r1 = 0;
            double r2;
            String e = exam.ToString();
            if (e == "ДЕ")
                r2 = quanOfDek * 3;
            else
                r2 = 0;
            double res = r1 + r2;
            return res;
        }
        public double GetStateExam(String exam, double coOfstud)
        {
            double r;
            String e = exam.ToUpper();
            if (e == "ДЕ")
                r = Math.Ceiling(coOfstud * 0.5);
            else
                r = 0;
            return r;
        }
        public double GetDimplomsManagement(String courseP, double coOfstud, double kr_kp_dr)
        {
            double r;
            String c = courseP.ToUpper();
            if (c == "ДР")
                r = coOfstud * kr_kp_dr;
            else
                r = 0;
            return r;
        }
        public double GetTotal(DayFormatSemester cur)
        {
            double res = cur.Lectures + cur.Practices + cur.Labs + cur.ExamConsults + cur.SemesterConsults
                        + cur.CheckingTests + cur.Projects + cur.Evaluation + cur.Exam +
                        +cur.PracticePreparation + cur.ExamParticipation + cur.StateExam + cur.DiplomsManagement + cur.Other;
            return res;
        }
        public double GetActive(DayFormatSemester cur)
        {
            double res = cur.Lectures + cur.Practices + cur.Labs + cur.ExamConsults + cur.ExamParticipation;
            return res;
        }
        public double GetBonus(String lan, double active)
        {
            double r;
            String l = lan.ToUpper();
            if (l == "А")
                r = active * 0.3;
            else
                r = 0;
            return r;
        }

    }

}
