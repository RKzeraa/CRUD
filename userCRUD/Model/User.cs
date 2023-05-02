class User
{
    static int nextId = 1;

    public int Id { get; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
     public DateTime CreatedUserDate { get; set; }

    public User(string name, string email, string birthDate)
    {
        Id = nextId;
        Name = name;
        Email = email;
        BirthDate = DateTime.Parse(birthDate);
        CreatedUserDate = DateTime.Now;
        nextId++;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Email: {Email}, Birthdate: {BirthDate.ToString("d")}, Created User Date: {CreatedUserDate}";
    }
}