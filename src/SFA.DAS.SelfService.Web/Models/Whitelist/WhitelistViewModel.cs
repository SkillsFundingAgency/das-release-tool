using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.SelfService.Web.Models
{
    public class WhitelistViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string IpAddress { get; set; }
    }
}