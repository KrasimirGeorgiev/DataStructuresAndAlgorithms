namespace StudentsAndCourses
{
    using System;

    public class Student : IComparable<Student>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Course { get; set; }

        public override string ToString()
        {
            return $"{this.FirstName} {this.LastName}";
        }

        public int CompareTo(Student other)
        {
            int comparison = string.Compare(this.LastName, other.LastName, StringComparison.CurrentCulture);
            if (comparison == 0)
            {
                comparison = string.Compare(this.FirstName, other.FirstName, StringComparison.CurrentCulture);
                if (comparison == 0)
                {
                    comparison = string.Compare(this.Course, other.Course, StringComparison.CurrentCulture);
                }
            }

            return comparison;
        }
    }
}