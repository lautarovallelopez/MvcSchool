namespace MvcSchool.Models;

public class Principal{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int SchoolId { get; set; }
    public virtual School School { get; set; }
}