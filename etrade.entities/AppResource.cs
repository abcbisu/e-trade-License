using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace etrade.entities
{
   public class ControllerPackage:IDataManage
    {
        public int PackageId { get; set; }
        [StringLength(50)]
        public string  PackageName { get; set; }
        public ICollection<Controller> Controllers { get; set; }
        public DateTime InsertedOn { get; set; }
        public long InsertedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public ControllerPackage()
        {
            Controllers = new List<Controller>();
        }
    }
    public class Controller:IDataManage
    {
        [Key]
        public int ControllerId { get; set; }
        [StringLength(50)]
        public string  ControllerName { get; set; }
        public bool IsActive { get; set; }
        public ControllerPackage ControllerPackage { get; set; }
        public int ControllerPackageID { get; set; }
        public ICollection<Action> Actions { get; set; }
        public DateTime InsertedOn { get; set; }
        public long InsertedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public Controller()
        {
            Actions = new List<Action>();
        }

    }
    public class Action:IDataManage
    {
        [Key]
        public int ActionId { get; set; }
        [StringLength(50)]
        public string ActionName { get; set; }
        public bool IsActive { get; set; }
        public Controller Controller { get; set; }
        public int ControllerId { get; set; }
        public DateTime InsertedOn { get; set; }
        public long InsertedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
   public class ActionsInRole : IDataManage
    {
        [Key]
        public long MapId { get; set; }
        public int ActionId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public Action Action { get; set; }
        public Role Role { get; set; }
        public DateTime InsertedOn { get; set; }
        public long InsertedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
    public class UsersInRole : IDataManage
    {
        [Key]
        public long MapId { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public bool IsActive
        {
            get; set;
        }
        public Role Role { get; set; }
        public DateTime InsertedOn { get; set; }
        public long InsertedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
    public class LoginType
    {
        [Key]
        public int TypeId { get; set; }
        [StringLength(5),Required(AllowEmptyStrings =false)]
        public string TypeCode { get; set; }
        [StringLength(20), Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}
