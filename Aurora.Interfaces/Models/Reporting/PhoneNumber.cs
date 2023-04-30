using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Interfaces.Models.Reporting;

[Owned]
public class PhoneNumber
{
    [StringLength(20)]
    public string Number { get; set; }
 
    public PhoneNumberTypeEnum Type { get; set; }

}