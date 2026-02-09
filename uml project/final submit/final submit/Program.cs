using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.ConstrainedExecution;
using System.Security.Authentication.ExtendedProtection;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;


/*namespace RJR
 * name= joudy alhaj 172845
 * name = Raghad alshorman 170792
 * name =reham subra 171266
 * 
 * 
 * 
*/








class Card
{
    int CardNO = 0;
    double Balance = 0;
    string Type = "";
    string Status = "unblocked";
    string UserID = "";
    public int cardNo
    {
        get { return CardNO; }
        set
        {

            if (value > 0)
                CardNO = value;
            else
                Console.WriteLine("Error you try Add CardNOmber that's already existing  : \n");

        }
    }

    public double balanc
    {
        get { return Balance; }
        set
        {

            if (value >= 0)
            {
                Balance = value;
            }
            else
                Console.WriteLine("Error you try to add non positive value to Balance: \n");

        }
    }

    public string type
    {
        get { return Type; }
        set
        {
           
                Type = value;
            

        }
    }

    public string status
    {
        get { return Status; }
        set { Status = value; }
    }
    public string userid
    {
        get { return UserID; }
        set { UserID = value; }
    }
    public Card()
    {
        CardNO = 0;
        Balance = 0;
        Status = "";
        UserID = "";
        Type = "";
    }
    public Card(int no, double ba, string t, string sa, string id)
    {
        CardNO = no;
        Balance = ba;
        Type = t;
        Status = sa;
        UserID = id;
    }
    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;
        if (this.GetType() != obj.GetType())
            return false;

        Card card = (Card)obj;
        if (this.CardNO == card.CardNO)
            return true;

        return false;
    }
    public override string ToString()
    {
        return "The card number is: " + CardNO + ", Balance: " + Balance + ", Type: " + Type + ", Status: " + Status + ", UserID: " + UserID;
    }

}
abstract class CardHolder
{
    string UserId = "";
    string Name = "";
    public string userid
    {
        get { return UserId; }
        set
        {

            if (!string.IsNullOrWhiteSpace(value))
            {
                UserId = value;
            }
            else
            {
                Console.WriteLine("Error you try to add empty UserID\n");
            }
        }
    }
    public string name
    {
        get { return Name; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
                Name = value;

        }
    }
    public CardHolder()
    {
        UserId = "";
        Name = "";
    }
    public CardHolder(string id, string name)
    {
        userid = id;
        Name = name;
    }

    public override string ToString()
    {
        return "Name:" + Name + ", UserID:" + UserId;
    }
    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        if (this.GetType() != obj.GetType())
            return false;
        CardHolder ch = (CardHolder)obj;
        if (this.UserId == ch.UserId)
            return true;
        return false;
    }
}

class Student : CardHolder
{
    List<string> RegisteredCourses = new List<string>();
    public Student() : base()
    {
        RegisteredCourses = new List<string>();
    }
    public Student(string userid, string name, List<string> regcourse) : base(userid, name)
    {
        if (regcourse != null)
            RegisteredCourses = regcourse;
        else
            RegisteredCourses = new List<string>();
    }
    public List<string> registeredCourses
    {
        get { return RegisteredCourses; }
        set
        {
            if (value != null)
                RegisteredCourses = value;
        }
    }

    public string GetStudentDetails()/////not needed

    {
        return "student Name: " + name + ", UserID: " + userid + ", Registered Courses: " + string.Join(", ", RegisteredCourses);
    }
    public override string ToString()
    {
        return "student Name: " + name + ", UserID: " + userid + ", Registered Courses: " + string.Join(", ", RegisteredCourses);
    }
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (!base.Equals(obj)) return false;
        if (obj.GetType() != this.GetType()) return false;
        Student s = (Student)obj;
        bool coursesEqual = !this.RegisteredCourses.Except(s.RegisteredCourses).Any() && !s.RegisteredCourses.Except(this.RegisteredCourses).Any();
        if (coursesEqual)
        {
            if (this.name == s.name && this.userid == s.userid)
            {
                return true;
            }
            return false;
        }
        return false;
    }
}
class FacultyMember : CardHolder
{
    List<string> Taught_Courses = new List<string>();
    public FacultyMember() : base()
    {
        Taught_Courses = new List<string>();
    }
    public FacultyMember(string na, string id, List<string> tc) : base(id, na)
    {
        if (tc != null)
            Taught_Courses = tc;
        else
            Taught_Courses = new List<string>();
    }
    public List<string> taught_courses
    {
        get { return Taught_Courses; }
        set
        {
            if (value != null)
                Taught_Courses = value;
            else
                Taught_Courses = new List<string>();
        }
    }


