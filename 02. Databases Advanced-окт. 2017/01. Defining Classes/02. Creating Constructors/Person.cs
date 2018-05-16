class Person
{
    private string name = "No name";
    private int age = 1;

    public Person()
    {
        this.Name = name;
        this.Age = 1;
    }

    public Person(int age)
    {
        this.Name = name;
        this.Age = age;
    }

    public Person(string name, int age) 
    {
        this.Name = name;
        this.Age = age;
    }


    public string  Name { get; set; }
    public int Age { get; set; }
}

