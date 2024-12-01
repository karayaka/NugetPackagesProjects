using System.ComponentModel.DataAnnotations;

namespace MyBaseRepository.Enums;

public enum ObjectStatus
{
    [Display(Name = "Silindi")]
    Deleted = 0,
    [Display(Name = "Silinmedi")]
    NonDeleted = 1
}
