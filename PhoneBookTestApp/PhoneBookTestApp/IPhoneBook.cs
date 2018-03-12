namespace PhoneBookTestApp
{
    public interface IPhoneBook
    {
        Person FindPerson(string firstName, string lastName);
        void AddPerson(Person newPerson);
    }
}