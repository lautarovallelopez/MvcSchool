namespace MvcSchool.Models;

public class School {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Adress { get; set; }
    public int PrincipalId { get; set; }
    public virtual Principal Principal { get; set; }
    public List<Teacher> Teachers { get; set; }
}