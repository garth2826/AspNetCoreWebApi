using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreWebApi.Models;

public partial class GarthDevice
{
    public int Id { get; set; }

    public int? CompanyId { get; set; }

    public string Module { get; set; } = null!;

    public string? SerialNumber { get; set; }

    public string? IpAddress { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? PositionOfDevice { get; set; }

    //garthtest
    //[ForeignKey("Company")]
    public virtual GarthCompany? Company { get; set; }

    public virtual ICollection<GarthLog> GarthLogs { get; set; } = new List<GarthLog>();
}
