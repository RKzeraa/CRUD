public class MensageGenerics<T>
{
    public void Mensage(T t)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"\nEnter {t}");
        Console.ResetColor();
    }

    public void MensageError(T t, int operation)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        switch (operation)
        {
            case 1:
                Console.WriteLine($"\n{t} format is invalid!");
                break;
            case 2:
                Console.WriteLine($"\n{t} not found!");
                break;
            case 0:
                Console.WriteLine($"{t}");
                break;
        }
        Console.ResetColor();
    }

    public void MensageSuccess(T t)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{t}");
        Console.ResetColor();
    }
}