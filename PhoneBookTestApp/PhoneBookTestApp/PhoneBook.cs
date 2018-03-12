using System;
using System.Collections.Generic;

namespace PhoneBookTestApp
{
    public class PhoneBook : IPhoneBook   
    {
        /// <summary>
        ///     Add a person to the database
        /// </summary>
        /// <param name="person">Person object that holds persons details</param>
        public void AddPerson(Person person)
        { 
            try
            {
                //  Check if the database exists
                bool dbExists = DatabaseUtil.checkDatabaseExists("MyDatabase.sqlite", "MyDatabase");

                if(!dbExists)
                {
                    //  The database does not exist so create it
                    DatabaseUtil.initializeDatabase();
                    //  New add the person into the database
                    DatabaseUtil.AddPersonToDB(person);
                }
                else
                {
                    //  The database exists so add person record to table
                    DatabaseUtil.AddPersonToDB(person);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to add person " + person.name + " to list", ex);
            }
        }

        /// <summary>
        ///     Gets all people in the Person table
        /// </summary>
        /// <returns>Returns a list of Person objects</returns>
        public List<Person> GetPeople()
        {
            List<Person> personList = new List<Person>();
            try
            {
                personList = DatabaseUtil.GetPhoneBook();
                return personList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to retrieve person records from the database",ex);
            }
        }
        

        /// <summary>
        ///     Find a person in the phonebook
        /// </summary>
        /// <param name="Name"></param>
        /// <returns>Returns a person object</returns>
        public Person FindPerson(string firstName, string lastName)
        {
            try
            {
                //  Get Cynthias' record
                Person cynthia = DatabaseUtil.GetPerson(firstName + " " + lastName);
                return cynthia;
            }
            catch (Exception ex)
            {
                throw new Exception("There has been an error ", ex);
            }
        }
    }
}