using System.ComponentModel.DataAnnotations;

namespace FWS.Generic.Framework.Interfaces
{
    public interface ITitle
    {
        [Required]
        [StringLength(128)]
        string Title { get; set; }
    }
}
