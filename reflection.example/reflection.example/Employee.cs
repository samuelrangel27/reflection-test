using System;
namespace reflection.example
{
    public class Employee
    {
        public Employee()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }

        private double Salary;

        protected string Department;

        public double CalculateTaxes()
        {
            return Salary * 0.3;
        }
    }
}
