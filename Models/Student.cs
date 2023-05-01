namespace MvcSchool.Models;

public class Student {
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Teacher> Teachers { get; set; }
}