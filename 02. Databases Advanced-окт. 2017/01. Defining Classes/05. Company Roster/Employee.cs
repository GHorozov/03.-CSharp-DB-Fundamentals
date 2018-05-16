public class Employee
{
    private string name;
    private decimal salary;
    private string position;
    private string department;
    private string email = "n/a";
    private int age=-1;

    public Employee(string name, decimal salary, string position, string department)
    {
        this.Name = name;
        this.Salary = salary;
        this.Position = position;
        this.Department = department;
    }

    public Employee(string name, decimal salary, string position, string department, string email, int age) : this(name, salary, position,department)
    {
        this.Email = email;
        this.Age = age;
    } 

    public string Name { get => name; set => name = value; }
    public decimal Salary { get => salary; set => salary = value; }
    public string Position { get => position; set => position = value; }
    public string Department { get => department; set => department = value; }
    public string Email { get => email; set => email = value; }
    public int Age { get => age; set => age = value; }
}

