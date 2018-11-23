using System;
using System.ComponentModel.DataAnnotations;

namespace etrade.entities
{
    public class Profile:IDataManage
    {
        public long ProfileId { get; set; }
        [StringLength(100), Required(AllowEmptyStrings = false)]
        public string FName { get; set; }
        
        [StringLength(20), Required(AllowEmptyStrings = false)]
        public string LName { get; set; }
        [StringLength(125), Required(AllowEmptyStrings = false)]
        public string FullName { get; set; }

        #region navigation properies
        public Mobile Mobile { get; set; }
        public long? MobileId { get; set; }
        public DateTime InsertedOn { get; set; }
        public long InsertedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        #endregion
    }

}
