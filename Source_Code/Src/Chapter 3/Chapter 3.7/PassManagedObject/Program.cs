using System;
using System.Collections.Generic;
using System.Text;

namespace PassManagedObject
{
    class Program
    {
        static void Main(string[] args)
        {
            ManagedObjectInCallback managedObject = new ManagedObjectInCallback();
            managedObject.Test();

            Console.WriteLine("\r\n��������˳�...");
            Console.Read();
        }
    }
}
