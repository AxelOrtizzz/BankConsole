using BankConsole;
using System.Text.RegularExpressions;

if (args.Length == 0){
    EmailService.SendMail();
}else{
    Console.WriteLine("Mostrar menú...");
    ShowMenu();
}

//Metodo Menú

void ShowMenu()
{
    Console.Clear();
    Console.WriteLine("Selecciona la opción: ");
    Console.WriteLine("1 - Crear un Usuario Nuevo.");
    Console.WriteLine("2 - Eliminar un Usuario existente.");
    Console.WriteLine("3 - Salir.");

    int option = 0;

    do
    {
        string input = Console.ReadLine();

        if(!int.TryParse(input, out option))
            Console.WriteLine("Debes ingresar un número (1, 2 o 3).");
        else if(option > 3)
            Console.WriteLine("Debes ingresar un número válido (1, 2 o 3).");
    }while(option == 0 || option > 3);

    switch (option)
    {
        case 1:
            CreateUser();
            break;
        case 2:
            DeleteUser();
            break;
        case 3:
            Environment.Exit(0);
            break;
    }
}

void CreateUser(){
    Console.Clear();
    Console.WriteLine("Ingrese la información del usuario: ");

    int ID;
    bool valid = false;

    do
    {
        Console.Write("ID: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out ID) || ID <= 0)
        {
            Console.WriteLine("El ID debe ser un número entero positivo");
            ID = 0;
        }

        valid = Storage.RepitUser(ID);
        if(valid){
            Console.WriteLine("Este ID ya existe");
        }
    } while (ID <= 0 || valid);

    Console.Write("Nombre: ");
    string name = Console.ReadLine();

    string email;
    bool isValidEmail;
    do{
        Console.Write("Email: ");
        email = Console.ReadLine();
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        isValidEmail = Regex.IsMatch(email, pattern);

        if (!isValidEmail)
        {
            Console.WriteLine("El correo electrónico no es válido");
        }
    }while(!isValidEmail);

    decimal balance;
    do{
        Console.Write("Saldo: ");
        string input = Console.ReadLine();

        if (!(decimal.TryParse(input, out balance) && balance > 0))
        {
            Console.WriteLine("El saldo debe de ser un decimal positivo válido");
        } 
    }while(balance < 0);

    char userType;
    do{
        Console.Write("Escribe 'c' si el usuario es Cliente, 'e' si es Empleado: ");
        userType = char.Parse(Console.ReadLine());

        if(userType != 'c' && userType != 'e'){
            Console.WriteLine("ERROR!, debe ingresar 'c' o 'e' segun sea el caso");
        }
    }while(userType != 'c' && userType != 'e');

    User newUser;

    if(userType.Equals('c'))
    {
        Console.Write("Regimen Fiscal: ");
        char taxRegime = char.Parse(Console.ReadLine());

        newUser = new Client(ID, name, email, balance, taxRegime);
    } 
    else
    {
        Console.Write("Departamento: ");
        string department = Console.ReadLine();

        newUser = new Employee(ID, name, email, balance, department);
    }

    Storage.AddUser(newUser);

    Console.WriteLine("Usuario creado con exito");
    Thread.Sleep(2000);
    ShowMenu();
}

void DeleteUser(){

    Console.Clear();




    int ID;
    bool valid = false;

    do
    {
        Console.Write("ID: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out ID) || ID <= 0)
        {
            Console.WriteLine("El ID debe ser un número entero positivo");
            ID = 0;
        }

        string result = Storage.DeleteUser(ID);

        if (result.Equals("Success"))
        {
            Console.Write("Usuario Eliminado");
            Thread.Sleep(2000);
            valid = true;
            ShowMenu();
        }else if(result.Equals("NotSuccess"))
        {
            Console.Write("Usuario no encontrado");
            Thread.Sleep(1000);
            valid = false;
        }
    } while (!valid);
}