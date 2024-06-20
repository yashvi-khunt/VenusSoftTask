using System;
using System.Collections.Generic;

namespace VenusSoftTask.Models;

public partial class Patient
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Prefix { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public string Address { get; set; } = null!;

    public string? Attachment { get; set; }

    public virtual ICollection<PatientEmail> PatientEmails { get; set; } = new List<PatientEmail>();

    public virtual ICollection<PatientPhoneNumber> PatientPhoneNumbers { get; set; } = new List<PatientPhoneNumber>();
}
