using System;
using System.Globalization;

public class DateModifier
{
    public int GetDateDifference(string firstDate, string secondDate)
    {
        var d1 = DateTime.ParseExact(firstDate, "yyyy MM dd", CultureInfo.InvariantCulture);
        var d2 = DateTime.ParseExact(secondDate, "yyyy MM dd", CultureInfo.InvariantCulture);

        if(d1 > d2)
        {
            return (d1 - d2).Days;
        }

        return (d2 - d1).Days;
    }
}

