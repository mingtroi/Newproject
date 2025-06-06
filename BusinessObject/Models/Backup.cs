using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Backup
{
    public int BackupId { get; set; }

    public byte[] BackupData { get; set; } = null!;

    public DateTime? BackupDate { get; set; }
}