    public string GetFacultyDetails()
    {
        return "Facultyn Name: " + base.name + ",UserID: " + base.userid + ",Taught Courses: " + string.Join(",", Taught_Courses);
    }
    public override string ToString()
    {
        return "Facultyn Name: " + base.name + ",UserID: " + base.userid + ",Taught Courses: " + string.Join(",", Taught_Courses);

    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (this.GetType() != obj.GetType()) return false;
        FacultyMember fa = (FacultyMember)obj;


        return this.userid == fa.userid;

    }
}

class BusTrack
{
    int TrackID;
    string Origin;
    string Destination;
    double Cost;
    public int trackID
    {
        set { TrackID = value; }
        get { return TrackID; }
    }
    public string origin
    {
        set { Origin = value; }
        get { return Origin; }
    }
    public string destination
    {
        set { Destination = value; }
        get { return Destination; }
    }
    public double cost
    {
        // set { Cost = value; }
        get { return Cost; }
    }

    public double GetCost()
    {

        switch (TrackID)
        {
            case 1: return 3;
            case 2: return 4;
            case 3: return 5;

            default: Console.WriteLine("the track is not available\n"); return 0;
        }
    }
    public BusTrack(int t, string d)
    {
        TrackID = t;
        Origin = "Origin";
        Destination = d;
        Cost = 0;
    }

    public static void PrintTracks()
    {
        Console.WriteLine("Track 1-> Origin: main gate , Destination: Northen buildings, Cost: 3JD");
        Console.WriteLine("Track 2-> Origin: main gate , Destination: Southern buildings, Cost: 4JD");
        Console.WriteLine("Track 3-> Origin: main gate , Destination: Library, Cost: 5JD\n");
    }

}


class ParkingService
{
    public ParkingService()
    {
        Console.WriteLine("Welcome to the university parking\n These are the booking times, prices, and available offers:");
        Console.ForegroundColor = ConsoleColor.Green;
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
            Console.WriteLine("Sorry,but you enter non logic number for hours.\n");
            return 0;
        }
    }

}

class Transaction
{
    string tranID;
    int CardNo;
    string Type;

    object Amount;
    public string tranid
    {
        get { return tranID; }
        set
        {

            if (!string.IsNullOrEmpty(value))
            {
                tranID = value;
            }
            else
            {
                Console.WriteLine("Error: Transaction ID cannot be null or empty.\n");


            }

        }
    }



    public int cardNo
    {
        get { return CardNo; }
        set { CardNo = value; }
    }
    public string type
    {
        get { return Type; }
        set { Type = value; }
    }
    public object amount
    {
        get { return Amount; }
        set { Amount = value; }

    }
    public Transaction()
    {
        tranID = "";
        CardNo = 0;
        Type = "";
        Amount = 0;
    }
    public Transaction(int cn, string t, object a)// tid means the transaction id :
    {
        this.tranID = "";
        CardNo = cn;
        Type = t;
        Amount = a;
    }
    public string GetTransactionID()
    {
        return "The transactionID is: " + tranID;
    }
    public string GetType() { return "The transaction is: " + Type; }

    public object GetAmount() { return "The amount is :" + Amount; }
    public override string ToString()
    {
        return GetTransactionID() + "The card nomber:" + CardNo + GetType() + GetAmount();
    }


}
class CafeteriaItem
{
    int select;
    double Price;

    public double price
    { get { return Price; } }
    public int s
    {
        set { select = value; }
    }
    public CafeteriaItem()
    {
        Console.WriteLine("Welcome to the  RJR namespace cafeteria\n\n");
        Console.WriteLine("         " + " ITEM " + "\t" + "PRICE");
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("    1:   " + " Steak" + "\t" + "8JD");
        Console.WriteLine("    2:  " + " Soup  " + "\t" + "2JD");
        Console.WriteLine("    3:" + " Sandwich" + "\t" + "3JD");
        Console.WriteLine("    4:   " + "Salad " + "\t" + "4JD");
        Console.WriteLine("    5:  " + " Tea  " + "\t" + "2JD");
        Console.WriteLine("    6:  " + "Juice " + "\t" + "3JD");
        Console.WriteLine("    7:  " + "Cake  " + "\t" + "5JD");
        Console.WriteLine("    8:   " + "Water" + "\t" + "1JD");

    }

    public double GetPrice()
    {
        switch (select)
        {
            case 1: return 8;
            case 2: return 2;
            case 3: return 3;
            case 4: return 4;
            case 5: return 2;
            case 6: return 3;
            case 7: return 5;
            case 8: return 1;
            default: Console.WriteLine("the item is not avaiable\n"); return 0;

        }

    }

}

class AttendanceRecord
{
    string CourseID;
    DateOnly Date = DateOnly.FromDateTime(DateTime.Today);//apears date only without time
    List<string> Attendees = new List<string>();
    public string courseid
    {
        get { return CourseID; }//check
        set { CourseID = value; }
    }
    public List<string> attendees
    {
        get { return Attendees; }
        set { Attendees = value; }

    }
    public DateOnly date
    {
        get { return Date; }
        set { Date = value; }
    }
    public AttendanceRecord()
    {


    }


