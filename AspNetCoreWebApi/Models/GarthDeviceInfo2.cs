using System;
using System.Collections.Generic;

namespace AspNetCoreWebApi.Models;

public partial class GarthDeviceInfo2
{
    public int Id { get; set; }

    public string? Company { get; set; }

    public string? Device { get; set; }

    public string? Module { get; set; }

    public string? SerialNumber { get; set; }

    public string? Ipaddress { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Position { get; set; }
}
