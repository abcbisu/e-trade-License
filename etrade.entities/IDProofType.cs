using System;
using System.ComponentModel.DataAnnotations;

namespace etrade.entities
{
    public class IDProofType:IDataManage
    {
        public int Id { get; set; }
        [StringLength(5), Required(AllowEmptyStrings = false)]
        public string TypeCode { get; set; }
        [StringLength(20),Required(AllowEmptyStrings =false)]
        public string TypeName { get; set; }
        public DateTime InsertedOn { get; set; }
        public long InsertedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }

}
