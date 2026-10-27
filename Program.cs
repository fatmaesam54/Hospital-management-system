using System;
using System.Collections.Generic;
using System.Globalization;

namespace OOP_Assignment
{

    #region PART 1 Struct
    internal struct Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public int BuildingNumber { get; set; }

        public Address(string city, string street, int buildingNumber)
        {
            City = city;
            Street = street;
            BuildingNumber = buildingNumber;
        }

        public string GetFullAddress()
        {
            return $"{BuildingNumber}, {Street}, {City}";
        }

        public override string ToString()
        {
            return GetFullAddress();
        }
    }
    #endregion

    #region PART 3 Enums
    internal enum Gender
    {
        Male,
        Female
    }

    internal enum EmployeeRole
    {
        Doctor,
        Nurse,
        Receptionist,
        Administrator
    }

    #endregion

    #region PART 2 Classes 
    internal class Person
    {
        private int id;
        private string name = "Unknown";
        private int age;
        private Address address;
        private Gender gender;

        public int Id
        {
            get { return id; }
            set { id = value < 0 ? 0 : value; }
        }

        public string Name
        {
            get { return name; }
            set { name = string.IsNullOrWhiteSpace(value) ? "Unknown" : value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value < 0 ? 0 : value; }
        }

        public Address Address
        {
            get { return address; }
            set { address = value; }
        }

        public Gender Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public Person() { }

        public Person(int id, string name, int age, Address address, Gender gender)
        {
            Id = id;
            Name = name;
            Age = age;
            Address = address;
            Gender = gender;
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"Gender: {Gender}");
            Console.WriteLine($"Address: {Address.GetFullAddress()}");
        }
    }
    #endregion

    #region PART 5 Hiring Date
    internal class HireDate
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public HireDate(int day, int month, int year)
        {
            Day = day < 1 ? 1 : day;
            Month = (month < 1 || month > 12) ? 1 : month;
            Year = year < 1 ? 1 : year;
        }

        public override string ToString()
        {
            return $"{Day:00}/{Month:00}/{Year}";
        }
    }
    #endregion

    #region PART 6 Interface
    internal interface IPayable
    {
        decimal CalculateSalary();
    }

    #endregion

    #region PART 4 Inheritance
    internal class Employee : Person
    {
        private decimal salary;
        private HireDate hireDate;
        private EmployeeRole employeeRole;
        private static int employeeCount = 0;

        public decimal Salary
        {
            get { return salary; }
            set { salary = value < 0 ? 0 : value; }
        }

        public HireDate HireDate
        {
            get { return hireDate; }
            set { hireDate = value; }
        }

        public EmployeeRole EmployeeRole
        {
            get { return employeeRole; }
            set { employeeRole = value; }
        }

        public Employee(int id, string name, int age, Address address,
                        Gender gender, decimal salary, HireDate hireDate,
                        EmployeeRole employeeRole)
            : base(id, name, age, address, gender)
        {
            Salary = salary;
            HireDate = hireDate;
            EmployeeRole = employeeRole;
            employeeCount++;
        }

        public static int GetEmployeeCount()
        {
            return employeeCount;
        }

        public virtual void PrintWorkDetails()
        {
            Console.WriteLine($"Employee Role: {EmployeeRole}");
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Role: {EmployeeRole}");
            Console.WriteLine($"Salary: {Salary}");
            Console.WriteLine($"Hire Date: {HireDate}");
        }

    #endregion

    #region PART 8 Tostring()
        public override string ToString()
        {
            return String.Format(
                CultureInfo.GetCultureInfo("en-US"),
                "ID: {0}\nName: {1}\nRole: {2}\nSalary: {3:C2}\nHire Date: {4}",
                Id, Name, EmployeeRole, Salary, HireDate);
        }
    }
        #endregion

    #region PART 7 Polymerphism
    internal class Doctor : Employee, IPayable
    {
        public string Specialization { get; set; }
        public string LicenseNumber { get; set; }

        public Doctor(int id, string name, int age, Address address,
                      Gender gender, decimal salary, HireDate hireDate,
                      string specialization, string licenseNumber)
            : base(id, name, age, address, gender, salary, hireDate,
                   EmployeeRole.Doctor)
        {
            Specialization = specialization;
            LicenseNumber = licenseNumber;
        }

        public decimal CalculateSalary()
        {
            return Salary + HospitalHelper.CalculateBonus(Salary, 20);
        }

        public sealed override void PrintWorkDetails()
        {
            Console.WriteLine($"Doctor: {Name}, Specialization: {Specialization}");
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Specialization: {Specialization}");
            Console.WriteLine($"License Number: {LicenseNumber}");
        }

        public override string ToString()
        {
            return base.ToString() +
                   $"\nSpecialization: {Specialization}" +
                   $"\nLicense Number: {LicenseNumber}";
        }
    }

    // ================= Nurse =================
    internal class Nurse : Employee, IPayable
    {
        public string Department { get; set; }

        public Nurse(int id, string name, int age, Address address,
                     Gender gender, decimal salary, HireDate hireDate,
                     string department)
            : base(id, name, age, address, gender, salary, hireDate,
                   EmployeeRole.Nurse)
        {
            Department = department;
        }

        public decimal CalculateSalary()
        {
            return Salary + HospitalHelper.CalculateBonus(Salary, 10);
        }

        public override void PrintWorkDetails()
        {
            Console.WriteLine($"Nurse: {Name}, Department: {Department}");
        }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Department: {Department}");
        }

        public override string ToString()
        {
            return base.ToString() + $"\nDepartment: {Department}";
        }
    }

    // ================= Receptionist =================
    internal class Receptionist : Employee
    {
        public Receptionist(int id, string name, int age, Address address,
                            Gender gender, decimal salary, HireDate hireDate)
            : base(id, name, age, address, gender, salary, hireDate,
                   EmployeeRole.Receptionist)
        { }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine("Work: Reception");
        }

        public override void PrintWorkDetails()
        {
            Console.WriteLine($"Receptionist: {Name}");
        }
    }

    // ================= Administrator =================
    internal class Administrator : Employee
    {
        public Administrator(int id, string name, int age, Address address,
                             Gender gender, decimal salary, HireDate hireDate)
            : base(id, name, age, address, gender, salary, hireDate,
                   EmployeeRole.Administrator)
        { }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine("Work: Administration");
        }

        public override void PrintWorkDetails()
        {
            Console.WriteLine($"Administrator: {Name}");
        }
    }
    #endregion

    #region PART 9 Static
    internal static class HospitalHelper
    {
        public static decimal CalculateBonus(decimal salary, decimal percentage)
        {
            return salary * percentage / 100;
        }

        public static decimal CalculateTax(decimal salary, decimal percentage)
        {
            return salary * percentage / 100;
        }

        public static bool IsAdult(int age)
        {
            return age >= 18;
        }
    }

    #endregion

    #region PART 10 Sealed
    internal sealed class HospitalSystem
    {
        public void DisplayHospitalInfo()
        {
            Console.WriteLine("Smart Hospital Management System");
        }

        public void ManageEmployee(Employee employee)
        {
            Console.WriteLine($"Managing Employee: {employee.Name}");
        }
    }
    #endregion

    #region PART 11 Sorting
    internal class EmployeeHireDateComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            if (x == null && y == null) return 0;
            if (x == null) return -1;
            if (y == null) return 1;

            if (x.HireDate.Year != y.HireDate.Year)
                return x.HireDate.Year.CompareTo(y.HireDate.Year);

            if (x.HireDate.Month != y.HireDate.Month)
                return x.HireDate.Month.CompareTo(y.HireDate.Month);

            return x.HireDate.Day.CompareTo(y.HireDate.Day);
        }
    }

    #endregion

    #region PART 12 Final integration
    internal class Program
    {
        static void Main(string[] args)
        {
            Address address1 = new Address("Sohag", "Main Street", 10);
            Address address2 = new Address("Cairo", "Nile Street", 20);

            Employee doctor = new Doctor(
                1, "Ahmed", 35, address1, Gender.Male, 25000,
                new HireDate(10, 5, 2022), "Cardiology", "D1001");

            Employee nurse = new Nurse(
                2, "Mona", 28, address2, Gender.Female, 12000,
                new HireDate(15, 8, 2023), "Emergency");

            Employee receptionist = new Receptionist(
                3, "Ali", 25, address1, Gender.Male, 8000,
                new HireDate(20, 2, 2021));

            Employee administrator = new Administrator(
                4, "Sara", 30, address2, Gender.Female, 15000,
                new HireDate(1, 1, 2024));

            Employee doctor2 = new Doctor(
                5, "Omar", 40, address1, Gender.Male, 30000,
                new HireDate(5, 3, 2020), "Surgery", "D2001");

            Employee[] employees =
            {
                doctor, nurse, receptionist, administrator, doctor2
            };

            #region PART 07 Polymorphism

            Console.WriteLine("=== Runtime Polymorphism ===");

            foreach (Employee employee in employees)
            {
                employee.PrintInfo();
                employee.PrintWorkDetails();
                Console.WriteLine(employee);
                Console.WriteLine("----------------------");
            }

            Employee emp = new Doctor(
                6, "Khaled", 38, address1, Gender.Male, 27000,
                new HireDate(12, 4, 2021), "Neurology", "D3001");

            emp.PrintWorkDetails();

            Console.WriteLine("\n=== Interface Polymorphism ===");

            IPayable payable = (IPayable)doctor;
            Console.WriteLine($"Doctor Salary: {payable.CalculateSalary():C2}");

            payable = (IPayable)nurse;
            Console.WriteLine($"Nurse Salary: {payable.CalculateSalary():C2}");

            #endregion

            Console.WriteLine("\n=== Employee Counter ===");
            Console.WriteLine($"Total Employees: {Employee.GetEmployeeCount()}");

            Console.WriteLine("\n=== Hospital Helper ===");

            decimal bonus = HospitalHelper.CalculateBonus(25000, 20);
            decimal tax = HospitalHelper.CalculateTax(25000, 10);
            bool adult = HospitalHelper.IsAdult(25);

            Console.WriteLine($"Bonus: {bonus:C2}");
            Console.WriteLine($"Tax: {tax:C2}");
            Console.WriteLine($"Is Adult: {adult}");

            Console.WriteLine("\n=== Hospital System ===");

            HospitalSystem hospital = new HospitalSystem();
            hospital.DisplayHospitalInfo();
            hospital.ManageEmployee(doctor);

            Console.WriteLine("\n=== Sorted Employees ===");

            Array.Sort(employees, new EmployeeHireDateComparer());

            foreach (Employee employee in employees)
            {
                Console.WriteLine($"{employee.Name} - {employee.HireDate}");
            }

            Console.WriteLine("\n=== Boxing / Unboxing ===");
            Console.WriteLine("Boxing operations during sorting: 0");
            Console.WriteLine("Unboxing operations during sorting: 0");

            Console.WriteLine("\nProgram Finished.");
        }
    }
    #endregion

}