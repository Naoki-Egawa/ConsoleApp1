using System;
using System.Diagnostics;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.WriteLine("eee");
            Debug.WriteLineIf(1 == 2, "test");

        }
    }
}
