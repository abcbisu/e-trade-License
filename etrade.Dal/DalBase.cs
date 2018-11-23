using System;

namespace etrade.dal
{
    public abstract class DalBase : DbHandler
    {
        public long _actorId { get; private set; }
        public DalBase(long actorId,string constring="constring"):base(constring)
        {
            if (actorId <= 0) throw new InvalidOperationException("Invalid User id");
            _actorId = actorId;
        }
    }
}
