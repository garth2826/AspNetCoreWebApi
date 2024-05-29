﻿using System;
using System.Collections.Generic;

namespace AspNetCoreWebApi.Models;

public partial class VehicleTemperature1
{
    public long VehicleTemperatureId { get; set; }

    public string VehicleRegistration { get; set; } = null!;

    public int ChillerSensorNumber { get; set; }

    public DateTime RecordedWhen { get; set; }

    public decimal Temperature { get; set; }

    public string? FullSensorData { get; set; }
}
