using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace VenusSoftTask.DTOs;

public class PatientCreationDTO
{
    [Required(ErrorMessage = "FirstName is required.")]
    public string FirstName { get; set; } = null!;
    
    
    [Required(ErrorMessage = "Last is required.")]
    public string LastName { get; set; } = null!;
    
    
    [Required(ErrorMessage = "Prefix is required.")]
    public string Prefix { get; set; } = null!;
    
    
    [Required(ErrorMessage = "DOB is required.")]
    [DataType(DataType.Date)]
    public DateTime Dob { get; set; }
    
    
    [Required(ErrorMessage = "Address is required.")]
    public string Address { get; set; } = null!;
    
    [Base64String]
    public string? Attachment { get; set; }
    
    
    [Required(ErrorMessage = "FirstName is required.")]
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    
    [Required(ErrorMessage = "Phone Number is required.")]
    [Display(Name = "Phone Number")]
    [RegularExpression("\\d{10}",ErrorMessage = "Phone number must contain 10 digits.")]
    public string Phone { get; set; }
}