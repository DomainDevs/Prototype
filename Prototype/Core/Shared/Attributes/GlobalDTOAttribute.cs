
namespace Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class GlobalDTOAttribute : Attribute
    {
        /// <summary>
        /// Descripción opcional del propósito del DTO.
        /// </summary>
        public string Description { get; set; } = "DTO global y reutilizable";

        public GlobalDTOAttribute() { }

        public GlobalDTOAttribute(string description)
        {
            Description = description;
        }
    }
}