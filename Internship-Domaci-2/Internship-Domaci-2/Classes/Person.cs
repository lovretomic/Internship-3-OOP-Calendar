using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Domaci_2.Classes
{
    public class Person
    {
        public string Name;
        public string Surname;
        public string Email { get; } 
        public Dictionary<string, bool> Attendance { get; private set; } = new Dictionary<string, bool>();

        public Person(string name, string surname, string email) {
            Name = name;
            Surname = surname;
            Email = email;
        }
    }
}
