using System;
using System.Collections.Generic;

namespace VenusSoftTask.Models;

public partial class PatientPhoneNumber
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public string Phone { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
