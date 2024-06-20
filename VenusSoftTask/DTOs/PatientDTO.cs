namespace VenusSoftTask.DTOs;

public class PatientDTO
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Prefix { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public string Address { get; set; } = null!;

    //public string? Attachment { get; set; }
    
    public string Email { get; set; } = null!;
    
    public string Phone { get; set; } = null!;
}