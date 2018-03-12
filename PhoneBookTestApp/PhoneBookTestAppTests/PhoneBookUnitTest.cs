using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneBookTestApp;

namespace PhoneBookTestAppTests
{
    [TestClass]
    public class PhoneBookUnitTest
    {
        PhoneBook phonebook = new PhoneBook();

        [TestMethod]
        public void addPerson()
        {
            Person testEntity = new Person()
            {
                name = "John Smith",
                address = "1234 Sand Hill Dr, Royaml Oak, MI",
                phoneNumber = "(248) 123-4567"
            };

            //  Check thats the database exists
            Assert.IsTrue(DatabaseUtil.checkDatabaseExists("MyDatabase.sqlite", "MyDatabase"));

            //Add person to database
            phonebook.AddPerson(testEntity);

            //  return the person and check 
            Person returnedEntity = phonebook.FindPerson("John", "smith");
            Assert.IsNotNull(returnedEntity);
            Assert.AreSame("John Smith", returnedEntity.name);
            Assert.AreSame("1234 Sand Hill Dr, Royaml Oak, MI", returnedEntity.address);
            Assert.AreSame("(248) 123-4567", returnedEntity.phoneNumber);
        }

        [TestMethod]
        public void findPerson()
        {
            Person cynthiaSmith = new Person()
            {
                name = "Cynthia Smith",
                address = "875 Main St, Ann Arbor, MI",
                phoneNumber = "(824) 128-8758"
            };

            //  Check thats the database exists
            Assert.IsTrue(DatabaseUtil.checkDatabaseExists("MyDatabase.sqlite", "MyDatabase"));

            //Add person to database
            phonebook.AddPerson(cynthiaSmith);

            //  return the person and check 
            Person returnedEntity = phonebook.FindPerson("Cynthia", "smith");
            Assert.IsNotNull(returnedEntity);
            Assert.AreSame("Cynthia Smith", returnedEntity.name);
            Assert.AreSame("875 Main St, Ann Arbor, MI", returnedEntity.address);
            Assert.AreSame("(824) 128-8758", returnedEntity.phoneNumber);
        }

        [TestMethod]
        public void returnPhoneBook()
        {
            //  Add two people to the database
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

            //Add person to database
            phonebook.AddPerson(johnSmith);
            phonebook.AddPerson(cynthiaSmith);

            //  Get all people from phonebook
            List<Person> people = phonebook.GetPeople();

            Assert.AreEqual(2, people.Count);
        }
    }
}
