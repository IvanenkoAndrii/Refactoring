using System;
using System.Linq;
using System.Text;

class Worker
{
    public string Name { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public Company WorkPlace { get; set; }

    public Worker()
    {
        Name = "";
        Year = 0;
        Month = 0;
        WorkPlace = new Company();
    }

    public Worker(string name, int year, int month, Company company)
    {
        Name = name;
        Year = year;
        Month = month;
        WorkPlace = company;
    }

    public Worker(Worker other)
    {
        Name = other.Name;
        Year = other.Year;
        Month = other.Month;
        WorkPlace = new Company(other.WorkPlace);
    }

    public int GetWorkExperience()
    {
        DateTime currentDate = DateTime.Now;
        int totalMonths = (currentDate.Year - Year) * 12 + (currentDate.Month - Month);
        return totalMonths;
    }

    public double GetTotalMoney() => WorkPlace.Salary * GetWorkExperience();
}

class Company
{
    public string Name { get; set; }
    public string Position { get; set; }
    public double Salary { get; set; }

    public Company()
    {
        Name = "";
        Position = "";
        Salary = 0.0;
    }

    public Company(string name, string position, double salary)
    {
        Name = name;
        Position = position;
        Salary = salary;
    }

    public Company(Company other)
    {
        Name = other.Name;
        Position = other.Position;
        Salary = other.Salary;
    }
}

class Program
{
    public static Worker[] ReadWorkersArray(int n)
    {
        Worker[] workers = new Worker[n];

        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"Введіть дані про працівника {i + 1}:");

            Console.Write("Прізвище та ініціали: ");
            string name = Console.ReadLine();

            int year;
            while (true)
            {
                Console.Write("Рік початку роботи: ");
                if (int.TryParse(Console.ReadLine(), out year) && year > 1900 && year <= DateTime.Now.Year)
                    break;
                Console.WriteLine("Помилка! Введіть коректний рік (не раніше 1900 і не пізніше поточного року).");
            }

            int month;
            while (true)
            {
                Console.Write("Місяць початку роботи: ");
                if (int.TryParse(Console.ReadLine(), out month) && month >= 1 && month <= 12)
                    break;
                Console.WriteLine("Помилка! Введіть число від 1 до 12.");
            }

            Console.Write("Назва компанії: ");
            string companyName = Console.ReadLine();

            Console.Write("Посада: ");
            string position = Console.ReadLine();

            double salary;
            while (true)
            {
                Console.Write("Зарплата: ");
                if (double.TryParse(Console.ReadLine(), out salary) && salary > 0)
                    break;
                Console.WriteLine("Помилка! Введіть позитивне число.");
            }

            Company company = new Company(companyName, position, salary);
            workers[i] = new Worker(name, year, month, company);
        }

        return workers;
    }

    public static void PrintWorker(Worker worker)
    {
        Console.WriteLine("Прізвище та ініціали: {0}", worker.Name);
        Console.WriteLine("Стаж роботи: {0} місяців", worker.GetWorkExperience());
        Console.WriteLine("Загальна сума зароблених коштів: {0}", worker.GetTotalMoney());
        Console.WriteLine("Назва компанії: {0}", worker.WorkPlace.Name);
        Console.WriteLine("Посада: {0}", worker.WorkPlace.Position);
        Console.WriteLine("Зарплата: {0}", worker.WorkPlace.Salary);
    }

    public static void PrintWorkers(Worker[] workers)
    {
        foreach (var worker in workers)
        {
            PrintWorker(worker);
            Console.WriteLine();
        }
    }

    public static void GetWorkersInfo(Worker[] workers, out double maxSalary, out double minSalary)
    {
        maxSalary = workers.Max(w => w.WorkPlace.Salary);
        minSalary = workers.Min(w => w.WorkPlace.Salary);
    }

    public static void SortWorkerBySalary(ref Worker[] workers)
    {
        Array.Sort(workers, (x, y) => y.WorkPlace.Salary.CompareTo(x.WorkPlace.Salary));
    }

    public static void SortWorkerByWorkExperience(ref Worker[] workers)
    {
        Array.Sort(workers, (x, y) => x.GetWorkExperience().CompareTo(y.GetWorkExperience()));
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;

        Console.Write("Введіть кількість працівників: ");
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
        {
            Console.WriteLine("Помилка! Введіть додатне число.");
        }

        Worker[] workers = ReadWorkersArray(n);

        Console.WriteLine("\nІнформація про працівників:");
        PrintWorkers(workers);

        double maxSalary, minSalary;
        GetWorkersInfo(workers, out maxSalary, out minSalary);
        Console.WriteLine("\nМаксимальна зарплата: {0}", maxSalary);
        Console.WriteLine("Мінімальна зарплата: {0}", minSalary);

        SortWorkerBySalary(ref workers);
        Console.WriteLine("\nСортування працівників за зарплатою:");
        PrintWorkers(workers);

        SortWorkerByWorkExperience(ref workers);
        Console.WriteLine("\nСортування працівників за стажем роботи:");
        PrintWorkers(workers);
    }
}