    public override string ToString()
    {
        return "CourseID: " + CourseID + "  Date: " + Date + "  Attendees:\n\t\t\t\t\t\t" + string.Join("\n\t\t\t\t\t\t", Attendees);
    }
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (this.GetType() != obj.GetType()) return false;
        AttendanceRecord ar = (AttendanceRecord)obj;
        bool diff = false;
        if (!(this.CourseID == ar.CourseID && this.Date == ar.Date)) return false;

        if (this.Attendees.Count == ar.Attendees.Count)
        {

            for (int i = 0; i < this.Attendees.Count; i++)
            {
                if (this.Attendees.Contains(ar.Attendees[i]))
                {
                    diff = true;


                }
                else return false;
            }
            return diff;

        }
        return false;
    }

}

class GlobalTransaction
{
    
    public static void SaveTransaction(List<Transaction> T)
    {

        using (FileStream a = new FileStream("TransactionDB.json", FileMode.Create, FileAccess.Write))
        {
            JsonSerializer.Serialize<List<Transaction>>(a, T);
        }

    }
    public static List<Transaction> ReadTransactionDB()
    {
        if (File.Exists("TransactionDB.json"))
        {
            try
            {
                using (FileStream A = new FileStream("TransactionDB.json", FileMode.Open, FileAccess.Read))
                {
                    List<Transaction> T = JsonSerializer.Deserialize<List<Transaction>>(A) ?? new List<Transaction>();
                    return T;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occurred while reading the Transaction database:\n" + ex.Message);
                Console.ResetColor();
                return new List<Transaction>();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("The Transaction database file does not exist!!!!\n");
            Console.ResetColor();
            return new List<Transaction>();
        }
    }

}

class RegisterAttendance
{

    public static void SaveRecordsAtendance(List<AttendanceRecord> AR)
    {


        using (FileStream a = new FileStream("AttendanceDB.json", FileMode.Create, FileAccess.Write))
        {
            JsonSerializer.Serialize<List<AttendanceRecord>>(a, AR);
        }
    }

    public static List<AttendanceRecord> ReadAttendanceDB()
    {
        if (File.Exists("AttendanceDB.json"))
        {
            try
            {
                using (FileStream A = new FileStream("AttendanceDB.json", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    List<AttendanceRecord> AR = JsonSerializer.Deserialize<List<AttendanceRecord>>(A) ?? new List<AttendanceRecord>();
                    return AR;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occurred while reading the Attendance database:\n" + ex.Message);
                Console.ResetColor();
                return new List<AttendanceRecord>();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("The Attendance database file does not exist!!!!\n");
            Console.ResetColor();
            return new List<AttendanceRecord>();
        }
    }
}

class CardReader
{
    public static void LoadInitialData()
    {
        if (!File.Exists("studentDB.json"))
        {
            List<string> R1 = new List<string>();
            R1.Add("CPE100");
            R1.Add("SE400");
            Student S1 = new Student("S01", "Ali", R1);
            List<string> R2 = new List<string>();
            R2.Add("CPE100");
            R2.Add("NES200");
            Student S2 = new Student("S02", "Omar", R2);

            List<string> R3 = new List<string>();
            R3.Add("NES200");
            R3.Add("CIS300");
            R3.Add("SE400");
            Student S3 = new Student("S03", "Reem", R3);
            List<string> R4 = new List<string>();
            R4.Add("CPE100");
            R4.Add("SE400");
            Student s4 = new Student("S04", "Maher", R4);
            List<Student> students = new List<Student>();
            students.Add(S1);
            students.Add(S2);
            students.Add(S3);
            students.Add(s4);
            using (FileStream studentDB = new FileStream("studentDB.json", FileMode.Create))
            {
                JsonSerializer.Serialize<List<Student>>(studentDB, students);
            }

        }
        if (!File.Exists("FacultysDB.json"))
        {
            List<string> FC1 = new List<string>();
            FC1.Add("CPE100");
            FC1.Add("CIS300");
            FacultyMember F1 = new FacultyMember("F01", "Sami", FC1);
            List<string> FC2 = new List<string>();
            FC2.Add("NES200");
            FC2.Add("SE400");
            FacultyMember F2 = new FacultyMember("F02", "Eman", FC2);
            List<FacultyMember> Faculty_Members = new List<FacultyMember>();
            Faculty_Members.Add(F1);
            Faculty_Members.Add(F2);
            using (FileStream FacultysDB = new FileStream("FacultysDB.json", FileMode.Create, FileAccess.Write))
            {
                JsonSerializer.Serialize<List<FacultyMember>>(FacultysDB, Faculty_Members);
            }
        }
        if (!File.Exists("CardsDB.json"))
        {
            Card C1 = new Card(10, 80, "faculty member", "unblocked", "F02");
            Card C2 = new Card(20, 110, "student", "unblocked", "S02");
            Card C3 = new Card(30, 95, "student", "blocked", "S03");
            Card C4 = new Card(40, 160, "student", "unblocked", "S04");
            List<Card> Cards = new List<Card>();
            Cards.Add(C1);
            Cards.Add(C2);
            Cards.Add(C3);
            Cards.Add(C4);
            using (FileStream CardsDB = new FileStream("CardsDB.json", FileMode.Create))
            {
                JsonSerializer.Serialize<List<Card>>(CardsDB, Cards);
            }
        }
        if (!File.Exists("TransactionDB.json"))
        {
            List<Transaction> transactions = new List<Transaction>();

            using (FileStream TransactionDB = new FileStream("TransactionDB.json", FileMode.Create))
            {
                JsonSerializer.Serialize<List<Transaction>>(TransactionDB, transactions);
            }
        }
        if (!File.Exists("AttendanceDB.json"))
        {
           List<AttendanceRecord> attendances = new List<AttendanceRecord>();

            using (FileStream AttendanceDB = new FileStream("AttendanceDB.json", FileMode.Create))
            {
                JsonSerializer.Serialize<List<AttendanceRecord>>(AttendanceDB, attendances);
            }
        }
        }

    public static void UpdateStudentDB(List<Student> students)
    {

        using (FileStream studentDB = new FileStream("studentDB.json", FileMode.Create))//creat is already cheacked if the file exisits or not and delete it and creat a new one save the updated data
        {
            JsonSerializer.Serialize<List<Student>>(studentDB, students);
        }


    }

    public static List<Student> ReadStudentsDB()
    {

        if (File.Exists("studentDB.json"))
        {
            try
            {
                using (FileStream studentDB = new FileStream("studentDB.json", FileMode.Open))
                {
                    List<Student> students = JsonSerializer.Deserialize<List<Student>>(studentDB) ?? new List<Student>();
                    return students;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occurred while reading the student database: \n" + ex.Message);
                Console.ResetColor();
                return new List<Student>();
            }
        }


        else
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("The student database file does not exist!!!!\n");
            Console.ResetColor();
            return new List<Student>();
        }

    }
    public static List<FacultyMember> ReadFacultyDB()
    {
        if (File.Exists("FacultysDB.json"))
        {
            try
            {
                using (FileStream f = new FileStream("FacultysDB.json", FileMode.Open, FileAccess.Read))
                {
                    List<FacultyMember> faculty = JsonSerializer.Deserialize<List<FacultyMember>>(f) ?? new List<FacultyMember>();
                    return faculty;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occurred while reading the Faculty Member database:\n" + ex.Message);
                Console.ResetColor();
                return new List<FacultyMember>();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("The Faculty Members database file does not exist!!!!\n");
            Console.ResetColor();
            return new List<FacultyMember>();
        }
    }

    public static void UpdatFacultyDB(List<FacultyMember> f)
    {
        using (FileStream FacultyDB = new FileStream("FacultysDB.json", FileMode.Create))
        {
            JsonSerializer.Serialize<List<FacultyMember>>(FacultyDB, f);
        }
    }
    public static List<Card> Readcards()
    {
        if (File.Exists("CardsDB.json"))
        {
            try
            {
                using (FileStream cardDB = new FileStream("CardsDB.json", FileMode.Open, FileAccess.Read))
                {
                    List<Card> C = JsonSerializer.Deserialize<List<Card>>(cardDB) ?? new List<Card>();
                    return C;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occurred while reading the CardsDB database:\n" + ex.Message);
                Console.ResetColor();
                return new List<Card>();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("The CardsDB database file does not exist!!!!\n");
            Console.ResetColor();
            return new List<Card>();
        }



    }
    public static void UpdateCardsDB(List<Card> Cards)
    {
        using (FileStream Cnew = new FileStream("CardsDB.json", FileMode.Create))
        {
            JsonSerializer.Serialize<List<Card>>(Cnew, Cards);
        }
    }
}


class Program
{


    public static void RechargeCard(int cardNo)
    {

        int cardNumber = cardNo;
        List<Card> C = new List<Card>();
        C = CardReader.Readcards();

        if ((C.Find(c => c.cardNo == cardNumber) != null))
        {
            Console.WriteLine(C.Find(c => c.cardNo == cardNumber));
            Console.WriteLine("Enter the amount to recharge:");
            double amount = System.Convert.ToDouble(Console.ReadLine());
            if (amount > 0)
            {
                (C.Find(c => c.cardNo == cardNumber)).balanc += amount;
                CardReader.UpdateCardsDB(C);
                Console.WriteLine("The proccess of recharging the card Done Successfuly. New Balance: " + (C.Find(c => c.cardNo == cardNumber)).balanc);
                List<Transaction> T = GlobalTransaction.ReadTransactionDB();
                Transaction newTransaction = new Transaction(cardNumber, "Recharge", amount);
                Console.Write("Enter The transaction id :");
                newTransaction.tranid = Console.ReadLine();
                if (T.Any(x => newTransaction.tranid == x.tranid))
                {
                    newTransaction.tranid = Guid.NewGuid().ToString().Substring(0, 8);
                }
                T.Add(newTransaction);
                GlobalTransaction.SaveTransaction(T);

            }
            else
            {
                Console.WriteLine("Invalid amount. Please enter a positive value.\n");
            }
        }
        else
        {
            Console.WriteLine("Card not found. Please check the card number and try again.\n");
        }
    }


    public static void RecordLectureAttendance(int cardNo)
    {//////search for student by card no
        List<Card> C = new List<Card>();
        C = CardReader.Readcards();
        string userId = (C.Find(c => c.cardNo == cardNo)).userid;
        ///// get student registered courses
        ///
        List<Student> students = new List<Student>();
        students = CardReader.ReadStudentsDB();
        Student st = students.Find(s => s.userid == userId);
        Console.WriteLine("your Registered courses:");
        foreach (string course in st.registeredCourses)
        {
            Console.Write(course + "\t");
        }
        ///////// create attendance record
        AttendanceRecord AR = new AttendanceRecord();
        //////
        Console.WriteLine("\nEnter the Course ID for which you want to record attendance:");
        string courseID = Console.ReadLine();
        //// check if the student is registered for the course
        if (!st.registeredCourses.Contains(courseID))
        {
            Console.WriteLine("You are not registered for this course. Attendance not recorded.\n");
            return;
        }


        Console.WriteLine("\nEnter the Date of Lecture in format (yyyy-mm-dd):");
        DateOnly Date = DateOnly.Parse(Console.ReadLine());

        //// check if the date is today's date
        if (Date != DateOnly.FromDateTime(DateTime.Now))
        {
            Console.WriteLine("Invalid date. Attendance can only be recorded for today's date:\n " + DateOnly.FromDateTime(DateTime.Now));
            return;
        }

        List<AttendanceRecord> existingRecords = new List<AttendanceRecord>();
        existingRecords = RegisterAttendance.ReadAttendanceDB();
        AttendanceRecord n = new AttendanceRecord();
        n = existingRecords.Find(r => r.courseid == courseID && r.date == Date);
        if (n != null)
        {
            if (n.attendees.Contains(userId)) Console.WriteLine("Attendance already recorded for Course ID: " + courseID + " on Date: " + Date);
            else n.attendees.Add(userId);
            RegisterAttendance.SaveRecordsAtendance(existingRecords);
        }
        else
        {
            n = new AttendanceRecord();
            n.courseid = courseID;
            n.date = Date;
            n.attendees.Add(userId);
            existingRecords.Add(n);
            RegisterAttendance.SaveRecordsAtendance(existingRecords);
            Console.WriteLine("Attendance recorded successfully for Course ID: " + courseID + " on Date: " + Date);
            Transaction T = new Transaction(cardNo, "attendance", "N/A");
            Console.Write("Enter The transaction id :");
            T.tranid = Console.ReadLine();
            List<Transaction> Tnew = new List<Transaction>();
            Tnew = GlobalTransaction.ReadTransactionDB();
            if (Tnew.Any(x => x.tranid == T.tranid))
            {
                T.tranid = Guid.NewGuid().ToString().Substring(0, 8);
            }
            Tnew.Add(T);
            GlobalTransaction.SaveTransaction(Tnew);


            return;
        }

    }

    public static void Pay_for_cafeteria(int c)
    {

        CafeteriaItem buy = new CafeteriaItem();
        int e = 1;
        double totPrice = 0;
        while (e != 0)
        {

            Console.WriteLine("Enter an item or 0 to end order:");
            e = System.Convert.ToInt32(Console.ReadLine());
            if (e == 0) break;
            else
            {
                buy.s = e;
                totPrice += buy.GetPrice();
            }
        }
        Console.WriteLine("the total price is:" + totPrice + "JD" + "\n To confirm enter #,To cancel enter any anathor symbols\n");
        string confirm = Console.ReadLine();
        if (confirm == "#")
        {
            List<Card> card = CardReader.Readcards();

            if ((card.Find(s => s.cardNo == c)).balanc >= totPrice)
            {
                (card.Find(s => s.cardNo == c)).balanc -= totPrice;
                CardReader.UpdateCardsDB(card);


                Console.WriteLine("The payment process was completed successfully\n");

                List<Transaction> t = new List<Transaction>();
                t = GlobalTransaction.ReadTransactionDB();
                Transaction T = new Transaction(c, "payment", totPrice);
                Console.Write("Enter The transaction id :\n");
                T.tranid = Console.ReadLine();
                if (t.Any(x => x.tranid == T.tranid))
                {
                    T.tranid = Guid.NewGuid().ToString().Substring(0, 8);
                }
                t.Add(T);
                GlobalTransaction.SaveTransaction(t);

            }
            else
                Console.WriteLine("Insufficient funds,Payment failed.\n");

        }
        else
            Console.WriteLine("The transaction was cancelled and your balance was not deducted.\n");



    }

    public static void Pay_for_bus_ride(int cnum)
    {

        BusTrack.PrintTracks();

        Console.Write("Enter the track number (1, 2, or 3): ");
        int trackNumber = Convert.ToInt32(Console.ReadLine());

        Console.Write("Enter the Destination (e.g., NB: Northern buildings, SB: Southern buildings, L: Library): ");
        string destination = Console.ReadLine();

        BusTrack bus = new BusTrack(trackNumber, destination);
        double cost = bus.GetCost();

        if (cost <= 0)
        {
            Console.WriteLine("Invalid track or destination. Transaction cancelled.\n");

        }
        else
        {
            Console.WriteLine($"The cost of the track is: {cost} JD");
            Console.WriteLine("To confirm payment, enter #. To cancel, enter any other symbol.\n");
            string confirm = Console.ReadLine();

            if (confirm != "#")
            {
                Console.WriteLine("The transaction was cancelled and your balance was not deducted.\n");

            }
            else
            {
                List<Card> cards = CardReader.Readcards();
                Card currentCard = cards.Find(c => c.cardNo == cnum);

                if (currentCard == null)
                {
                    Console.WriteLine("Card not found. Transaction cancelled.\n");

                }
                else
                {
                    if (currentCard.balanc < cost)
                    {
                        Console.WriteLine("Insufficient funds. Payment failed.\n");

                    }
                    else
                    {
                      
                        currentCard.balanc -= cost;
                        CardReader.UpdateCardsDB(cards);
                        Console.WriteLine("The payment process was completed successfully.\n");

                        Console.Write("Enter the transaction ID: \n");

                        List<Transaction> transactions = GlobalTransaction.ReadTransactionDB();
                        Transaction newTransaction = new Transaction(cnum, "Bus Payment", cost);
                        newTransaction.tranid = Console.ReadLine();
                        if (transactions.Any(x => x.tranid == newTransaction.tranid))
                        {
                            newTransaction.tranid = Guid.NewGuid().ToString().Substring(0, 8);
                        }


                        transactions.Add(newTransaction);
                        GlobalTransaction.SaveTransaction(transactions);
                    }
                }
            }
        }
    }
    public static void ViewTransactionHstory(int cn)
    {
        List<Card> Cards = new List<Card>();
        Cards = CardReader.Readcards();


        List<Transaction> t = new List<Transaction>();
        t = GlobalTransaction.ReadTransactionDB();
        if (Cards.Find(s => s.cardNo == cn) == null)
        {
            Console.WriteLine("The card number that you entered is not found.\n");

        }
        else
        {
            foreach (Transaction T in t)
            {
                if (T.cardNo == cn)
                    Console.WriteLine(T);
            }

        }
    }

    public static void Access_car_parking(int num)
    {
        ParkingService park = new ParkingService();

        Console.Write("Enter the number of hours ");
        int h = System.Convert.ToInt32(Console.ReadLine());
        int cost = park.CalculateFee(h);

        List<Card> card = new List<Card>();
        card = CardReader.Readcards();
        if ((card.Find(s => s.cardNo == num)).balanc >= cost)
        {
            (card.Find(s => s.cardNo == num)).balanc -= cost;

            Console.WriteLine("The payment process was completed successfully\n");

            List<Transaction> t = new List<Transaction>();
            t = GlobalTransaction.ReadTransactionDB();
            Transaction T = new Transaction(num, "payment", cost);
            Console.Write("Enter The transaction id :");
            T.tranid = Console.ReadLine();
            if (t.Any(x => x.tranid == T.tranid))
            {
                T.tranid = Guid.NewGuid().ToString().Substring(0, 8);
            }
            t.Add(T);
            GlobalTransaction.SaveTransaction(t);
        }
        else
        {
            Console.WriteLine("Insufficient funds,Payment failed.\n");
            Console.WriteLine("The transaction was cancelled and your balance was not deducted.\n");
        }
    }


    public static void Generate_attendance_report(int num)
    {

        List<Card> cards = CardReader.Readcards();


        Card currentCard = cards.Find(c => c.cardNo == num);
        if (currentCard == null)
        {
            Console.WriteLine("Card not found.");
            return;
        }

        string facultyUserId = currentCard.userid;


        List<FacultyMember> facultyList = CardReader.ReadFacultyDB();
        FacultyMember faculty = facultyList.Find(f => f.userid == facultyUserId);

        if (faculty == null)
        {
            Console.WriteLine("Faculty member not found.\n");
            return;
        }


        Console.WriteLine("Courses you teach:");
        foreach (string course in faculty.taught_courses)
        {
            Console.Write(course + "  ");
        }


        Console.WriteLine("\nEnter the Course ID to generate attendance report:");
        string courseId = Console.ReadLine();


        Console.WriteLine("Enter the Date of Lecture (yyyy-mm-dd):");
        DateOnly date = DateOnly.Parse(Console.ReadLine());


        List<AttendanceRecord> records = RegisterAttendance.ReadAttendanceDB();


        AttendanceRecord record =
            records.Find(r => r.courseid == courseId && r.date == date);

        if (record != null)
        {
            Console.WriteLine("\nAttendance Report:");
            Console.WriteLine(record);
        }
        else
        {
            Console.WriteLine("No attendance record found for this course and date.\n");
        }
    }









    public static void IssueCard()
    {
        Console.WriteLine("Please Enter the info to creat a new card According to the following requrments: \n");
        Console.WriteLine("Enter the Card Number :");
        int n = System.Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the Card TypeUser :");
        string t = Console.ReadLine();

        if (t != "student" && t != "faculty member")
        {
            Console.WriteLine("Invalid card type. Use 'student' or 'faculty member'.");
            return;
        }

        Console.WriteLine("Enter the User ID :");
        string id = Console.ReadLine();
        Card New = new Card(n, 50, t, "unblocked", id);
        List<Card> C = new List<Card>();

        C = CardReader.Readcards();

        if ((C.Find(j => j.cardNo == n) != null))
        {
            Console.WriteLine("The card already exists \n");
        }
        else
        {
            C.Add(New);
            CardReader.UpdateCardsDB(C);
            Console.WriteLine("The proccess of creating new card  Done Successfuly\n ");
        }

    }
    public static void Blockcard()
    {
        List<Card> AllCards = new List<Card>();
        List<Card> unblockedCards = new List<Card>();
        AllCards = CardReader.Readcards();
        foreach (Card c in AllCards)
        {
            if (c.status == "unblocked")
            {
                unblockedCards.Add(c);
                Console.WriteLine(c.ToString());
            }
        }
        Console.WriteLine("Please Enter the Card Number Which you want to change it's status to blocked: \n");
        int n = System.Convert.ToInt32(Console.ReadLine());
        if (unblockedCards.Find(s => s.cardNo == n) != null)
        {
            unblockedCards.Find(s => s.cardNo == n).status = "blocked";
            CardReader.UpdateCardsDB(AllCards);
            Console.WriteLine("The proccess of change the status of the card that had cardNumber =" + n + " to blocked Done Successfuly ");
        }
        else
        {
            Console.WriteLine("The card number that you entered is not unblocked or not found\n ");
        }
    }
    public static void unBlockcard()
    {
        List<Card> AllCards = new List<Card>();
        List<Card> blockedCards = new List<Card>();
        AllCards = CardReader.Readcards();
        foreach (Card c in AllCards)
        {
            if (c.status == "blocked")
            {
                blockedCards.Add(c);
                Console.WriteLine(c.ToString());
            }
        }
        Console.WriteLine("Please Enter the Card Number Which you want to change it's status to unblocked: ");
        int n = System.Convert.ToInt32(Console.ReadLine());

        if (blockedCards.Find(s => s.cardNo == n) != null)
        {
            blockedCards.Find(s => s.cardNo == n).status = "unblocked";
            CardReader.UpdateCardsDB(AllCards);
            Console.WriteLine("The proccess of change the status of the card that had cardNumber =" + n + " to unblocked Done Successfuly ");
        }
        else
        {
            Console.WriteLine("The card number that you entered is not blocked or not found \n");
        }
    }

    public static void View_all_cards()
    {
        List<Card> cards = CardReader.Readcards();
        foreach (Card c in cards)
        {
            Console.WriteLine(c);
        }
    }

    public static void View_all_transactions()
    {
        List<Transaction> t = GlobalTransaction.ReadTransactionDB();
        foreach (Transaction tr in t)
        {
            Console.WriteLine(tr);
        }
    }




    public static void CardHolderMenu()
    {
        int[] CardHolderChoice = LoginAsCardHolder();
        while (CardHolderChoice[0] != 3 && CardHolderChoice[1] != 0)
        {
            if (CardHolderChoice[0] == 3) break;
            switch (CardHolderChoice[0])
            {
                case 1:
                    StudentMenu(CardHolderChoice[1]);
                    break;
                case 2:
                    FacultyMemberMenu(CardHolderChoice[1]);
                    break;
                    
            }
            CardHolderChoice = LoginAsCardHolder();
        }
    }

    public static void FacultyMemberMenu(int cardNo)
    {
        int n = 1;
        while (n != 0)
        {
            Console.WriteLine("Welcome Faculty Member! Please choose an option:");
            Console.WriteLine("[1] recharge Card");
            Console.WriteLine("[2] Access Car parking");
            Console.WriteLine("[3] Generate Attendance Report");
            Console.WriteLine("[4] Logout");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    RechargeCard(cardNo); break;

                case "2":
                    Access_car_parking(cardNo); break;

                case "3":
                    Generate_attendance_report(cardNo); break;

                case "4":
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

    public static void StudentMenu(int cardNo)
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
                    RechargeCard(cardNo);
                    break;
                case "2":
                    RecordLectureAttendance(cardNo);
                    break;
                case "3":
                    Pay_for_cafeteria(cardNo);
                    break;
                case "4":
                    Pay_for_bus_ride(cardNo);
                    break;
                case "5":
                    ViewTransactionHstory(cardNo);
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







    public static void AdminMenu()
    {
        int n = 1;

        while (n != 0)
        {
            Console.WriteLine("Welcome Admin! Please choose an option:");
            Console.WriteLine("[1] Issue Card");
            Console.WriteLine("[2] Block Card");
            Console.WriteLine("[3] Unblock Card");
            Console.WriteLine("[4] View All Cards");
            Console.WriteLine("[5] View All Transactions");
            Console.WriteLine("[6] Back To Main Login Screen\n");
            Console.Write("Enter your choice: ");
            string adminChoice = Console.ReadLine();
            switch (adminChoice)
            {
                case "1":
                    IssueCard();
                    break;
                case "2":
                    Blockcard();
                    break;
                case "3":
                    unBlockcard();
                    break;
                case "4":
                    View_all_cards();
                    break;
                case "5":
                    View_all_transactions();
                    break;
                case "6":
                    n = 0;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.\n");
                    break;
            }

        }
    }



    public static int[] LoginAsCardHolder()
    {
        int[] arr = new int[2];

        Console.WriteLine("[1] Login As Student");
        Console.WriteLine("[2] Login As Faculty Member");
        Console.WriteLine("[3] Back To Main Login Screen\n");
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                {
                    Console.WriteLine("Hmm, you're trying to log in as a Student\n");
                    Console.Write("Enter Valid Card Number:");
                    int no = System.Convert.ToInt32(Console.ReadLine());
                    List<Card> cards = CardReader.Readcards();
                    Card card = cards.Find(c => c.cardNo == no && c.type == "student");
                    if (card != null)
                    {
                        Console.WriteLine("Card found.");
                        if (card.status == "blocked") { Console.WriteLine(" But,Sorry, your card is blocked. Please contact the administration.\n"); return arr = new int[] { 3, card.cardNo }; }
                        else { Console.WriteLine("Login successful!"); return arr = new int[] { 1, card.cardNo }; }
                    }

                    else { Console.WriteLine("Invalid card number. Please try again."); }
                }
                break;

            case "2":
                {
                    Console.WriteLine("Hmm, you're trying to log in as a faculty member\n");
                    Console.Write("Enter Valid Card Number:");
                    int no = System.Convert.ToInt32(Console.ReadLine());
                    List<Card> cards = CardReader.Readcards();
                    Card card = cards.Find(c => c.cardNo == no && c.type == "faculty member");
                    if (card != null)
                    {
                        Console.WriteLine("Card found.");
                        if (card.status == "blocked") { Console.WriteLine(" But,Sorry, your card is blocked. Please contact the administration.\n"); return arr = new int[] { 3, card.cardNo }; }
                        else { Console.WriteLine("Login successful!"); return arr = new int[] { 2, card.cardNo }; }
                    }

                    else { Console.WriteLine("Invalid card number. Please try again.\n"); }
                }
                break;


            case "3":
                return arr = new int[] { 3, 0 };
            default:
                {
                    Console.WriteLine("Invalid choice. Please try again.\n");

                    return arr = new int[] { 0, 0 };
                }
                break;
        }
        return arr = new int[] { 0, 0 };
    }
    public static string MainLoginScreen()
    {
        Console.WriteLine("[1] Login as Admin ");
        Console.WriteLine("[2] Login as Card Holder ");
        Console.WriteLine("[3] Exit\n ");
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                return "1";
            case "2":
                return "2";
            case "3":
                return "3";
            default:
                Console.WriteLine("Invalid choice. Please try again.\n");
                return "0";
        }
    }
    static void Main(string[] args)
    {
        CardReader.LoadInitialData();

        string MainLoginChoice = MainLoginScreen();
        while (MainLoginChoice != "3")
        {
            switch (MainLoginChoice)
            {
                case "1": AdminMenu(); break;

                case "2": CardHolderMenu(); break;
                default:
                    break;

            }
            MainLoginChoice = MainLoginScreen();


        }
        Console.WriteLine("Exiting the system. Goodbye!");
        return;
    }
}
