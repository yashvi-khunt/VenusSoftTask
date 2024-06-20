using System;
using System.Collections.Generic;

namespace VenusSoftTask.Models;

public partial class PatientEmail
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public string Email { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
