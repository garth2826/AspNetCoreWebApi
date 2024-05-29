using System;
using System.Collections.Generic;

namespace AspNetCoreWebApi.Models;

public partial class GarthTest1
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public float? Score { get; set; }

    public string? Password { get; set; }

    public byte[]? CreationTime { get; set; }

    public object? SqlVariable { get; set; }
}
