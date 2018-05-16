using System;

public abstract class Animal : ISoundProducable
{
    private string name;
    private int age;
    private string gender;

    public Animal(string name, int age, string gender)
    {
        this.Name = name;
        this.age = age;
        this.Gender = gender;
    }

    public string Name { get => name; set => name = value; }
    public int Age { get => age; set => age = value; }
    public string Gender { get => gender; set => gender = value; }

    string IAnimal.name => throw new NotImplementedException();

    string ISoundProducable.name => throw new NotImplementedException();

    int IAnimal.age => throw new NotImplementedException();

    int ISoundProducable.age => throw new NotImplementedException();

    string IAnimal.gender => throw new NotImplementedException();

    string ISoundProducable.gender => throw new NotImplementedException();

    public abstract string ProduceSound();
}

