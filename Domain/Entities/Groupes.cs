namespace Domain.Entities;

public class Groupes
{
    public int Id { get; set; }
    public string GroupName { get; set; }
    public string GroupDescription { get; set; }
    public int CourseId { get; set; }
}
