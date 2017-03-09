namespace StudentsAndCourses
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public class StudentFileParsr
    {
        private const int MAGIC_NUMBER = 38498;

        public static SortedDictionary<string, SortedSet<Student>> GiveMeAllTheThings(string path)
        {
            string[] data;
            try
            {
                data = File.ReadAllLines(path);
            }
            catch (IOException)
            {
                return null;
            }

            var result = new SortedDictionary<string, SortedSet<Student>>();

            if (data.Length > MAGIC_NUMBER)
            {
                ParallelForeach(data, result);
            }
            else
            {
                foreach (var line in data)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    string[] splitInformation = line.Split('|');
                    string fname = splitInformation[0].Trim();
                    string lname = splitInformation[1].Trim();
                    string course = splitInformation[2].Trim();

                    if (!result.ContainsKey(course))
                    {
                        result[course] = new SortedSet<Student>();
                    }

                    result[course].Add(new Student
                    {
                        FirstName = fname,
                        LastName = lname,
                        Course = course
                    });
                }
            }

            return result;
        }

        private static void ParallelForeach(string[] data, SortedDictionary<string, SortedSet<Student>> result)
        {
            Parallel.ForEach(
                data, line =>
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        return;
                    }

                    string[] splitInformation = line.Split('|');
                    string fname = splitInformation[0].Trim();
                    string lname = splitInformation[1].Trim();
                    string course = splitInformation[2].Trim();

                    Student next = new Student
                    {
                        FirstName = fname,
                        LastName = lname,
                        Course = course
                    };

                    if (!result.ContainsKey(course))
                    {
                        lock (result)
                        {
                            if (!result.ContainsKey(course))
                            {
                                result[course] = new SortedSet<Student> { next };
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            result[course].Add(next);
                        }
                        catch (KeyNotFoundException)
                        {
                            // Problem solved...
                        }
                        catch (NullReferenceException)
                        {
                            // Another solved problem...
                        }
                    }
                });
        }
    }
}