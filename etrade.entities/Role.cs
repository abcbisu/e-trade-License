using System;
using System.ComponentModel.DataAnnotations;

namespace etrade.entities
{
    public class Role:IDataManage
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string RoleName { get; set; }
        [StringLength(10)]
        public string RoleCode { get; set; }
        public bool IsActive
        {
            get; set;
        }
        public DateTime InsertedOn { get; set; }
        public long InsertedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }

}
