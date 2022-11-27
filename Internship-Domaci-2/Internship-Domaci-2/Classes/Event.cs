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

        public void PrintAttendees()
        {
            var counter = 0;
            var output = "";
            foreach (var Email in Emails)
            {
                if (counter != 0) output += ", ";
                output += Email;
                counter++;
            }
            Console.WriteLine(output);
        }

        public void PrintAttendance(Guid eventId, List<Person> people)
        {
            var counterTrue = 0;
            var counterFalse = 0;
            var attendanceTrue = "Prisutni: ";
            var attendanceFalse = "Odsutni: ";
            foreach (var person in people)
                if (person.Attendance[eventId.ToString()] is true)
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
