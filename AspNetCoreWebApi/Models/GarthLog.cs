using System;
using System.Collections.Generic;

namespace AspNetCoreWebApi.Models;

public partial class GarthLog
{
    public int Id { get; set; }

    public int? DeviceId { get; set; }

    public DateTime? LogDate { get; set; }

    public string DeviceLog { get; set; } = null!;

    public string? DeviceConfig { get; set; }

    public virtual GarthDevice? Device { get; set; }
}
