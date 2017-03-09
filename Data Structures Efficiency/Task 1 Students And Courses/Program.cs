namespace StudentsAndCourses
{
    using System;
    using System.Collections.Generic;
    using HomeworkHelpers;

    /// <summary>
    /// A text file students.txt holds information about students and their courses in the following format:
    ///     Using SortedDictionary{K,T} print the courses in alphabetical order and for each of them prints 
    ///     the students ordered by family and then by name
    /// </summary>
    public class Program
    {
        private static readonly HomeworkHelper helper = new HomeworkHelper();

        // Students file is set to be created in the working directory when the project compiles
        private static string pathToStudents = "students.txt";

        private static void Main()
        {
            helper.ConsoleMio.Setup();

            helper.ConsoleMio.PrintHeading("Task 1 Information About The Students And Their Courses ");

            SortedDictionary<string, SortedSet<Student>> information =
                StudentFileParsr.GiveMeAllTheThings(pathToStudents);

            if (information == null)
            {
                helper.ConsoleMio.WriteLine("Ne stana", ConsoleColor.Red);
                Environment.Exit(1);
            }

            foreach (var course in information.Keys)
            {
                helper.ConsoleMio.Write("{0}: ", ConsoleColor.DarkGreen, course)
                    .WriteLine(string.Join(", ", information[course]), ConsoleColor.DarkGray);
            }
        }
    }
}
