using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCoreWebApi.Models;

public partial class GarthCompany
{
    public int Id { get; set; }

    public string Company { get; set; } = null!;

    //garthtest
    //[ForeignKey("Company")]
    public virtual ICollection<GarthDevice> GarthDevices { get; set; } = new List<GarthDevice>();
}
