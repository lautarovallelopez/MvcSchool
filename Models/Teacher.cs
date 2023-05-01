namespace MvcSchool.Models;

public class Teacher {
    public int Id { get; set; }
    public string Name { get; set; }
    public int SchoolId { get; set; }
    public virtual School School { get; set; }
    public List<Student> Students { get; } = new();
}