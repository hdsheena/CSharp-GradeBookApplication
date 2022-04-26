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
            if (Students.Count < 5) {
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
            else if (listPosition >= Students.Count - (2*fifthOfStudents))
                return 'B';
            else if (listPosition >= Students.Count - (3 * fifthOfStudents))
                return 'C';
            else if (listPosition >= Students.Count - (4 * fifthOfStudents))
                return 'D';
            else
                return 'F';
        }
        public virtual void CalculateStatistics()
        {
            var allStudentsPoints = 0d;
            var campusPoints = 0d;
            var statePoints = 0d;
            var nationalPoints = 0d;
            var internationalPoints = 0d;
            var standardPoints = 0d;
            var honorPoints = 0d;
            var dualEnrolledPoints = 0d;

            foreach (var student in Students)
            {
                student.LetterGrade = GetLetterGrade(student.AverageGrade);
                student.GPA = GetGPA(student.LetterGrade, student.Type);

                Console.WriteLine("{0} ({1}:{2}) GPA: {3}.", student.Name, student.LetterGrade, student.AverageGrade, student.GPA);
                allStudentsPoints += student.AverageGrade;

                switch (student.Enrollment)
                {
                    case EnrollmentType.Campus:
                        campusPoints += student.AverageGrade;
                        break;
                    case EnrollmentType.State:
                        statePoints += student.AverageGrade;
                        break;
                    case EnrollmentType.National:
                        nationalPoints += student.AverageGrade;
                        break;
                    case EnrollmentType.International:
                        internationalPoints += student.AverageGrade;
                        break;
                }

                switch (student.Type)
                {
                    case StudentType.Standard:
                        standardPoints += student.AverageGrade;
                        break;
                    case StudentType.Honors:
                        honorPoints += student.AverageGrade;
                        break;
                    case StudentType.DualEnrolled:
                        dualEnrolledPoints += student.AverageGrade;
                        break;
                }
            }

            // #todo refactor into it's own method with calculations performed here
            Console.WriteLine("Average Grade of all students is " + (allStudentsPoints / Students.Count));
            if (campusPoints != 0)
                Console.WriteLine("Average for only local students is " + (campusPoints / Students.Where(e => e.Enrollment == EnrollmentType.Campus).Count()));
            if (statePoints != 0)
                Console.WriteLine("Average for only state students (excluding local) is " + (statePoints / Students.Where(e => e.Enrollment == EnrollmentType.State).Count()));
            if (nationalPoints != 0)
                Console.WriteLine("Average for only national students (excluding state and local) is " + (nationalPoints / Students.Where(e => e.Enrollment == EnrollmentType.National).Count()));
            if (internationalPoints != 0)
                Console.WriteLine("Average for only international students is " + (internationalPoints / Students.Where(e => e.Enrollment == EnrollmentType.International).Count()));
            if (standardPoints != 0)
                Console.WriteLine("Average for students excluding honors and dual enrollment is " + (standardPoints / Students.Where(e => e.Type == StudentType.Standard).Count()));
            if (honorPoints != 0)
                Console.WriteLine("Average for only honors students is " + (honorPoints / Students.Where(e => e.Type == StudentType.Honors).Count()));
            if (dualEnrolledPoints != 0)
                Console.WriteLine("Average for only dual enrolled students is " + (dualEnrolledPoints / Students.Where(e => e.Type == StudentType.DualEnrolled).Count()));
        }
    }
}
