
using Internship_Domaci_2.Classes;

// Predefinirani eventi i osobe;
var events = new List<Event>()
{
    new Event("Event1", "Location1", "25-11-2022", "31-12-2022", "tomiclovre05@gmail.com drugimail@gmail.com"),
    new Event("Event2", "Location2", "15-07-2005", "03-05-2020", "tomiclovre05@gmail.com drugimail@gmail.com"),
    new Event("Event3", "Location3", "15-07-2005", "15-07-2006", "tomiclovre05@gmail.com drugimail@gmail.com"),
    new Event("Event4", "Location4", "15-07-2005", "15-07-2006", "tomiclovre05@gmail.com drugimail@gmail.com"),
    new Event("Event5", "Location5", "30-12-2022 13:54:09", "31-12-2022 21:12:47", "tomiclovre05@gmail.com drugimail@gmail.com"),
    new Event("Event6", "Location6", "13-01-2023 13:54:09", "18-01-2023 21:12:47", "tomiclovre05@gmail.com drugimail@gmail.com")
};

var people = new List<Person>()
{
    new Person("Ime1", "Prezime1", "Email1"),
    new Person("Ime2", "Prezime2", "Email2"),
    new Person("Ime3", "Prezime3", "Email3"),
    new Person("Ime4", "Prezime4", "Email4"),
    new Person("Ime5", "Prezime5", "Email5"),
    new Person("Ime6", "Prezime6", "Email6"),
    new Person("Ime7", "Prezime7", "Email7"),
    new Person("Ime8", "Prezime8", "Email8"),
    new Person("Ime9", "Prezime9", "Email9"),
    new Person("Ime10", "Prezime10", "Email10"),
    new Person("Lovre", "Tomić", "tomiclovre05@gmail.com"),
    new Person("Druga", "Osoba", "drugimail@gmail.com")
};

foreach (var person in people)
    foreach (var singleEvent in events)
        if (singleEvent.Emails.Contains(person.Email))
            person.Attendance.Add(singleEvent.Id.ToString(), true);
        else
            person.Attendance.Add(singleEvent.Id.ToString(), false);

int getInput()
{
    string input;
    int a;

    do
    {
        input = Console.ReadLine();
        if (int.TryParse(input, out a) is true)
            return int.Parse(input);
        else
            Console.WriteLine("## Pogrešan unos! Unesi novu vrijednost.");
    } while (int.TryParse(input, out a) is false);

    return 0;
}
void Main()
{
    Console.Clear();
    Console.WriteLine("--- MAIL KALENDAR ---");
    Console.WriteLine("1 - Aktivni eventi");
    Console.WriteLine("2 - Nadolazeći eventi");
    Console.WriteLine("3 - Eventi koji su završili");
    Console.WriteLine("4 - Kreireaj Event");
    Console.WriteLine("0 - Izađi iz programa");

    switch(getInput())
    {
        case 1:
            printActiveEvents();
            break;
        case 2:
            printFutureEvents();
            break;  
        case 3:
            printPastEvents();
            break;
        case 4:
            createEvent();
            break;
        case 0:
            return;
            break;
    }
}

void returnToMain(int modifier) // modifier == 0 (bez provjere) / modifier != 0 (sa provjerom)
{
    if(modifier != 0)
    {
        Console.WriteLine("Pritisni ENTER za povratak na glavni izbornik.");
        Console.ReadLine();
    }
    Main();
}

