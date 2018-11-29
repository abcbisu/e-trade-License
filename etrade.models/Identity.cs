using etrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace etrade.models
{
    public class Identity
    {
        /// <summary>
        /// user id=>mobile no/ email
        /// </summary>
        public string Idntity { get; set; }
        /// <summary>
        /// type of user id
        /// </summary>
        public IdentityType IdType { get; set; }
        public virtual void Validate()
        {
            if (IdType == IdentityType.email)
            {
                etrade.Validate.Email(this.Idntity);
            }
            else if(IdType==IdentityType.mobile)
            {
                etrade.Validate.Mobile(this.Idntity);
            }
            else
            {
                throw new Exception("Invalid Id Type");
            }
        }
    }
}