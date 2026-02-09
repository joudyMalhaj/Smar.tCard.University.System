using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text.Json;


static class FileManager
{
    private const string StudentsFile = "students.json";
    private const string FacultyFile = "faculty.json";
    private const string CardsFile = "cards.json";
    private const string TransactionsFile = "transactions.json";
    private const string AttendanceFile = "attendance.json";

    private static readonly JsonSerializerOptions JsonOpt = new JsonSerializerOptions
    {
        WriteIndented = true,
        IncludeFields = true
    };

    public static void LoadFirstData()
    {
        if (!File.Exists(StudentsFile))
        {
            List<Student> students = new List<Student>
            {
                new Student("S01","Ali"){ RegisteredCourses = new List<string>{"CPE100","SE400"} },
                new Student("S02","Omar"){ RegisteredCourses = new List<string>{"CPE100","NES200"} },
                new Student("S03","Reem"){ RegisteredCourses = new List<string>{"NES200","CIS300","SE400"} },
                new Student("S04","Maher"){ RegisteredCourses = new List<string>{"CPE100","SE400"} }
            };

            using (FileStream fs = new FileStream(StudentsFile, FileMode.Create))
            {
                JsonSerializer.Serialize(fs, students, JsonOpt);
            }
        }

        if (!File.Exists(FacultyFile))
        {
            List<FacuiltyMember> faculty = new List<FacuiltyMember>
            {
                new FacuiltyMember("F01","Sami"){ TaughtCourses = new List<string>{"CPE100","CIS300"} },
                new FacuiltyMember("F02","Eman"){ TaughtCourses = new List<string>{"NES200","SE400"} }
            };

            using (FileStream fs = new FileStream(FacultyFile, FileMode.Create))
            {
                JsonSerializer.Serialize(fs, faculty, JsonOpt);
            }
        }

        if (!File.Exists(CardsFile))
        {
            List<Cards> cards = new List<Cards>
            {
                new Cards(10,80,"unblocked","faculty member","F02"),
                new Cards(20,110,"unblocked","student","S02"),
                new Cards(30,95,"blocked","student","S03"),
                new Cards(40,160,"unblocked","student","S04")
            };

            using (FileStream fs = new FileStream(CardsFile, FileMode.Create))
            {
                JsonSerializer.Serialize(fs, cards, JsonOpt);
            }
        }
    }

    public static List<T> LoadList<T>(string file)
    {
        if (!File.Exists(file)) return new List<T>();
        string json = File.ReadAllText(file);
        return JsonSerializer.Deserialize<List<T>>(json, JsonOpt) ?? new List<T>();
    }

    public static void SaveList<T>(string file, List<T> data)
    {
        string json = JsonSerializer.Serialize(data, JsonOpt);
        File.WriteAllText(file, json);
    }

    public static List<Student> LoadStudents() => LoadList<Student>(StudentsFile);
    public static List<FacuiltyMember> LoadFaculty() => LoadList<FacuiltyMember>(FacultyFile);
    public static List<Cards> LoadCards() => LoadList<Cards>(CardsFile);
    public static List<TransactonRecords> LoadTransactions() => LoadList<TransactonRecords>(TransactionsFile);
    public static List<AttendanceRecords> LoadAttendance() => LoadList<AttendanceRecords>(AttendanceFile);

    public static void SaveStudents(List<Student> s) => SaveList(StudentsFile, s);
    public static void SaveFaculty(List<FacuiltyMember> f) => SaveList(FacultyFile, f);
    public static void SaveCards(List<Cards> c) => SaveList(CardsFile, c);
    public static void SaveTransactions(List<TransactonRecords> t) => SaveList(TransactionsFile, t);
    public static void SaveAttendance(List<AttendanceRecords> a) => SaveList(AttendanceFile, a);
}
abstract class CardHolder
{
    private string userID;
    private string name;
    public string UserID { set { userID = value; } get { return userID; } }
    public string Name { set { name = value; } get { return name; } }

    public CardHolder(string userID, string name)
    {
        this.userID = userID;
        this.name = name;
    }
}

class Student : CardHolder
{
    public Student(string userID, string name) : base(userID, name) { }

