using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    public static void Main(string[] args)
    {
        try
        {
            var studentInfo = Console.ReadLine().Split();
            var firstName = studentInfo[0];
            var lastName = studentInfo[1];
            var facultyNumber = studentInfo[2];

            var student = new Student(firstName, lastName, facultyNumber);

            var workerInfo = Console.ReadLine().Split();
            var workerFirstName = workerInfo[0];
            var workerLastName = workerInfo[1];
            var salary = decimal.Parse(workerInfo[2]);
            var workingHours = double.Parse(workerInfo[3]);

            var worker = new Worker(workerFirstName, workerLastName, salary, workingHours);

            Console.WriteLine(student);
            Console.WriteLine();
            Console.WriteLine(worker);

        }
        catch(ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }

    }
}
