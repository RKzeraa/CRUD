using System;
using System.Collections.Generic;
class Program
{
    static void Main(string[] args)
    {
        IUserController user = new UserController();

        while (true)
        {
            Console.WriteLine("\n\n1 - Add New User");
            Console.WriteLine("2 - Edit User");
            Console.WriteLine("3 - Delete User");
            Console.WriteLine("4 - List All Users");
            Console.WriteLine("5 - List by Name");
            Console.WriteLine("6 - List by Id");
            Console.WriteLine("7 - List by Birthdate");
            Console.WriteLine("8 - List Older User");
            Console.WriteLine("9 - Sair");

            Console.Write("\nChoosen an option: ");
            string opcao = Console.ReadLine()!;
            Console.Clear();

            switch (opcao)
            {
                case "1":
                    user.AddUser();
                    break;
                case "2":
                    user.EditUser();
                    break;
                case "3":
                    user.DeleteUser();
                    break;
                case "4":
                    user.ListAllUsers();
                    break;
                case "5":
                    user.ListByName();
                    break;
                case "6":
                    user.ListById();
                    break;
                case "7":
                    user.ListByBirthDate();
                    break;
                case "8":
                    user.ListOlderUser();
                    break;
                case "9":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("\nInvalid Option!");
                    break;
            }
        }
    }
}