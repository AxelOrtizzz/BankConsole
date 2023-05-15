namespace BankConsole;

public abstract class Person
{
    //Metodos
    public abstract string GetName();

    public string GetCountry()
    {
        return "Mexico";
    }
}

public interface IPerson
{
    string GetName();
    string GetCountry();
}