namespace BankConsole;

public class Client : User, IPerson
{
    public char TaxRegime { get; set; }

    //constructores
    public Client(){}
    public Client(int ID, string Name, string Email, decimal Balance, char TaxRegime) : base(ID, Name, Email, Balance)
    {
        this.TaxRegime = TaxRegime;
        setBalance(Balance);
    }

    //metodos
    public override void setBalance(decimal amount)
    {
        base.setBalance(amount);

        if (TaxRegime.Equals('M'))
        {
            Balance += (amount * 0.02m);
        }
    }

    public override string ShowDate()
    {
        return base.ShowDate() + $", Régimen fiscal: {this.TaxRegime}";
    }

    public string GetName()
    {
        return Name + ", Cliente";
    }

    public string GetCountry()
    {
        return "Mexico, Cliente";
    }
}