    public List<string> registeredCourses = new List<string>();
    public List<string> RegisteredCourses
    { set { registeredCourses = value; } get { return registeredCourses; } }

    public override string ToString()
    {
        return "UserID: " + this.UserID + " Name: " + this.Name +
               " registeredCourses : " + string.Join(", ", registeredCourses);
    }

    public List<string> GetRegisteredCourses() { return registeredCourses; }
}

class FacuiltyMember : CardHolder
{
    public FacuiltyMember(string userID, string name) : base(userID, name) { }

    public List<string> taughtCourses = new List<string>();
    public List<string> TaughtCourses
    { set { taughtCourses = value; } get { return taughtCourses; } }

    public List<string> GetTaughtCourses() { return taughtCourses; }

    public override string ToString()
    {
        return "UserID: " + this.UserID + " Name: " + this.Name +
               " TaughtCourses : " + string.Join(", ", taughtCourses);
    }
}


class Cards
{
    private string userID;
    private int cardNumber;
    private double balance;
    private string status;
    private string type;

    
    public Cards() { }//ضرورية عشان ال 

    public Cards(int c, double b, string s, string t, string u)
    {
        userID = u;
        cardNumber = c;
        balance = b;
        status = s;
        type = t;
    }

    public string UserID { set { userID = value; } get { return userID; } }
    public int CardNumber { set { cardNumber = value; } get { return cardNumber; } }
    public double Balance { set { balance = value; } get { return balance; } }
    public string Status { set { status = value; } get { return status; } }
    public string Type { set { type = value; } get { return type; } }

    public string GetStatus() { return status; }
}


class TransactonRecords
{
    private string transactionID;
    private int cardNumber;
    private string type;
    private object amount;

    public string TransactionID { set { transactionID = value; } get { return transactionID; } }
    public int CardNumber { set { cardNumber = value; } get { return cardNumber; } }
    public string Type { set { type = value; } get { return type; } }
    public object Amount { set { amount = value; } get { return amount; } }
    public override string ToString()
    {
        return "TransactionID: " + this.transactionID + " | CardNumber: " + this.cardNumber +
               " | Type: " + this.type + " | Amount: " + this.amount;
    }

    public TransactonRecords(string transactionID, int cardNumber, string type, object amount)
    {
        this.transactionID = transactionID;
        this.cardNumber = cardNumber;
        this.type = type;
        this.amount = amount;
    }
}

class Cafetiera
{
    private int select;
    private double price;

    public int Select { set { select = value; } get { return select; } }
    public double Price { set { price = value; } get { return price; } }
    public Cafetiera()
    {

        Console.WriteLine("         " + " ITEM " + "\t" + "PRICE");
        Console.WriteLine("    1:   " + " Steak" + "\t" + "8JD");
        Console.WriteLine("    2:  " + " Soup  " + "\t" + "2JD");
        Console.WriteLine("    3:" + " Sandwich" + "\t" + "3JD");
        Console.WriteLine("    4:   " + "Salad " + "\t" + "4JD");
        Console.WriteLine("    5:  " + " Tea  " + "\t" + "2JD");
        Console.WriteLine("    6:  " + "Juice " + "\t" + "3JD");
        Console.WriteLine("    7:  " + "Cake  " + "\t" + "5JD");
        Console.WriteLine("    8:   " + "Water" + "\t" + "1JD");

    }
    public int getcost()
    {
        switch (select)
        {
            case 1: return 8; break;
            case 2: return 2; break;
            case 3: return 3; break;
            case 4: return 4; break;
            case 5: return 2; break;
            case 6: return 3; break;
            case 7: return 5; break;
            case 8: return 1; break;
            default: { Console.WriteLine("item not valid :"); return 0; break; }
        }
    }


}

class AttendanceRecords
{
    private string courseID;
    private string date;

    public string CourseID { set { courseID = value; } get { return courseID; } }
    public string Date { set { date = value; } get { return date; } }

    public List<string> attendances = new List<string>();
    public List<string> Attendances
    { set { attendances = value; } get { return attendances; } }
    public override string ToString()
    {
        return "CourseID: " + CourseID +
               " | Date: " + Date +
               " | Students: " + string.Join(", ", Attendances);
    }

}

class BusTrack
{
    private int trackID;
    private string origin;
    private string destination;
    private double cost;

