using System;
using System.ComponentModel.DataAnnotations;

namespace etrade.entities
{
    public class Mobile : IDataManage
    {
        public long MobileNoId { get; set; }
        [StringLength(15), Required(AllowEmptyStrings = false)]
        public string MobileNo { get; set; }
        [StringLength(5), Required(AllowEmptyStrings = false)]
        public string ContryCode { get; set; }
        public DateTime InsertedOn { get; set; }
        public long InsertedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }

}
