using System;
using System.Reflection;

namespace reflection.example
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Let's talk about reflection");
            Console.WriteLine("1. First we're gonna create an object at runtime");
            var employee = createObjectAtRuntime();
            Console.Read();
            
            Console.WriteLine("2. Now let's view its fields/properties metadata");
            getViewTypeInformation(employee);
            Console.Read();

            Console.WriteLine("Let's set properties and fields values");
            setFieldsPropertiesAtRuntime(employee);
            Console.Read();

            Console.WriteLine("These are the values we set");
            PrintFieldProperties(employee);

            Console.WriteLine("Last thing we'll do is to invoke a method at runtime");
            invokeMethod(employee);

            Console.WriteLine("That is all Folks!! Enjoy reflection, it's a really powerful tool!!");
            
        }

        static Employee createObjectAtRuntime()
        {
            string assemblyPath = Environment.CurrentDirectory + "\\reflection.example.dll";
            Assembly assembly;

            assembly = Assembly.LoadFrom(assemblyPath);
            Type type = assembly.GetType("reflection.example.Employee");
            return Activator.CreateInstance(type) as Employee;
        }

        static void getViewTypeInformation(Employee employee)
        {
            Type type = employee.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            Console.WriteLine("========= Fields =========");
            PrintMembers(fields);

            Console.WriteLine("========= Properties =========");
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            PrintMembers(properties);

            Console.WriteLine("========= Methods =========");
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            PrintMembers(methods);

        }

        static void PrintMembers(MemberInfo[] ms)
        {
            foreach (MemberInfo m in ms)
            {
                Console.WriteLine("{0}{1}", "     ", m);
            }
            Console.WriteLine();
        }

        static void setFieldsPropertiesAtRuntime(Employee employee)
        {
            Type type = employee.GetType();

            // Id property
            PropertyInfo idProp = type.GetProperty("Id");
            idProp.SetValue(employee, 1);

            // Name property
            PropertyInfo nameProp = type.GetProperty("Name");
            nameProp.SetValue(employee,"Jhon doe");

            // Salary field
            FieldInfo salaryField = type.GetField("Salary", BindingFlags.NonPublic | BindingFlags.Instance);
            salaryField.SetValue(employee, 1000);

            // Department field
            FieldInfo departmentField = type.GetField("Department", BindingFlags.NonPublic | BindingFlags.Instance);
            departmentField.SetValue(employee, "Development");
        }

        static void PrintFieldProperties(Employee employee)
        {
            Type type = employee.GetType();
            var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            Console.WriteLine("***** Properties *****");
            foreach (var p in properties)
            {
                Console.WriteLine(string.Format("Property:{0} - Value:{1}", p.Name, p.GetValue(employee)));
            }

            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
            foreach (var f in fields)
            {
                Console.WriteLine(string.Format("Field:{0} - Value:{1}", f.Name, f.GetValue(employee)));
            }
        }

        static void invokeMethod(Employee employee)
        {
            Type type = employee.GetType();
            MethodInfo methodInfo = type.GetMethod("CalculateTaxes");
            var result = methodInfo.Invoke(employee, null);
            Console.WriteLine(string.Format("Result for calculate taxes is {0}", result));
        }
    }
}
