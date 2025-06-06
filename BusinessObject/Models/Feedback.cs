using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Feedback
{
    public int FeedbackId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string Message { get; set; } = null!;
}
