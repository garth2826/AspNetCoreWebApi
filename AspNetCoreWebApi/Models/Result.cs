using System;
using System.Collections.Generic;

namespace AspNetCoreWebApi.Models;

public partial class Result
{
    public int Id { get; set; }

    public string? ColumnName { get; set; }

    public string? ColumnValue { get; set; }
}
