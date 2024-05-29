using System;
using System.Collections.Generic;

namespace AspNetCoreWebApi.Models;

public partial class GarthCompany
{
    public int Id { get; set; }

    public string Company { get; set; } = null!;

    public virtual ICollection<GarthDevice> GarthDevices { get; set; } = new List<GarthDevice>();
}
