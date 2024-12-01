using MyBaseRepository.Enums;

namespace MyBaseRepository.Models;

public class BaseModel
{
    public Guid ID { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? LastModifiedBy { get; set; }

    public DateTime CreatedDate { get; set; }=DateTime.Now;

    public DateTime LastModifiedOn { get; set; }=DateTime.Now;
    
    public ObjectStatus ObjectStatus { get; set; } = ObjectStatus.NonDeleted;
    
    public Status Status { get; set; }=Status.Active;
}
