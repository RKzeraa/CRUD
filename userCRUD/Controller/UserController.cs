using System.Globalization;
using System.Text.RegularExpressions;

internal class UserController : IUserController
{
    static List<User> users = new List<User>();
    private MensageGenerics<string> show = new MensageGenerics<string>();
    public void AddUser()
    {
        string name;
        string email;
        string birthDate;

        do
        {
            show.Mensage("your name: ");
            name = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(name))
            {
                show.MensageError("Name", 1);
            }
        } while (string.IsNullOrWhiteSpace(name) || NameIsExist(name));

        do
        {
            show.Mensage("your email: ");
            email = Console.ReadLine()!;
            // bool ok = Regex.IsMatch(email, @"/^[a-z0-9.]+@[a-z0-9]+\.[a-z]+\.([a-z]+)?$/i");
            if (!IsValidEmail(email))
            {
                show.MensageError("Email", 1);
            }
        } while (!IsValidEmail(email));

        do
        {
            // try
            // {
            //     IsValidDate(birthDate);
            //     // DateTime result = DateTime.ParseExact(birthDate, "d", CultureInfo.InvariantCulture);
            //     // show.MensageSuccess($"{birthDate} converts to {result.ToString("d")}.");
            // }
            // catch (FormatException)
            // {
            //     show.MensageError($"{birthDate} is not in the correct format. \nThe correct format is ../../....", 0);
            // }
            show.Mensage("your birthDate (Day/Month/Year): ");
            birthDate = Console.ReadLine()!;
            if (!IsValidDate(birthDate))
            {
                show.MensageError($"\n{birthDate} is not in the correct format. \nThe correct format is 01/01/1900 until 31/12/2023", 0);
            }
        } while (!IsValidDate(birthDate));

        User user = new User(name, email, birthDate);
        users.Add(user);

        show.MensageSuccess("\nUser added successfully!");
        Console.ReadKey();
    }

    public void EditUser()
    {
        int id;
        string name;
        string email;
        string birthDate;

        do
        {
            show.Mensage("the ID of the user to edit: ");
            id = Convert.ToInt32(Console.ReadLine());
        } while (id == 0);

        User user = users.Find(u => u.Id == id)!;

        if (user == null)
        {
            show.MensageError("User", 2);
            return;
        }

        do
        {
            show.Mensage("the new name: ");
            name = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(name))
            {
                show.MensageError("Name", 1);
            }
        } while (string.IsNullOrWhiteSpace(name) || NameIsExist(name));

        do
        {
            show.Mensage("the new email: ");
            email = Console.ReadLine()!;
            if (!IsValidEmail(email))
            {
                show.MensageError("Email", 1);
            }
        } while (!IsValidEmail(email));

        do
        {
            show.Mensage("the new birthdate (Day/Month/Year): ");
            birthDate = Console.ReadLine()!;
            if (!IsValidDate(birthDate))
            {
                show.MensageError($"\n{birthDate} is not in the correct format. \nThe correct format is 01/01/1900 until 31/12/2023", 0);
            }
        } while (!IsValidDate(birthDate));

        user.Name = name;
        user.Email = email;
        user.BirthDate = DateTime.Parse(birthDate);
        user.CreatedUserDate = DateTime.Now;

        show.MensageSuccess("\nUser edited successfully!");
        Console.ReadKey();
    }

    public void DeleteUser()
    {
        show.Mensage("the ID of the user to delete: ");
        int id = Convert.ToInt32(Console.ReadLine());

        User user = users.Find(u => u.Id == id)!;

        if (user == null)
        {
            show.MensageError("User", 2);
            return;
        }

        users.Remove(user);

        show.MensageSuccess("\nUser deleted successfully!");
        Console.ReadKey();
    }

    public void ListAllUsers()
    {
        if (users.Count > 0)
        {
            show.MensageSuccess("\nAll users registered:");
            foreach (User user in users)
            {
                Console.WriteLine(user);
            }
            Console.ReadKey();
        }
        else
        {
            show.MensageError("\nList is Empty, Create a new user to list all users", 0);
        }
        Console.Clear();
    }

    public void ListByName()
    {
        string name;
        do
        {
            show.Mensage("the name to be searched: ");
            name = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(name))
            {
                show.MensageError("Name", 1);
            }
        } while (string.IsNullOrWhiteSpace(name));

        List<User> usersFound = users.FindAll(u => u.Name.ToLower() == name.ToLower());

        if (usersFound.Count == 0)
        {
            show.MensageError("\nNo user was found with the given name!", 0);
            return;
        }

        show.MensageSuccess($"\nUsers found with the name '{name}':");

        foreach (User user in usersFound)
        {
            Console.WriteLine(user);
        }
        Console.ReadKey();
        Console.Clear();
    }

    public void ListById()
    {
        show.Mensage("the ID to be searched: ");
        int id = Convert.ToInt32(Console.ReadLine());

        User user = users.Find(u => u.Id == id)!;

        if (user == null)
        {
            show.MensageError("User", 2);
            return;
        }
        else
        {
            show.MensageSuccess($"\nUser found: \n{user}");
        }
        Console.ReadKey();
        Console.Clear();
    }

    public void ListByBirthDate()
    {
        string birthDate;
        do
        {
            show.Mensage("the birthdate to be searched (Day/Month/Year):");
            birthDate = Console.ReadLine()!;
            if (!IsValidDate(birthDate))
            {
                show.MensageError($"\n{birthDate} is not in the correct format. \nThe correct format is 01/01/1900 until 31/12/2023", 0);
            }
        } while (!IsValidDate(birthDate));

        List<User> usersFound = users.FindAll(u => u.BirthDate == DateTime.Parse(birthDate));

        if (usersFound.Count == 0)
        {
            show.MensageError("\nNo user was found with the given birthdate!", 0);
            Console.ReadKey();
            return;
        }

        show.MensageSuccess($"\nUsers found with the birthdate  '{birthDate}':");
        foreach (User user in usersFound)
        {
            Console.WriteLine(user);
        }
        Console.ReadKey();
        Console.Clear();
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
            show.MensageSuccess($"\nUser oldest registered user: \n{olderUser}");
            Console.ReadKey();
        }
        else
        {
            show.MensageError("\nNo user was registered!", 0);
            Console.ReadKey();
        }
        Console.Clear();
    }

    private Boolean NameIsExist(string name)
    {

        List<User> usersFound = users.FindAll(u => u.Name.ToLower().Contains(name.ToLower()));

        if (usersFound.Count == 0)
        {
            return false;
        }
        show.MensageSuccess($"\nUsers found with the name '{name}', you need to other name!");
        Console.ReadKey();
        return true;
    }

    private Boolean IsValidEmail(string email)
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

    private Boolean IsValidDate(string birthDate)
    {
        Regex birthDateRx = new Regex(@"^(0[1-9]|[1-2][0-9]|3[0-1])[./-](0[1-9]|1[0-2])[./-](19[0-9][0-9]|202[0-3]|20[0-1][0-9])$", RegexOptions.IgnoreCase);
        return birthDateRx.IsMatch(birthDate);
    }
}

