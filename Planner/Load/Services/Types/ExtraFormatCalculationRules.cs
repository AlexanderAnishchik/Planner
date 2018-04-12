using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Load.Helpers;
using Load.Mapper.SemesterFormat;

namespace Load.Services.Types
{
    public class ExtraFormatCalculationRules
    {
        public double GetWrittenWorks(string ex, double quanOfSt)
        {
            double res;
            string e = ex.ToUpper();

            if (e == "ДІ")
                res = MathHelper.MidpointRound(quanOfSt * 0.5);
            else
                res = 0;
            return res;
        }

        public double GetExamConsultsFs(string ex1, string ex2, double quanOfth)
        {
            double res, r1, r2;
            string e1 = ex1.ToUpper();
            string e2 = ex2.ToUpper();
            if (e1 == "ЕКЗ")
                r1 = 2 * quanOfth;
            else
                r1 = 0;
            //
            if (e2 == "ЕКЗ")
                r2 = 2 * quanOfth;
            else
                r2 = 0;
            //
            res = MathHelper.MidpointRound(r1 + r2);
            return res;
        }
        public double GetExamConsultsSs(string ex, double normaKR, double quanOfGr)
        {
            double res;
            string e1 = ex.ToUpper();

            if (e1 == "ДІ")
                res = 2 * normaKR * quanOfGr;
            else
                res = 0;

            res = MathHelper.MidpointRound(res);
            return res;
        }

        public double GetProjects(string KR, double quanOfStud, double KR_DR_DI)
        {
            double res;
            string kr = KR.ToUpper();
            if (kr == "КП" || kr == "КР")
                res = MathHelper.MidpointRound(quanOfStud * KR_DR_DI);
            else
                res = 0;
            return res;
        }
        public double GetEvaluation(string eval, double quanOfGroup)
        {
            string e = eval.ToUpper();
            if (e == "ЗАЛІК")
                return MathHelper.MidpointRound(2 * quanOfGroup);
            else
                return 0;
        }
        public double GetTotalFs(ExtraFormatSemester c)
        {
            double res = c.Lectures + c.Practices + c.Labs + c.SemesterConsults + c.ExamConsults +
                c.WrittenWorks + c.AnalyticalWorks + c.Projects + c.Evaluation + c.OralExams +
                c.WrittenExams;
            return res;
        }
        public double GetActiveFs(ExtraFormatSemester c)
        {
            double res = c.Lectures + c.Practices + c.Labs + c.ExamConsults + c.Evaluation + c.OralExams;
            return res;
        }
        public double GetTotalSs(ExtraFormatSemester c)
        {
            double res = c.Lectures + c.Practices + c.Labs + c.SemesterConsults + c.ExamConsults +
                c.WrittenWorks + c.AnalyticalWorks + c.Projects + c.Evaluation + c.OralExams +
                c.WrittenExams + c.CheckingTests + c.DiplomManagement + c.DekParticipation + c.CheckWriteWorks +
                c.Protection;
            return res;
        }
        public double GetActiveSs(ExtraFormatSemester c)
        {
            double res = c.Lectures + c.Practices + c.Labs + c.ExamConsults + c.Evaluation + c.OralExams
                + c.DekParticipation;
            return res;
        }
        public double GetOralExams(string ex1, string ex2, double quanOfStud)
        {
            double res, r1, r2;
            string e1 = ex1.ToUpper();
            string e2 = ex2.ToUpper();
            //
            if (e1 == "ЕКЗ")
                r1 = MathHelper.MidpointRound(0.33 * quanOfStud);
            else
                r1 = 0;
            //
            if (e2 == "ЕКЗ")
                r2 = MathHelper.MidpointRound(0.33 * quanOfStud);
            else
                r2 = 0;
            //
            double r = MathHelper.MidpointRound(r1) + MathHelper.MidpointRound(r2);
            res = MathHelper.MidpointRound(r);
            return res;
        }
        public double GetExamParticipation(string ex1, double quanOfth, string S_KR, double quanOfSt)
        {
            double res, r1, r2;
            string e1 = ex1.ToUpper();
            string kr = S_KR.ToUpper();
            if (e1 == "ДІ")
                r1 = 3 * quanOfth * 4;
            else
                r1 = 0;
            //
            if (kr == "ДР")
                r2 = 0.5 * quanOfSt * 4;
            else
                r2 = 0;
            double r = MathHelper.MidpointRound(r1) + MathHelper.MidpointRound(r2);
            res = r;
            return res;
        }
        public double GetCheckingTests(string kr, double quanOfstud)
        {
            double res;
            if (kr == "к.р.")
                res = MathHelper.MidpointRound(quanOfstud * 0.33);
            else
                res = 0;
            return res;
        }
    }
}
