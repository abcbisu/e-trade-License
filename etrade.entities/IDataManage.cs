using System;

namespace etrade.entities
{
    public interface IDataManage
    {
         DateTime InsertedOn { get; set; }
         long InsertedBy { get; set; }
         DateTime?  UpdatedOn { get; set; }
         long? UpdatedBy { get; set; }
         bool IsDeleted { get; set; }
         long? DeletedBy { get; set; }
         DateTime? DeletedOn { get; set; }
    }

}
