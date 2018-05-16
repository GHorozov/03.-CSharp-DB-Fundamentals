public class Person
{
    private string name;
    private int age;

    public Person(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }

    public string Name { get => name; set => name = value; }
    public int Age { get => age; set => age = value; }
}

