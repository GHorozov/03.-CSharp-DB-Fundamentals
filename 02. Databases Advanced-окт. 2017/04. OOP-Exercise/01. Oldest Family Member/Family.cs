using System;
using System.Collections.Generic;
using System.Linq;

public class Family
{
    private List<Person> listOfPeople;

    public Family()
    {
        this.listOfPeople = new List<Person>();
    }

    public void AddMember(Person member)
    {
        this.listOfPeople.Add(member);
    }

    public Person GetOldestMember()
    {
        var old = listOfPeople.OrderByDescending(x => x.Age).FirstOrDefault();

        return old;
    }
}

