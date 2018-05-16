using System;
using P02_DatabaseFirst.Data;

namespace P02_DatabaseFirst
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            var db = new SoftUniContext();
        }
    }
}
