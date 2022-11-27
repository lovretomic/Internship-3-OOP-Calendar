
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
    var input = Console.ReadLine();
    if (int.TryParse(input, out var a) is true)
        return int.Parse(input);
    else
    {
        Console.WriteLine("## Pogrešan unos! Unesi novu vrijednost.");
        getInput();
    }
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
            Console.WriteLine(eventData.Id);                                                // zaokruži na jednu decimalu VVV
            Console.WriteLine($"{eventData.Title} - {eventData.Location} - Ended before {(today - eventData.EndDate).TotalDays} days - Duration: {(eventData.EndDate - eventData.BeginDate).TotalHours} hours");
            eventData.PrintAttendees(people);
            eventData.PrintAttendance(eventData.Id, people);
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
            /*
            foreach(var person in people)
                if(eventData.Emails.Contains(person.Email))
                    person.Attendance[eventData.Id.ToString()] = true;  // Pretpostavka da su svi prisutni
            */
            Console.WriteLine(eventData.Id);
            Console.WriteLine($"{eventData.Title} - {eventData.Location} - Ends in {eventData.EndDate - today}");
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

            // TU SI STAO --> TREBAŠ ZA SVE UNESENE MAILOVE KOJI ODGOVARAJU EVENTU POSTAVITI ATTENDANCE NA FALSE
            Console.WriteLine("Unesi emailove osoba koje nisu prisutne odvojene razmakom.");
            var inputEmails = Console.ReadLine().Split(" ").ToList();
            for(var i = 0; i < people.Count(); i++)
            {
                foreach(var presence in people[i].Attendance)
                {
                    if (inputEmails.Contains(people[i].Email))
                    {
                        people[i].Attendance[inputId] = false;
                    }
                    else
                    {
                        // Treba dodati poruku
                    }
                }
            }
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
        if (eventData.BeginDate > today) // Sigurni smo da je datum kraja iza datuma početka
        {
            counter++;
            Console.WriteLine(eventData.Id);
            var eventLength = eventData.EndDate - eventData.BeginDate;
            var eventLengthInHours = Math.Round(eventLength.TotalHours, 1);
            if(eventData.BeginDate.Day - today.Day == 1)
                Console.WriteLine($"{eventData.Title} - {eventData.Location} - Starts in {eventData.BeginDate.Day - today.Day} day - Length in hours: {eventLengthInHours}");
            else
                Console.WriteLine($"{eventData.Title} - {eventData.Location} - Starts in {eventData.BeginDate.Day - today.Day} days - Length in hours: {eventLengthInHours}");
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
            foreach (var Event in events)
                Console.WriteLine($"{Event.Title} - {Event.Id}");
            var condition = true;
            do
            {
                Console.WriteLine("\nUnesi id eventa kojeg želiš izbrisati ili 0 ako zelis povratak na glavni izbornik."); //## Provjeri (vi/ti) u ostatku koda
                var inputId = Console.ReadLine();
                if (String.Equals(inputId, "0")) returnToMain(0);
                foreach (var Event in events)
                {
                    if (String.Equals(Event.Id.ToString(), inputId))
                    {
                        events.Remove(Event);       //## Dodaj potvrdu
                        Console.WriteLine("Event je izbrisan.");
                        condition = false;
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

    DateTime dateResult;    // Za korištenje DateTime.TryParse()
    bool condition;         // Varijabla koja prati uspješnost pojedinog unosa
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

    events.Add(new Event(newEventTitle, newEventLocation, newEventBeginDate, newEventEndDate, newEventEmails));

    Console.WriteLine("\nEvent je dodan!");
    returnToMain(1);
}

Main();