void printPastEvents()
{
    Console.Clear();
    Console.WriteLine("--- ZAVRŠENI EVENTI ---\n");
    var today = DateTime.Today;
    Console.WriteLine("--------------------");

    var counter = 0;
    foreach (var eventData in events)
    {
        if (eventData.EndDate < today)
        {
            counter++;
            Console.WriteLine(eventData.Id);
            Console.WriteLine($"{eventData.Title} - {eventData.Location} - Ended before {Math.Round((today - eventData.EndDate).TotalDays, 1)} days - Duration: {(eventData.EndDate - eventData.BeginDate).TotalHours} hours");
            eventData.PrintAttendees(people);
            eventData.PrintAttendance(people);
            Console.WriteLine("--------------------");
        }
    }

    if (counter == 0)
    {
        Console.WriteLine("Nema završenih evenata.\n--------------------");
        returnToMain(1);
    }

    returnToMain(1);
}
void printActiveEvents()
{
    Console.Clear();
    Console.WriteLine("--- AKTIVNI EVENTI ---\n");
    var today = DateTime.Today;
    Console.WriteLine("--------------------");

    var counter = 0;
    foreach (var eventData in events) {
        if(eventData.BeginDate < today && eventData.EndDate > today)
        {
            counter++;
            Console.WriteLine(eventData.Id);
            if(Math.Round((eventData.EndDate - today).TotalDays, 1) is 1)
                Console.WriteLine($"{eventData.Title} - {eventData.Location} - Ends in {Math.Round((eventData.EndDate - today).TotalDays, 1)} day");
            else
                Console.WriteLine($"{eventData.Title} - {eventData.Location} - Ends in {Math.Round((eventData.EndDate - today).TotalDays, 1)} days");
            eventData.PrintAttendees(people);
            Console.WriteLine("--------------------");
        }
    }

    if (counter == 0)
    {
        Console.WriteLine("Nema aktivnih evenata.\n--------------------");
        returnToMain(1);
    }

    Console.WriteLine("\nFunkcije");
    Console.WriteLine("1 - Zabilježi nepristunosti");
    Console.WriteLine("0 - Povratak na glavni izbornik");
    switch(getInput())
    {
        case 1:
            var condition = true;
            var inputIdChecked = "";
            var inputId = "";
            do
            {
                Console.WriteLine("\nUnesi id eventa ili 0 za povratak na glavni izbornik.");
                inputId = Console.ReadLine();
                if (String.Equals(inputId, "0")) returnToMain(0);
                foreach (var Event in events)
                {
                    if (String.Equals(Event.Id.ToString(), inputId))
                    {
                        inputIdChecked = inputId;
                        condition = false;
                    }
                }
                if (condition is true)
                {
                    Console.WriteLine("Event nije pronađen. Želiš li upisati novi id? (D/N)");
                    var inputStr = Console.ReadLine().ToUpper();
                    if (String.Equals(inputStr, "N")) returnToMain(1);
                }
            } while (condition is true);

            Console.WriteLine("Unesi emailove osoba koje nisu prisutne odvojene razmakom.");
            var inputEmails = Console.ReadLine().Split(" ").ToList();
            var incorrectEmails = new List<string>();
            foreach (var singleEvent in events)
                if(String.Compare(singleEvent.Id.ToString(), inputId) is 0)
                    foreach(var email in inputEmails)
                        if (!singleEvent.Emails.Contains(email)) incorrectEmails.Add(email);

            if (incorrectEmails.Count() is not 0)
            {
                counter = 0;
                var output = "Nisu pronađeni sljedeći emailovi: ";
                foreach (var incorrectEmail in incorrectEmails)
                {
                    if (counter != 0) output += ", ";
                    output += incorrectEmail;
                    counter++;
                }
                Console.WriteLine(output);
            }

            for (var i = 0; i < people.Count(); i++)
            {
                foreach(var presence in people[i].Attendance)
                {
                    if (inputEmails.Contains(people[i].Email))
                    {
                        people[i].Attendance[inputId] = false;
                    }
                }
            }
            Console.WriteLine("\nSvi potvrđeni emailovi su izbrisani.");
            returnToMain(1);
            break;
        case 0:
            returnToMain(0);
            break;
    }
}
void printFutureEvents()
{
    Console.Clear();
    Console.WriteLine("--- NADOLAZEĆI EVENTI ---\n");
    var today = DateTime.Today;
    Console.WriteLine("--------------------");

    var counter = 0;
    foreach (var eventData in events)
    {
        if (eventData.BeginDate > today)
        {
            counter++;
            Console.WriteLine(eventData.Id);
            var eventLength = eventData.EndDate - eventData.BeginDate;
            var eventLengthInHours = Math.Round(eventLength.TotalHours, 1);
            if(Math.Round((eventData.BeginDate - today).TotalDays, 1) == 1)
                Console.WriteLine($"{eventData.Title} - {eventData.Location} - Starts in {Math.Round((eventData.BeginDate - today).TotalDays, 1)} day - Length in hours: {eventLengthInHours}");
            else
                Console.WriteLine($"{eventData.Title} - {eventData.Location} - Starts in {Math.Round((eventData.BeginDate - today).TotalDays, 1)} days - Length in hours: {eventLengthInHours}");
            eventData.PrintAllAttendees();
            Console.WriteLine("--------------------");
        }
    }

    if (counter == 0)
    {
        Console.WriteLine("Nema nadolazecih evenata.\n--------------------");
        returnToMain(1);
    }

    Console.WriteLine("\nFunkcije");
    Console.WriteLine("1 - Izbrisi event");
    Console.WriteLine("2 - Ukloni osobe s eventa");
    Console.WriteLine("0 - Povratak na glavni izbornik");
    switch (getInput())
    {
        case 1:
            Console.Clear();
            Console.WriteLine("--- BRISANJE NADOLAZECEG EVENTA ---\n");
            foreach (var singleEvent in events)
                if(singleEvent.BeginDate > today)
                    Console.WriteLine($"{singleEvent.Title} - {singleEvent.Id}");
            var condition = true;
            do
            {
                Console.WriteLine("\nUnesi id eventa kojeg želiš izbrisati ili 0 ako zelis povratak na glavni izbornik.");
                var inputId = Console.ReadLine();
                if (String.Equals(inputId, "0")) returnToMain(0);
                foreach (var Event in events)
                {
                    if (String.Equals(Event.Id.ToString(), inputId))
                    {
                        condition = false;
                        Console.WriteLine("Jesi li siguran da zelis izbrisati taj event? (D/N)");
                        var input = Console.ReadLine().ToUpper();
                        if (String.Equals(input, "D"))
                        {
                            events.Remove(Event);
                            Console.WriteLine("Event je izbrisan.");
                            foreach(var person in people)
                                person.Attendance[inputId] = false;

                        }
                        else returnToMain(1);
                        break;
                    }
                }
                if(condition is true)
                {
                    Console.WriteLine("Event nije pronađen. Želiš li upisati novi id? (D/N)");
                    var inputStr = Console.ReadLine().ToUpper();
                    if(String.Equals(inputStr, "N")) condition = false;
                }

            } while (condition is true);
            
            returnToMain(1);
            break;
        case 2:
            condition = true;
            do
            {
                var inputId = "";
                void inputIdFunction()
                {
                    Console.WriteLine("\nUnesi id eventa kojeg želiš urediti ili 0 za povratak na glavni izbornik.");
                    inputId = Console.ReadLine();
                    if (String.Equals(inputId, "0")) returnToMain(0);

                    foreach (var singleEvent in events)
                        if (String.Compare(singleEvent.Id.ToString(), inputId) is 0) { condition = false; break; }

                    if (condition is true)
                    {
                        Console.WriteLine("Event nije pronađen. Želiš li upisati novi id? (D/N)");
                        var inputStr = Console.ReadLine().ToUpper();
                        if (String.Equals(inputStr, "N")) returnToMain(1);
                        else inputIdFunction();
                    }
                }

                inputIdFunction();

                Console.WriteLine("\nUnesi emailove osoba koje želiš izbrisati odvojene jednim razmakom.");
                var inputEmails = Console.ReadLine().Split(" ").ToList();
                var incorrectEmails = new List<string>();
                foreach (var singleEvent in events)
                    if (String.Compare(singleEvent.Id.ToString(), inputId) is 0)
                    {
                        foreach (var email in inputEmails)
                            if (singleEvent.Emails.Contains(email))
                            {
                                singleEvent.Emails.Remove(email);
                                foreach(var person in people)
                                    if(String.Equals(person.Email, email))
                                        person.Attendance[inputId] = false;
                            }
                            else incorrectEmails.Add(email);
                        break;
                    }
                if(incorrectEmails.Count() is not 0)
                {
                    counter = 0;
                    var output = "Nisu pronađeni sljedeći emailovi: ";
                    foreach (var incorrectEmail in incorrectEmails)
                    {
                            if (counter != 0) output += ", ";
                            output += incorrectEmail;
                            counter++;
                    }
                    Console.WriteLine(output);
                }

            } while (condition is true);
            Console.WriteLine("\nSvi su potvrdeni mailovi izbrisani.");
            returnToMain(1);
            break;
        case 0:
            returnToMain(0);
            break;
    }
}

