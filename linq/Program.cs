using System;
using System.Linq;
using System.Globalization;
using System.IO;
using linq.Entities;
using System.Collections.Generic;

namespace linq
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter full file path: ");
            string path = Console.ReadLine();

            Console.Write("Enter salary: ");
            double salary = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            List<Employee> list = new List<Employee>();

            using (StreamReader sr = File.OpenText(path))
            {
                while (!sr.EndOfStream)
                {
                    string[] fields = sr.ReadLine().Split(",");
                    string name = fields[0];
                    string email = fields[1];
                    double price = double.Parse(fields[2], CultureInfo.InvariantCulture);
                    list.Add(new Employee(name, email, price));
                }
            }

            var moreThanReference = list.Where(p => p.Salary > salary).OrderBy(p => p.Name).Select(p => p.Email);
            Console.WriteLine("Email of people whose salary is more than " + salary.ToString("F2", CultureInfo.InvariantCulture));
            foreach (string personEmail in moreThanReference)
            {
                Console.WriteLine(personEmail);
            }

            var salarySum = list.Where(obj => obj.Name[0] == 'M').Sum(obj => obj.Salary);

            Console.WriteLine("Sum of salary of people whose name starts with 'M': " + salarySum.ToString("F2", CultureInfo.InvariantCulture));
        }
    }
}
