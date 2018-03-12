using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PhoneBookTestApp
{
    class Program
    {

        static void Main(string[] args)
        {
            PhoneBook phonebook = new PhoneBook();
            try
            {
                /* TODO: create person objects and put them in the PhoneBook and database
                * John Smith, (248) 123-4567, 1234 Sand Hill Dr, Royal Oak, MI
                * Cynthia Smith, (824) 128-8758, 875 Main St, Ann Arbor, MI
                */

                //  The database does not exist so create it
                DatabaseUtil.initializeDatabase();

                //  Create person objects
                Person johnSmith = new Person()
                {
                    name = "John Smith",
                    address = "1234 Sand Hill Dr, Royaml Oak, MI",
                    phoneNumber = "(248) 123-4567"
                };

                Person cynthiaSmith = new Person()
                {
                    name = "Cynthia Smith",
                    address = "875 Main St, Ann Arbor, MI",
                    phoneNumber = "(824) 128-8758"
                };


                // Insert the new person objects into the database
                phonebook.AddPerson(johnSmith);
                phonebook.AddPerson(cynthiaSmith);

                // Print the phone book out to System.out
                foreach (var person in phonebook.GetPeople())
                {
                    //  Write out list of people to console
                    Console.WriteLine("Name: {0}, Number: {1}, Address: {2}\n", person.name, person.phoneNumber, person.address);
                }


                // Find Cynthia Smith and print out just her entry
                Person cynthia = phonebook.FindPerson("Cynthia", "Smith");
                //  Print out Cynthias' details to console
                Console.WriteLine("Name: {0}, Number: {1}, Address: {2}\n", cynthia.name, cynthia.phoneNumber, cynthia.address);
            }
            finally
            {
                DatabaseUtil.CleanUp();
            }
        }
    }
}
