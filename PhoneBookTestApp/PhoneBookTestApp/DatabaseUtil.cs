using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace PhoneBookTestApp
{
    public class DatabaseUtil
    {
        public static void initializeDatabase()
        {
            var dbConnection = new SQLiteConnection("Data Source= MyDatabase.sqlite;Version=3;");
            dbConnection.Open();

            try
            {
                //  Check if the table PHONEBOOK exists, if not create it
                SQLiteCommand cmd = new SQLiteCommand("select * from PHONEBOOK", dbConnection);
                SQLiteDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    SQLiteCommand command =
                        new SQLiteCommand(
                            "create table PHONEBOOK (NAME varchar(255), PHONENUMBER varchar(255), ADDRESS varchar(255))",
                            dbConnection);
                    command.ExecuteNonQuery();

                    command =
                        new SQLiteCommand(
                            "INSERT INTO PHONEBOOK (NAME, PHONENUMBER, ADDRESS) VALUES('Chris Johnson','(321) 231-7876', '452 Freeman Drive, Algonac, MI')",
                            dbConnection);
                    command.ExecuteNonQuery();

                    command =
                        new SQLiteCommand(
                            "INSERT INTO PHONEBOOK (NAME, PHONENUMBER, ADDRESS) VALUES('Dave Williams','(231) 502-1236', '285 Huron St, Port Austin, MI')",
                            dbConnection);
                    command.ExecuteNonQuery();
                }

            }
            catch (SQLiteException ex)
            {
                throw new SQLiteException("Unable to create database", ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        /// <summary>
        ///     Add the person to the database
        /// </summary>
        /// <param name="person">A person object</param>
        public static void AddPersonToDB(Person person)
        {
            try
            {
                string sql = "INSERT INTO PHONEBOOK (NAME, PHONENUMBER, ADDRESS) VALUES('@name','(@phonenumber', '@address')";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@name", person.name);
                    cmd.Parameters.AddWithValue("@phonenumber", person.phoneNumber);
                    cmd.Parameters.AddWithValue("@address", person.address);
                    cmd.ExecuteNonQuery();
                };
            }
            catch (SQLiteException ex)
            {
                throw new SQLiteException(ex.ToString());
            }
        }

        /// <summary>
        ///     Returns all people in the phonebook
        /// </summary>
        /// <returns></returns>
        public static List<Person> GetPhoneBook()
        {
            SQLiteDataReader reader = null;
            List<Person> people = new List<Person>();
            try
            {
                string sql = "SELECT NAME, PHONENUMBER, ADDRESS FROM PHONEBOOK";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, GetConnection()))
                {
                    //  Execute reader
                    reader = cmd.ExecuteReader();
                   
                    if (reader.HasRows)
                    { 
                        //  If reader has rows add record to person object
                        while(reader.Read())
                        {
                            Person person = new Person()
                            {
                                name = reader["NAME"].ToString(),
                                phoneNumber = reader["PHONENUMBER"].ToString(),
                                address = reader["ADDRESS"].ToString(),
                            };

                            //  Add person object to people list
                            people.Add(person);
                        }
                    }

                    //  Return list
                    return people;
                };

            }
            catch (SQLiteException ex)
            {
                throw new SQLiteException("Unable to retrieve person records", ex);
            }
        }

        /// <summary>
        ///     Returns a single person from th database
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Returns a person bject containing a single person from the database</returns>
        public static Person GetPerson(string name)
        {
            SQLiteDataReader reader = null;
            Person person = new Person();
            try
            {
                string sql = "SELECT NAME, PHONENUMBER, ADDRESS FROM PHONEBOOK WHERE NAME = '" + name + "'";

                using (SQLiteCommand cmd = new SQLiteCommand(sql, GetConnection()))
                {
                    //  Execute reader
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        new Person
                        {
                            name = reader["NAME"].ToString(),
                            phoneNumber = reader["PHONENUMBER"].ToString(),
                            address = reader["ADDRESS"].ToString(),
                        };
                    }

                    //  Return list
                    return person;
                };

            }
            catch (SQLiteException ex)
            {
                throw new SQLiteException("Unable to retrieve person from table", ex);
            }
        }


        /// <summary>
        ///     Return database connection string
        /// </summary>
        /// <returns>Database connection string</returns>
        public static SQLiteConnection GetConnection()
        {
            var dbConnection = new SQLiteConnection("Data Source= MyDatabase.sqlite;Version=3;");
            dbConnection.Open();

            return dbConnection;
        }

        public static void CleanUp()
        {
            var dbConnection = new SQLiteConnection("Data Source= MyDatabase.sqlite;Version=3;");
            dbConnection.Open();

            try
            {
                SQLiteCommand command =
                    new SQLiteCommand(
                        "drop table PHONEBOOK",
                        dbConnection);
                command.ExecuteNonQuery();
            }
            catch (SQLiteException ex)
            {
                throw new Exception("There has been an error.  Unable to connect to database ", ex);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        /// <summary>
        ///     Check to see if the database exists
        /// </summary>
        /// <param name="server">The server name</param>
        /// <param name="database">The database to check</param>
        /// <returns>Returns boolean depending on if the databse exists or not</returns>
        public static Boolean checkDatabaseExists(string server, string database)
        {
            String connString = ("Data Source =" + (server + "; Initial Catalog = master; Integrated Security = True;"));
            String cmdText = "SELECT name FROM PHONEBOOK ";
            Boolean dbExists = false;

            SQLiteConnection sqlConnection = new SQLiteConnection(connString);
            SQLiteCommand sqlCmd = new SQLiteCommand(cmdText, sqlConnection);
            SQLiteDataReader reader = null;
            try
            {
                sqlConnection.Open();
                reader = sqlCmd.ExecuteReader();
                dbExists = reader.HasRows;
                sqlConnection.Close();
            }
            catch (SQLiteException ex)
            {
                sqlConnection.Close();
                throw new Exception("There has been an error ", ex);
            }
            finally
            {
                sqlCmd.Dispose();
                sqlConnection.Dispose();
            }

           return dbExists;
        }
    }
}