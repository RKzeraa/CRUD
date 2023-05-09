using System.Globalization;
using System.Text.RegularExpressions;

internal class UserController : IUserController
{
    static List<User> users = new List<User>();
    public void AddUser()
    {
        string name;
        string email;
        string birthDate;

        do
        {
            Console.Write("\nEnter your name: ");
            name = Console.ReadLine()!;
        } while (string.IsNullOrWhiteSpace(name) || NameIsExist(name));

        do
        {
            Console.Write("\nEnter your email: ");
            email = Console.ReadLine()!;
            // bool ok = Regex.IsMatch(email, @"/^[a-z0-9.]+@[a-z0-9]+\.[a-z]+\.([a-z]+)?$/i");
            if (!IsValidEmail(email))
                Console.WriteLine("\nEmail format is invalid!");
        } while (!IsValidEmail(email));

        do
        {
            Console.Write("\nEnter your birthDate (Day/Month/Year): ");
            birthDate = Console.ReadLine()!;
        } while (string.IsNullOrWhiteSpace(birthDate));

        User user = new User(name, email, birthDate);
        users.Add(user);

        Console.WriteLine("\nUser added successfully!");
    }

    public void EditUser()
    {
        int id;
        string name;
        string email;
        string birthDate;

        do
        {
            Console.Write("\nEnter the ID of the user to edit: ");
            id = Convert.ToInt32(Console.ReadLine());
        } while (id == 0);

        User user = users.Find(u => u.Id == id)!;

        if (user == null)
        {
            Console.WriteLine("\nUser not found!");
            return;
        }

        do
        {
            Console.Write("\nEnter the new name: ");
            name = Console.ReadLine()!;
        } while (string.IsNullOrWhiteSpace(name) || NameIsExist(name));

        do
        {
            Console.Write("\nEnter the new email: ");
            email = Console.ReadLine()!;
            if (!IsValidEmail(email))
                Console.WriteLine("\nEmail format is invalid!");
        } while (!IsValidEmail(email));

        do
        {
            Console.Write("\nEnter the new birthdate (Day/Month/Year): ");
            birthDate = Console.ReadLine()!;
        } while (string.IsNullOrWhiteSpace(birthDate));

        user.Name = name;
        user.Email = email;
        user.BirthDate = DateTime.Parse(birthDate);
        user.CreatedUserDate = DateTime.Now;

        Console.WriteLine("\nUser edited successfully!");
    }

    public void DeleteUser()
    {
        Console.Write("\nEnter the ID of the user to delete: ");
        int id = Convert.ToInt32(Console.ReadLine());

        User user = users.Find(u => u.Id == id)!;

        if (user == null)
        {
            Console.WriteLine("\nUser not found!");
            return;
        }

        users.Remove(user);

        Console.WriteLine("\nUser deleted successfully!");
    }

    public void ListAllUsers()
    {
        if (users.Count > 0)
        {
            Console.WriteLine("\nAll users registered:");

            foreach (User user in users)
            {
                Console.WriteLine(user);
            }
        }
        else
        {
            Console.WriteLine("\nList is Empty, Create a new user to list all users");
        }
    }

    public void ListByName()
    {
        Console.Write("\nEnter the name to be searched: ");
        string name = Console.ReadLine()!;

        List<User> usersFound = users.FindAll(u => u.Name.ToLower() == name.ToLower());

        if (usersFound.Count == 0)
        {
            Console.WriteLine("\nNo user was found with the given name!");
            return;
        }

        Console.WriteLine($"\nUsers found with the name '{name}':");

        foreach (User user in usersFound)
        {
            Console.WriteLine(user);
        }
    }

    public void ListById()
    {
        Console.Write("\nEnter the ID to be searched: ");
        int id = Convert.ToInt32(Console.ReadLine());

        User user = users.Find(u => u.Id == id)!;

        if (user == null)
        {
            Console.WriteLine("\nUser not found!");
            return;
        }

        Console.WriteLine($"\nUser found:");
        Console.WriteLine(user);
    }

    public void ListByBirthDate()
    {
        Console.Write("\nEnter the birthdate to be searched (Day/Month/Year):");
        string birthDate = Console.ReadLine()!;

        List<User> usersFound = users.FindAll(u => u.BirthDate == DateTime.Parse(birthDate));

        if (usersFound.Count == 0)
        {
            Console.WriteLine("\nNo user was found with the given birthdate!");
            return;
        }

        Console.WriteLine($"\nUsers found with the birthdate  '{birthDate}':");

        foreach (User user in usersFound)
        {
            Console.WriteLine(user);
        }
    }

    public void ListOlderUser()
    {
        if (users.Count > 0)
        {
            User olderUser = users[0];

            foreach (User user in users)
            {
                if (DateTime.Now.Subtract(user.BirthDate) > DateTime.Now.Subtract(olderUser.BirthDate))
                {
                    olderUser = user;
                }
            }

            Console.WriteLine("\nUser oldest registered user:");
            Console.WriteLine(olderUser);
        }
        else
        {
            Console.WriteLine("\nNo user was registered!");
        }

    }

    public Boolean NameIsExist(string name)
    {

        List<User> usersFound = users.FindAll(u => u.Name.ToLower().Contains(name.ToLower()));

        if (usersFound.Count == 0)
        {
            return false;
        }

        Console.WriteLine($"\nUsers found with the name '{name}', you need to other name!");
        return true;
    }

    private static bool IsValidEmail(string email)
    {
        Regex emailRx = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", RegexOptions.IgnoreCase);
        return emailRx.IsMatch(email);
        // try
        // {
        //     var addr = new System.Net.Mail.MailAddress(email);
        //     return addr.Address == email;
        // }
        // catch
        // {
        //     return false;
        // }
    }
}

