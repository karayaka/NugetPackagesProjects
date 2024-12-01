using System.ComponentModel.DataAnnotations;

namespace MyBaseRepository.Enums;

public enum Status
{
    [Display(Name = "Aktif")]
    Active = 1,

    [Display(Name = "Pasif")]
    Passive = 0,
}