    public int TrackID { get { return trackID; } set { trackID = value; } }
    public string Origin { get { return origin; } set { origin = value; } }
    public string Destination { get { return destination; } set { destination = value; } }
    public double Cost { set { cost = value; } get { return cost; } }

    public BusTrack()
    {
        Console.WriteLine("Track 1-> Origin: main gate , Destination: Northen buildings, Cost: 3JD");
        Console.WriteLine("Track 2-> Origin: main gate , Destination: Southern buildings, Cost: 4JD");
        Console.WriteLine("Track 3-> Origin: main gate , Destination: Library, Cost: 5JD\n");
    }

    public double GetCost()
    {
        switch (Destination)
        {
            case "NB": return 3; break;
            case "SB": return 4; break;
            case "L": return 5; break;
            default: { Console.WriteLine("Track not valid :"); return 0; break; }
        }
    }
}

class CarParking
{
    public CarParking()
    {
        Console.WriteLine("Welcome to the university parking\n These are the booking times, prices, and available offers:");
        Console.WriteLine("1st hour: 5 JD\n2nd hour: 4 JD\n3rd hour: 3 JD\n4th hour: 2 JD\n5th hour: 1 JD\nAbove 5 hours for free.\n");
    }
    public int CalculateFee(int hours)
    {
        if (hours > 0)
        {
            switch (hours)
            {
                case 1: return 5;
                case 2: return 9;
                case 3: return 12;
                case 4: return 14;
                case 5: return 15;
                default: return 15;
            }
        }
        else
        {
            Console.WriteLine("invalid hours.\n");
            return 0;
        }
    }
}
class Program
{
    public static void CardIssue()
    {
        Console.Write("Enter Card Number: ");
        if (!int.TryParse(Console.ReadLine(), out int n))
        {
            Console.WriteLine("Invalid card number format.");
            return;
        }

        List<Cards> cards = FileManager.LoadCards();

        if (cards.Exists(c => c.CardNumber == n))
        {
            Console.WriteLine("Card Number already exists.");
            return;
        }

        Console.Write("Enter User ID for new card: ");
        string u = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(u))
        {
            Console.WriteLine("User ID cannot be empty.");
            return;
        }

       
        string u2 = "";
        while (true)
        {
            Console.WriteLine("Select Card Type:");
            Console.WriteLine("[1] Student");
            Console.WriteLine("[2] Faculty Member");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine()?.Trim();

            if (choice == "1")
            {
                u2 = "student";
                break;
            }
            else if (choice == "2")
            {
                u2 = "faculty member";
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
            }
        }

        Cards c = new Cards(n, 50, "unblocked", u2, u);
        cards.Add(c);
        FileManager.SaveCards(cards);

