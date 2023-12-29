using System.IO;

public class User
{
    private string name;
    private static string usersPath = Path.Combine(Directory.GetCurrentDirectory(), "Users.txt");

    public User(string name, string password)
    {
        this.name = name;
        if (Login(name, password)) Console.WriteLine("Login successful.\n");
        else Console.WriteLine("User successfully registered.\n");
    }

    public User(string password)
    {
        GenerateUniqueName();
        if (Register(name, password)) Console.WriteLine($"User {name} successfully registered.\n");
        else Console.WriteLine("Error registering user.\n");
    }

    public string GetName()
    {
        return name;
    }

    private bool Login(string name, string password)
    {
        if (IsUserExists(name))
        {
            return true;
        }
        else
        {
            try
            {
                StreamWriter sw = File.AppendText(usersPath);
                sw.WriteLine($"{name} {password}");
                sw.Close();
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
            return false;
        }
    }

    private bool Register(string name, string password)
    {
        if (!IsUserExists(name))
        {
            try
            {
                StreamWriter sw = File.AppendText(usersPath);
                sw.WriteLine($"{name} {password}");
                sw.Close();
                return true;
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"Exception: {e.Message}");
            }
        }
        return false;
    }

    private void GenerateUniqueName()
    {
        string baseName = "User";
        int i = 1;
        while (IsUserExists($"{baseName}{i}"))
        {
            i++;
        }
        name = $"{baseName}{i}";
    }

    private bool IsUserExists(string name)
    {
        StreamReader sr = new StreamReader(usersPath);
        string line = sr.ReadLine();
        while (line != null)
        {
            if (line.Split(' ')[0] == name)
            {
                sr.Close();
                return true;
            }
            line = sr.ReadLine();
        }
        sr.Close();
        return false;
    }
}
