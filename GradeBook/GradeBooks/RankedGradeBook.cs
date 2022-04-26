using System;
using System.Collections.Generic;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {

        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            Name = name;
            Type = Enums.GradeBookType.Ranked;
            Students = new List<Student>();
            IsWeighted = isWeighted;


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
            List<double> AllAverageGradesSorted = new List<double>();
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
            int listPosition = 0;
            //how many students are there 100
            //what's this student's rank among those students 34
            //what ranks get what grades 0-20 = F, 21-40 = D, 41-60 = C, 61-80 = B, 81-100 = A
            //what's this student's grade D
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Ranked grading requires a minumum of 5 students");
            }
            double fifthOfStudents = Students.Count / 5;
            //rank student among other students
            for (var i = 0; i < AllAverageGradesSorted.Count; i++)
            {
                double grade = AllAverageGradesSorted[i];

                Console.WriteLine("Grade");
                Console.WriteLine(grade);
                Console.WriteLine("Index");
                Console.WriteLine(i);
                if (averageGrade > grade)
                {
                    Console.WriteLine("Grade is larger, keep checking");
                }
                else
                {
                    Console.WriteLine("Return this index as where the grade is in the list");
                    listPosition = i;
                    break;
                }
            }
            if (listPosition >= Students.Count - fifthOfStudents)
            {
                return 'A';
            }
            else if (listPosition >= Students.Count - (2 * fifthOfStudents))
                return 'B';
            else if (listPosition >= Students.Count - (3 * fifthOfStudents))
                return 'C';
            else if (listPosition >= Students.Count - (4 * fifthOfStudents))
                return 'D';
            else
                return 'F';
        }
        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStatistics();
        }
        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}
