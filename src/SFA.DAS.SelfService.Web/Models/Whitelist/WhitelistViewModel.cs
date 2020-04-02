using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SFA.DAS.SelfService.Web.Models
{
    public class WhitelistViewModel
    {
        [Required(ErrorMessage = "Please enter a valid IPv4 Address")]
        [RegularExpression(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$", ErrorMessage = "Please enter a valid IPv4 Address")]
        public string IpAddress { get; set; }

        [Required(ErrorMessage = "Please select at least one environment")]
        public List<int> SelectedEnvironmentIds { get; set; }
    }
}