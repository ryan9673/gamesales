using System;
using System.Collections.Generic;

namespace prjMvcCoreDemo.Models;

public partial class TResponse
{
    public int FId { get; set; }

    public string? FDate { get; set; }

    public int? FProductId { get; set; }

    public string? FUser { get; set; }

    public string? FMessage { get; set; }
}
