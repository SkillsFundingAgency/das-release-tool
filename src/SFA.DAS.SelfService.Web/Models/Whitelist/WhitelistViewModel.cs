using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.SelfService.Web.Models
{
    public class WhitelistViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a valid IPV4 Address")]
        [RegularExpression(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$", ErrorMessage = "Please enter a valid IPV4 Address")]
        public string IpAddress { get; set; }
    }
}