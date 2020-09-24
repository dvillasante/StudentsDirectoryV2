using System;

namespace Models
{
    public class Student
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Dob { get; set; }
        public string Sex { get; set; }
        public bool Enabled { get; set; }
        
        public string LastUpdated { get; set; }
        public Student()
        {

        }
        public Student(int id, string firstName, string lastName, string dob, string sex)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Dob = dob;
            Sex = sex;
        }

    }
}