        Console.WriteLine("Card issued successfully.");
    }


    public static void BlockCards()
    {
        Console.Write("Enter Card Number to block: ");
        int num = Convert.ToInt32(Console.ReadLine());

        List<Cards> cards = FileManager.LoadCards();
        Cards card = cards.Find(c => c.CardNumber == num);

        if (card == null)
        {
            Console.WriteLine("Card not found.");
            return;
        }

        card.Status = "blocked";
        FileManager.SaveCards(cards);
        Console.WriteLine("Card blocked successfully.");
    }

    public static void UnBlockCards()
    {
        Console.Write("Enter Card Number to unblock: ");
        int num = Convert.ToInt32(Console.ReadLine());

        List<Cards> cards = FileManager.LoadCards();
        Cards card = cards.Find(c => c.CardNumber == num);

        if (card == null)
        {
            Console.WriteLine("Card not found.");
            return;
        }

        card.Status = "unblocked";
        FileManager.SaveCards(cards);
        Console.WriteLine("Card unblocked successfully.");
    }

    public static void ViewAllTransactions()
    {

        List<TransactonRecords> t = new List<TransactonRecords>();
        t = FileManager.LoadTransactions();
        if (t.Count != 0)
        {
            foreach (TransactonRecords c in t)
            {
                Console.WriteLine(c);
            }
        }
        else
        {
            Console.WriteLine("No Transactions Available");
        }
    }
    public static void ViewALLcards()
    {
        List<Cards> cards = new List<Cards>();
        cards = FileManager.LoadCards();
        foreach (Cards c in cards)
        {
            Console.WriteLine("UserID: " + c.UserID + " | CardNumber: " + c.CardNumber + " | Balance: " + c.Balance + " | Status: " + c.Status + " | Type: " + c.Type);
        }
    }

    public static void PayForCafetira(int c)
    {
        Cafetiera buy = new Cafetiera();
        int e = 1;
        double totPrice = 0;

        while (e != 0)
        {
            Console.WriteLine("Enter an item number or 0 to end order:");
            e = Convert.ToInt32(Console.ReadLine());

            if (e == 0) break;

            buy.Select = e;
            totPrice += buy.getcost();
        }

        Console.WriteLine("The total price is: " + totPrice + " JD");

        List<Cards> cards = FileManager.LoadCards();
        Cards card = cards.Find(s => s.CardNumber == c);

        if (card == null)
        {
            Console.WriteLine("Card not found.");
            return;
        }

        if (card.Balance < totPrice)
        {
            Console.WriteLine("Not enough balance.");
            return;
        }

        card.Balance -= totPrice;
        FileManager.SaveCards(cards);

        List<TransactonRecords> t = FileManager.LoadTransactions();
        Console.Write("Enter transaction ID: ");
        string tid = Console.ReadLine();

        t.Add(new TransactonRecords(tid, c, "payment", totPrice));
        FileManager.SaveTransactions(t);

        Console.WriteLine("Payment successful.");
    }

    public static void RecordAttendance(int c)
    {
        List<Cards> cards = FileManager.LoadCards();
        Cards card = cards.Find(s => s.CardNumber == c);

        if (card == null)
        {
            Console.WriteLine("Card not found.");
            return;
        }

        string uid = card.UserID;

        List<Student> students = FileManager.LoadStudents();
        Student s1 = students.Find(s => s.UserID == uid);

        if (s1 == null)
        {
            Console.WriteLine("Student not found.");
            return;
        }

        Console.WriteLine(s1);

        Console.Write("Enter course ID: ");
        string courseID = Console.ReadLine();

        Console.Write("Enter date (YYYY-MM-DD): ");
        string date = Console.ReadLine();

        List<AttendanceRecords> attendanceList = FileManager.LoadAttendance();
        AttendanceRecords attendance =
            attendanceList.Find(a => a.CourseID == courseID && a.Date == date);

        if (attendance == null)
        {
            attendance = new AttendanceRecords
            {
                CourseID = courseID,
                Date = date,
                Attendances = new List<string>()
            };

            attendance.Attendances.Add(uid);
            attendanceList.Add(attendance);
            FileManager.SaveAttendance(attendanceList);

            Console.WriteLine("Attendance recorded successfully.");
        }
        else
        {
            if (attendance.Attendances.Contains(uid))
            {
                Console.WriteLine("Attendance already recorded.");
                return;
            }

            attendance.Attendances.Add(uid);
            FileManager.SaveAttendance(attendanceList);

            Console.WriteLine("Attendance recorded successfully.");
        }

        List<TransactonRecords> t = FileManager.LoadTransactions();
        Console.Write("Enter transaction ID: ");
        string tid = Console.ReadLine();

        t.Add(new TransactonRecords(tid, c, "attendance", "N/A"));
        FileManager.SaveTransactions(t);
    }


    public static void RechageCard(int c)
    {
        List<Cards> cards = FileManager.LoadCards();
        Cards card = cards.Find(s => s.CardNumber == c);
        if (card != null)
        {
            Console.WriteLine("Enter amount to recharge:");
            double amount = Convert.ToDouble(Console.ReadLine());
            card.Balance += amount;
            Console.WriteLine("Card recharged successfully. New balance: " + card.Balance + "JD");
            FileManager.SaveCards(cards);
        }
        else
        {
            Console.WriteLine("Card not found.");
        }
    }
    public static void PayforbusRide(int c)
    {
        BusTrack track = new BusTrack();

        Console.Write("Enter destination code (NB / SB / L): ");
        track.Destination = Console.ReadLine();

        double cost = track.GetCost();

        List<Cards> cards = FileManager.LoadCards();
        Cards card = cards.Find(s => s.CardNumber == c);

        if (card == null)
        {
            Console.WriteLine("Card not found.");
            return;
        }

        if (card.Balance < cost)
        {
            Console.WriteLine("Insufficient balance.");
            return;
        }

        card.Balance -= cost;
        FileManager.SaveCards(cards);

        List<TransactonRecords> t = FileManager.LoadTransactions();
        Console.Write("Enter transaction ID: ");
        string tid = Console.ReadLine();

        t.Add(new TransactonRecords(tid, c, "payment", track.Destination));
        FileManager.SaveTransactions(t);

        Console.WriteLine("Payment successful.");
    }


    public static void GenerateReport(int c)
    {
        List<Cards> cards = FileManager.LoadCards();
        Cards card = cards.Find(s => s.CardNumber == c);

        if (card == null)
        {
            Console.WriteLine("Card not found.");
            return;
        }

        string uid = card.UserID;

        List<FacuiltyMember> faculty = FileManager.LoadFaculty();
        FacuiltyMember f1 = faculty.Find(f => f.UserID == uid);

        if (f1 == null)
        {
            Console.WriteLine("Faculty member not found.");
            return;
        }

        Console.WriteLine(f1);

        Console.Write("Enter course ID: ");
        string courseId = Console.ReadLine();

        Console.Write("Enter date (YYYY-MM-DD): ");
        string date = Console.ReadLine();

        List<AttendanceRecords> attendanceList = FileManager.LoadAttendance();
        AttendanceRecords attendance =
            attendanceList.Find(a => a.CourseID == courseId && a.Date == date);

        if (attendance == null)
        {
            Console.WriteLine("No attendance records found.");
            return;
        }

        Console.WriteLine(attendance);
    }

    public static void ViewTransactionHistory(int c)//for student
    {
        List<TransactonRecords> transactions = FileManager.LoadTransactions();
        List<TransactonRecords> userTransactions = transactions.FindAll(t => t.CardNumber == c);//find all transactions for this card number(note find returns first match only ,but findall returns all matches)
        if (userTransactions.Count == 0)
        {
            Console.WriteLine("No transaction records found.");
            return;
        }
        foreach (var transaction in userTransactions)
        {
            Console.WriteLine(transaction);
        }
    }

    public static void PayForParking(int c)
    {
        CarParking parking = new CarParking();

        Console.Write("Enter number of hours parked: ");
        int hours = Convert.ToInt32(Console.ReadLine());

        int fee = parking.CalculateFee(hours);

        List<Cards> cards = FileManager.LoadCards();
        Cards card = cards.Find(s => s.CardNumber == c);

        if (card == null)
        {
            Console.WriteLine("Card not found.");
            return;
        }

        if (card.Balance < fee)
        {
            Console.WriteLine("Insufficient balance.");
            return;
        }

        card.Balance -= fee;
        FileManager.SaveCards(cards);

        List<TransactonRecords> t = FileManager.LoadTransactions();
        Console.Write("Enter transaction ID: ");
        string tid = Console.ReadLine();

        t.Add(new TransactonRecords(tid, c, "payment", fee));
        FileManager.SaveTransactions(t);

        Console.WriteLine("Payment successful.");
    }


    public static void studentMenu(int cardNumber)
    {
        int n = 1;
        while (n != 0)
        {
            Console.WriteLine("Welcome Student! Please choose an option:");
            Console.WriteLine("[1] recharge Card");
            Console.WriteLine("[2] Record lecture attendance");
            Console.WriteLine("[3] Pay for Cafeteria ");
            Console.WriteLine("[4] Pay for bus ride ");
            Console.WriteLine("[5] View Transaction History");

            Console.WriteLine("[6] Logout\n");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    RechageCard(cardNumber);
                    break;
                case "2":
                    RecordAttendance(cardNumber);
                    break;
                case "3":
                    PayForCafetira(cardNumber);
                    break;
                case "4":
                    PayforbusRide(cardNumber);
                    break;
                case "5":
                    ViewTransactionHistory(cardNumber);
                    break;

                case "6":
                    n = 0;
                    break;
                default:
                    {
                        Console.WriteLine("Invalid choice. Please try again.\n");
                    }
                    break;
            }
        }
    }

    public static void facultyMenu(int cardNumber)
    {
        int n = 1;
        while (n != 0)
        {
            Console.WriteLine("Welcome Faculty Member! Please choose an option:");
            Console.WriteLine("[1] Generate Attendance Report");
            Console.WriteLine("[2] Pay for Parking");
            Console.WriteLine("[3] Logout\n");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    GenerateReport(cardNumber);
                    break;
                case "2":
                    PayForParking(cardNumber);
                    break;
                case "3":
                    n = 0;
                    break;
                default:
                    {
                        Console.WriteLine("Invalid choice. Please try again.\n");
                    }
                    break;
            }
        }
    }
    public static void AdminMenu()
    {
        int n = 1;
        while (n != 0)
        {
            Console.WriteLine(
                "Welcome Admin! Please choose an option:");

            Console.WriteLine("[1] Issue New Card");
            Console.WriteLine("[2] Block Card");
            Console.WriteLine("[3] Unblock Card");
            Console.WriteLine("[4] View All Cards");
            Console.WriteLine("[5] View All Transactions");
            Console.WriteLine("[6] Logout");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CardIssue();
                    break;
                case "2":
                    BlockCards();
                    break;
                case "3":
                    UnBlockCards();
                    break;
                case "4":
                    ViewALLcards();
                    break;

                case "5":
                    ViewAllTransactions();
                    break;
                case "6":
                    n = 0;
                    break;
                default:
                    {
                        Console.WriteLine("Invalid choice. Please try again.\n");
                    }
                    break;
            }
        }
    }


    public static void Main(string[] args)
    {
        FileManager.LoadFirstData();

        int n = 1;
        while (n != 0)
        {
            Console.WriteLine(
                "Welcome to University Card Management System! Please choose your role:");

            Console.WriteLine("[1] Admin");
            Console.WriteLine("[2] Card Holder");
            Console.WriteLine("[3] Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AdminMenu();
                    break;
                case "2":
                    Console.WriteLine("[1] Login as student");
                    Console.WriteLine("[2] Login as faculty member");
                    Console.Write("Enter your choice: ");
                    string ch = Console.ReadLine();
                    switch (ch)
                    {
                        case "1":
                            Console.WriteLine("Hmm, you're trying to log in as a Student\n");
                            Console.Write("Enter your card number: ");
                            int studentCardNumber = Convert.ToInt32(Console.ReadLine());
                            List<Cards> studentCards = FileManager.LoadCards();
                            // مقارنة بدون حساسية للحروف وإزالة الفراغات
                            Cards studentCard = studentCards.Find(c =>
                                c.CardNumber == studentCardNumber &&
                                string.Equals(c.Type?.Trim(), "student", StringComparison.OrdinalIgnoreCase)
                            );

                            if (studentCard != null)
                            {
                                if (studentCard.GetStatus().Trim().ToLower() == "unblocked")
                                {
                                    studentMenu(studentCardNumber);
                                }
                                else
                                {
                                    Console.WriteLine("Your card is blocked. Please contact admin.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid card number or not a student card.");
                            }
                            break;

                        case "2":
                            Console.WriteLine("Hmm, you're trying to log in as a faculty member\n");
                            Console.Write("Enter your card number: ");
                            int facultyCardNumber = Convert.ToInt32(Console.ReadLine());
                            List<Cards> facultyCards = FileManager.LoadCards();
                            
                            Cards facultyCard = facultyCards.Find(c =>
                                c.CardNumber == facultyCardNumber &&
                                string.Equals(c.Type?.Trim(), "faculty member", StringComparison.OrdinalIgnoreCase) );//عشان يفرق الاحرف

                            if (facultyCard != null)
                            {
                                if (facultyCard.GetStatus().Trim().ToLower() == "unblocked")
                                {
                                    facultyMenu(facultyCardNumber);
                                }
                                else
                                {
                                    Console.WriteLine("Your card is blocked. Please contact admin.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid card number or not a faculty member card.");
                            }
                            break;

                        case "3":
                            n = 0;
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.\n");
                            break;
                    }
                    break;
                    case "3":
                        Console.WriteLine("Exiting the system. Goodbye!");
                    n = 0;
                        break; 

            }
        }
    }
}

