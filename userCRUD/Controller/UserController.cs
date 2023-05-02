internal class UserController : IUserController
{
    static List<User> users = new List<User>();
    public void AddUser()
    {
        Console.Write("\nEnter your name: ");
        string name = Console.ReadLine();

        Console.Write("\nEnter your email: ");
        string email = Console.ReadLine();

        Console.Write("Enter your birthDate (Day/Month/Year): ");
        string birthDate = Console.ReadLine();

        User user = new User(name, email, birthDate);
        users.Add(user);

        Console.WriteLine("\nUser added successfully!");
    }

    public void EditUser()
    {
        Console.Write("\nEnter the ID of the user to edit: ");
        int id = Convert.ToInt32(Console.ReadLine());

        User user = users.Find(p => p.Id == id);

        if (user == null)
        {
            Console.WriteLine("\nUser not found!");
            return;
        }

        Console.Write("Enter the new name: ");
        string name = Console.ReadLine();

        Console.Write("Enter the new email: ");
        string email = Console.ReadLine();

        Console.Write("Enter the new birthdate (Day/Month/Year): ");
        string birthDate = Console.ReadLine();

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

        User user = users.Find(p => p.Id == id);

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
        Console.WriteLine("\nAll users registered:");

        foreach (User user in users)
        {
            Console.WriteLine(user);
        }
    }

    public void ListByName()
    {
        Console.Write("\nEnter the name to be searched: ");
        string name = Console.ReadLine();

        List<User> usersFound = users.FindAll(p => p.Name == name);

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

        User user = users.Find(p => p.Id == id);

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
        string birthDate = Console.ReadLine();

        List<User> usersFound = users.FindAll(p => p.BirthDate == DateTime.Parse(birthDate));

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
}