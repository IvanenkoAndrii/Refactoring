using System;
using System.Linq;
using System.Text;

class Worker
{
    public string FullName { get; set; } // Назва змінина на більш змістовну
    public int StartYear { get; set; } // Назва змінина на більш змістовну
    public int StartMonth { get; set; } // Назва змінина на більш змістовну
    public Company Employer { get; set; } 

    private const int MinYearOfEmployment = 1900; // Змінив MinYear на MinYearOfEmployment

    public Worker()
    {
        InitializeDefaultValues();
    }

    public Worker(string fullName, int startYear, int startMonth, Company employer)
    {
        FullName = fullName;
        StartYear = startYear;
        StartMonth = startMonth;
        Employer = employer;
    }

    public Worker(Worker other)
    {
        FullName = other.FullName;
        StartYear = other.StartYear;
        StartMonth = other.StartMonth;
        Employer = new Company(other.Employer);
    }

    private void InitializeDefaultValues()
    {
        FullName = string.Empty;
        StartYear = MinYearOfEmployment;
        StartMonth = 1;
        Employer = new Company();
    }

    public int CalculateWorkExperience()
    {
        DateTime currentDate = DateTime.Now;
        int totalMonths = (currentDate.Year - StartYear) * 12 + (currentDate.Month - StartMonth);
        return totalMonths;
    }

    public double CalculateTotalEarnings()
    {
        DateTime currentDate = DateTime.Now;
        DateTime startDate = new DateTime(StartYear, StartMonth, 1);
        TimeSpan workDuration = currentDate - startDate;

        return Employer.Salary * workDuration.Days / 30; // Умова взята за 30 днів у місяці
    }
}

class Company
{
    public string CompanyName { get; set; } 
    public string JobTitle { get; set; } 
    public double Salary { get; set; }

    public Company()
    {
        CompanyName = string.Empty;
        JobTitle = string.Empty;
        Salary = 0.0;
    }

    public Company(string companyName, string jobTitle, double salary)
    {
        CompanyName = companyName;
        JobTitle = jobTitle;
        Salary = salary;
    }

    public Company(Company other)
    {
        CompanyName = other.CompanyName;
        JobTitle = other.JobTitle;
        Salary = other.Salary;
    }
}

class Program
{
    
    public static int GetValidYear()
    {
       
    }

    public static int GetValidMonth()
    {
        
    }

    public static double GetValidSalary()
    {
       
    }

    public static Worker[] ReadWorkersData(int numberOfWorkers)
    {
        Worker[] workers = new Worker[numberOfWorkers];

        for (int i = 0; i < numberOfWorkers; i++)
        {
            Console.WriteLine($"Введіть дані про працівника {i + 1}:");

            string fullName = Console.ReadLine();
            int startYear = GetValidYear();
            int startMonth = GetValidMonth();
            string companyName = Console.ReadLine();
            string jobTitle = Console.ReadLine();
            double salary = GetValidSalary();

            Company company = new Company(companyName, jobTitle, salary);
            workers[i] = new Worker(fullName, startYear, startMonth, company);
        }

        return workers;
    }

    public static void SortAndDisplayWorkersBy(Func<Worker, object> sortingCriterion, Worker[] workers)
    {
        Array.Sort(workers, (x, y) => Comparer<object>.Default.Compare(sortingCriterion(x), sortingCriterion(y)));
        DisplayWorkersData(workers);
    }

    public static void DisplayWorkersData(Worker[] workers)
    {
        foreach (var worker in workers)
        {
            Console.WriteLine($"Працівник: {worker.FullName}");
            Console.WriteLine($"Стаж роботи: {worker.CalculateWorkExperience()} місяців");
            Console.WriteLine($"Зарплата: {worker.Employer.Salary} грн");
            Console.WriteLine($"Посада: {worker.Employer.JobTitle}");
            Console.WriteLine($"Компанія: {worker.Employer.CompanyName}\n");
        }
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.Unicode;
        Console.InputEncoding = Encoding.Unicode;

        Worker[] workers = ReadWorkersData(3); 
        SortAndDisplayWorkersBy(w => w.CalculateWorkExperience(), workers);
    }
}
