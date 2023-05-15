using Newtonsoft.Json;
namespace BankConsole;

public class User
{
    //Propiedades
    [JsonProperty]
    protected int ID {get; set;}
    [JsonProperty]
    protected string Name { get; set; }
    [JsonProperty]
    protected string Email { get; set; }
    [JsonProperty]
    protected decimal Balance { get; set; }
    [JsonProperty]
    protected DateTime RegisterDate { get; set; }
    

    //constructor
    public User(){}

    public User(int ID, string Name, string Email, decimal Balance){
        this.ID = ID;
        this.Name = Name;
        this.Email = Email;
        this.RegisterDate = DateTime.Now;

    }

    //Metodos
    public int GetID(){
        return ID;
    }
    public DateTime GetRegisterDate(){
        return RegisterDate;
    }
    public virtual string ShowDate(){
        return $"ID: {this.ID}, Nombre: {this.Name}, Correo: {this.Email}, Saldo: {this.Balance}, Fecha de registro: {this.RegisterDate}";
    }

    public string ShowDate(string initialMessage){
        return $"{initialMessage} -> Nombre: {this.Name}, Correo: {this.Email}, Saldo: {this.Balance}, Fecha de registro: {this.RegisterDate.ToShortDateString()}";
    }

    public virtual void setBalance(decimal amount){
        decimal quantity = 0;
        if(amount < 0){
            quantity = 0;
        }else{
            quantity = amount;
        }
        this.Balance += quantity;
    }

    //Metodos de la relacion Interface
}