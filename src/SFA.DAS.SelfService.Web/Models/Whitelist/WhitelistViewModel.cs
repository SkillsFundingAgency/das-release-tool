using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.SelfService.Web.Models
{
    public class WhitelistViewModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Please enter a valid IPV4 Address")]
        [RegularExpression(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$", ErrorMessage = "Please enter a valid IPV4 Address")]
        public string IpAddress { get; set; }
    }
}