void createEvent()
{
    Console.Clear();
    string newEventTitle, newEventLocation, newEventBeginDate, newEventEndDate, newEventEmails;
    Console.WriteLine("--- Unos eventa ---");

    Console.WriteLine("Unesi naziv eventa.");
    newEventTitle = Console.ReadLine();
    Console.Clear();
    Console.WriteLine("--- Unos eventa ---");
    Console.WriteLine($"NAZIV EVENTA: {newEventTitle}");
    
    Console.WriteLine("Unesi lokaciju eventa.");
    newEventLocation = Console.ReadLine();
    Console.Clear();
    Console.WriteLine("--- Unos eventa ---");
    Console.WriteLine($"NAZIV EVENTA: {newEventTitle}");
    Console.WriteLine($"LOKACIJA EVENTA: {newEventLocation}");

    DateTime dateResult;
    bool condition;
    do
    {
        condition = true;
        Console.WriteLine("Unesi datum početka eventa. (DD-MM-GGGG)");
        newEventBeginDate = Console.ReadLine();
        if (DateTime.TryParse(newEventBeginDate, out dateResult) is false)
        {
            Console.WriteLine("## Pogrešan unos! Unesi u pravilnom formatu.");
            condition = false;
        }
        else if (DateTime.Parse(newEventBeginDate) < DateTime.Now)
        {
            Console.WriteLine("## Pogrešan unos! Uneseni datum mora biti u budućnosti.");
            condition = false;
        }
    } while (condition is false);
    Console.Clear();
    Console.WriteLine("--- Unos eventa ---");
    Console.WriteLine($"NAZIV EVENTA: {newEventTitle}");
    Console.WriteLine($"LOKACIJA EVENTA: {newEventLocation}");
    Console.WriteLine($"DATUM POČETKA EVENTA: {newEventBeginDate}");

    do
    {
        condition = true;
        Console.WriteLine("Unesi datum kraja eventa. (DD-MM-GGGG)");
        newEventEndDate = Console.ReadLine();
        if (DateTime.TryParse(newEventEndDate, out dateResult) is false)
        {
            Console.WriteLine("## Pogrešan unos! Unesi u pravilnom formatu.");
            condition = false;
        }
        else if (DateTime.Parse(newEventEndDate) < DateTime.Parse(newEventBeginDate))
        {
            Console.WriteLine("## Pogrešan unos! Uneseni datum mora biti nakon datuma početka.");
            condition = false;
        }
    } while (condition is false);
    Console.Clear();
    Console.WriteLine("--- Unos eventa ---");
    Console.WriteLine($"NAZIV EVENTA: {newEventTitle}");
    Console.WriteLine($"LOKACIJA EVENTA: {newEventLocation}");
    Console.WriteLine($"DATUM POČETKA EVENTA: {newEventBeginDate}");
    Console.WriteLine($"DATUM KRAJA EVENTA: {newEventEndDate}");

    Console.WriteLine("Unesi e-mailove pozvanih osoba. (odvoji jednim razmakom)");
    newEventEmails = Console.ReadLine();

    var newEventEmailsList = new List<string>(newEventEmails.Split(" ").ToList());
    foreach (var person in people)
        foreach (var attendingEventId in person.Attendance.Keys)
            foreach (var singleEvent in events)
                if (String.Equals(singleEvent.Id.ToString(), attendingEventId) && newEventEmails.Contains(person.Email))
                    if (!(singleEvent.EndDate < DateTime.Parse(newEventBeginDate) || singleEvent.BeginDate > DateTime.Parse(newEventEndDate)))
                    {
                        newEventEmailsList.Remove(person.Email);
                        Console.WriteLine($"Osoba ({person.Name}, {person.Email}) vec sudjeluje u drugom eventu.");
                    }

    events.Add(new Event(newEventTitle, newEventLocation, newEventBeginDate, newEventEndDate, String.Join(" ", newEventEmailsList.ToArray())));

    Console.WriteLine("\nEvent je dodan!");
    returnToMain(1);
}

Main();