using System.ComponentModel.DataAnnotations;

namespace etrade_server.Models
{
    public class Credenetial:Identity
    {
        [StringLength(20,MinimumLength =8,ErrorMessage ="Password length should be with in 08 to 20 character")]
        public string Password { get; set; }
    }
}