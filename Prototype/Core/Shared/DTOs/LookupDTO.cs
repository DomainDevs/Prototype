
using Shared.Attributes;
using System.ComponentModel;

namespace Shared.DTOs
{
    /// <summary>
    /// DTO para listados tipo lookup/dropdown.
    /// </summary>
    [GlobalDTO]
    public class LookupDTO
    {
        [DefaultValue(null)]
        public string? Key { get; set; } = null;

        [DefaultValue(null)]
        public string? Value { get; set; } = null;
    }
}
