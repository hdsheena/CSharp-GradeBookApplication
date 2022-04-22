using System;
using System.Collections.Generic;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {

        public RankedGradeBook(string name): base(name)
        {
            Name = name;
            Type = Enums.GradeBookType.Ranked;
            Students = new List<Student>();


        }

        /// <summary>
        /// So this was fun but I don't think it's actually needed or useful at alll
        /// </summary>
        /// <returns>class average</returns>
        public double GetClassAverage()
        {
            var allStudentsPoints = 0d;

            foreach (var student in Students)
            {
                student.LetterGrade = GetLetterGrade(student.AverageGrade);
                student.GPA = GetGPA(student.LetterGrade, student.Type);

                Console.WriteLine("{0} ({1}:{2}) GPA: {3}.", student.Name, student.LetterGrade, student.AverageGrade, student.GPA);
                allStudentsPoints += student.AverageGrade;
            }
            return allStudentsPoints / Students.Count;
        }

        //get all average grades
        public List<double> AllAverageGrades()
        {
            List<double> AllAverageGradesSorted = new();
            foreach (var student in Students)
            {
                AllAverageGradesSorted.Add(student.AverageGrade);
            }
            AllAverageGradesSorted.Sort();
            return AllAverageGradesSorted;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            List<double> AllAverageGradesSorted = AllAverageGrades();
            //how many students are there 100
            //what's this student's rank among those students 34
            //what ranks get what grades 0-20 = F, 21-40 = D, 41-60 = C, 61-80 = B, 81-100 = A
            //what's this student's grade D
            if (Students.Count < 5) {
                throw new InvalidOperationException("Ranked grading requires a minumum of 5 students");
            }
            double fifthOfStudents = Students.Count / 5;
            //rank student among other students
            for (var i = 0; i < AllAverageGradesSorted.Count; i++)
            {
                double grade = AllAverageGradesSorted[i];
           
                Console.WriteLine("Grade", grade);
                Console.WriteLine("Index", i);
            }

            if (averageGrade >= 90)
                return 'A1';
            else if (averageGrade >= 80)
                return 'B';
            else if (averageGrade >= 70)
                return 'C';
            else if (averageGrade >= 60)
                return 'D';
            else
                return 'F2';
        }
    }
}
