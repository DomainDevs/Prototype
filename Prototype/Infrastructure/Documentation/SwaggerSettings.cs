using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Documentation;

public class SwaggerSettings
{
    public bool Enable { get; set; }
    public string? Title { get; set; }
    public string? Version1 { get; set; }
    public string? Version2 { get; set; }
    public string? Description { get; set; }
    public string Contact { get; set; }
    public string? ContactEmail { get; set; }
}