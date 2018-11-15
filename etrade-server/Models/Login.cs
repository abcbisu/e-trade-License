using System.ComponentModel.DataAnnotations;

namespace etrade_server.Models
{
    public class Login:Identity
    {
        /// <summary>
        /// Intermediate secured code
        /// </summary>
        [Required,StringLength(maximumLength:100)]
        public string IMCode { get; set; }
        [Required]
        public string Otp { get; set; }
    }
}