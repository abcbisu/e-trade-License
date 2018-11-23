using System;
using System.ComponentModel.DataAnnotations;

namespace etrade.entities
{
    public interface IUser
    {
        DateTime? AccLockedOn { get; set; }
        bool IsAccLocked { get; set; }
        Profile ProfileBasic { get; set; }
        long ProfileBasicId { get; set; }
        long UserId { get; set; }
        int LoginTypeId { get; set; }

    }
   public class UserCredential
    {
        public long UserId { get; set; }
        [StringLength(70), Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }
        [StringLength(200), Required(AllowEmptyStrings = false)]
        public string Password { get; set; }
        public String UserNameType { get; set; }
        public User User { get; set; }
    }

    public  class User : IUser
    {
        public  long UserId { get; set; }
        public  DateTime? AccLockedOn { get; set; }
        public  bool IsAccLocked { get; set; }
        public  Profile ProfileBasic { get; set; }
        public long ProfileBasicId { get; set; }
        public LoginType LoginType { get; set; }
        public int LoginTypeId { get; set; }
    }
}