using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.ReleaseTool.Web.Models
{
    public class WhitelistViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string IpAddress { get; set; }
    }
}
