using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BankConsole;

public static class Storage
{
    static string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\users.json";

    public static void AddUser(User user)
    {
        string json = "", usersInFile = "";

        if (File.Exists(filePath))
        {
            usersInFile = File.ReadAllText(filePath);
        }

        var listUsers = JsonConvert.DeserializeObject<List<object>>(usersInFile);

        if (listUsers == null)
        {
            listUsers = new List<object>();
        }

        listUsers.Add(user);

        JsonSerializerSettings settings = new JsonSerializerSettings{ Formatting = Formatting.Indented };

        json = JsonConvert.SerializeObject(listUsers, settings);

        File.WriteAllText(filePath, json);
    }

    public static List<User> GetNewUsers()
    {
        string usersInFile = "";
        var listUsers = new List<User>();

        if (File.Exists(filePath))
        {
            usersInFile = File.ReadAllText(filePath);
        }

        var listObjetcs = JsonConvert.DeserializeObject<List<object>>(usersInFile);

        if (listObjetcs == null)
        {
            return listUsers;
        }

        foreach(Object obj in listObjetcs){
            User newUser;
            JObject user = (JObject)obj;

            if(user.ContainsKey("TaxRegime"))
                newUser = user.ToObject<Client>();
            else
                newUser= user.ToObject<Employee>();

            listUsers.Add(newUser);    
        }

        var newUserList = listUsers.Where(user => user.GetRegisterDate().Date.Equals(DateTime.Today)).ToList();

        return newUserList;
    }

    public static string DeleteUser(int ID)
    {
        string usersInFile = "";
        var listUsers = new List<User>();

        if (File.Exists(filePath))
        {
            usersInFile = File.ReadAllText(filePath);
        }

        var listObjetcs = JsonConvert.DeserializeObject<List<object>>(usersInFile);

        if (listObjetcs == null)
        {
            return "No hay usuarios en el archivo";
        }

        foreach(Object obj in listObjetcs){
            User newUser;
            JObject user = (JObject)obj;

            if(user.ContainsKey("TaxRegime"))
                newUser = user.ToObject<Client>();
            else
                newUser= user.ToObject<Employee>();

            listUsers.Add(newUser);    
        }

        var userToDelete = listUsers.Where(user => user.GetID() == ID).FirstOrDefault();
        if (userToDelete != null)
        {
            listUsers.Remove(userToDelete);
        }
        else
        {
            return"NotSuccess";
        }
        

        JsonSerializerSettings settings = new JsonSerializerSettings{ Formatting = Formatting.Indented };

        string json = JsonConvert.SerializeObject(listUsers, settings);

        File.WriteAllText(filePath, json);

        return "Success";  
    }

    public static bool RepitUser(int ID)
    {
        bool usuarioRepetido = false; // Variable para indicar si se encontró o no un usuario con el mismo ID

        // Leer el archivo JSON y deserializarlo a una lista de objetos User
        string jsonString = File.ReadAllText(filePath);
        var listaUsuarios = JsonConvert.DeserializeObject<List<User>>(jsonString);

        // Buscar si existe otro usuario con el mismo ID
        foreach (var usuario in listaUsuarios)
        {
            if (usuario.GetID() == ID)
            {
                usuarioRepetido = true;
                break;
            }
        }

        // Devolver true si se encontró un usuario con el mismo ID, false de lo contrario
        return usuarioRepetido;
    }
}