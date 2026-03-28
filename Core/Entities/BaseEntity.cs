namespace Core.Entities;

public abstract class BaseEntity
{
    public string? Id { get; set; }
    public string CreatedAt { get; set; }
    
    protected  BaseEntity()
    {
        Id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow.ToString("MM/dd/yyyy");
    }
}