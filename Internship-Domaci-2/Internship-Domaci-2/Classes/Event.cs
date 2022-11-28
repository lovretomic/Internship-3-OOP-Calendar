using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Internship_Domaci_2.Classes
{
    public class Event
    {
        public Guid Id { get; private set; }
        public string Title;
        public string Location;
        public DateTime BeginDate;
        public DateTime EndDate;
        public List<string> Emails { get; private set; }

        public Event(string title, string location, string beginDate, string endDate, string emails) {
            Id = Guid.NewGuid();
            Title = title;
            Location = location;
            BeginDate = Convert.ToDateTime(beginDate);
            EndDate = Convert.ToDateTime(endDate);
            Emails = new List<string>(emails.Split(" "));
        }

        public void PrintAttendees(List<Person> people)
        {
            var counter = 0;
            var output = "";

            foreach (var person in people)
            {
                if (person.Attendance[Id.ToString()] is true)
                {
                    if (counter != 0) output += ", ";
                    output += person.Email;
                    counter++;
                }
            }
            if (counter == 0) output = "Nema upisanih osoba.";
            Console.WriteLine(output);   
        }

        public void PrintAllAttendees()
        {
            var counter = 0;
            var output = "";

            foreach (var email in Emails)
            {
                if (counter != 0) output += ", ";
                output += email;
                counter++;
            }
            if (counter == 0) output = "Nema upisanih osoba.";
            Console.WriteLine(output);
        }

        public void PrintAttendance(List<Person> people)
        {
            var counterTrue = 0;
            var counterFalse = 0;
            var attendanceTrue = "Prisutni: ";
            var attendanceFalse = "Odsutni: ";
            foreach (var person in people)
                if(Emails.Contains(person.Email))
                    if (person.Attendance[Id.ToString()] is true)
                    {
                        attendanceTrue += $"\n* {person.Name} {person.Surname} ({person.Email}) ";
                        counterTrue++;
                    }
                    else
                    {
                        attendanceFalse += $"\n* {person.Name} {person.Surname} ({person.Email}) ";
                        counterFalse++;
                    }
            if (counterTrue == 0) attendanceTrue += "nije bilo prisutnih";
            if (counterFalse == 0) attendanceFalse += "nije bilo odsutnih";
            Console.WriteLine(attendanceTrue);
            Console.WriteLine(attendanceFalse);
        }
    }
}
