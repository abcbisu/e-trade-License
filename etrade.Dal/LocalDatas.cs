using System.Collections.Generic;

namespace etrade.models
{
    internal class LocalDatas
    {
        public LocalDatas()
        {
        }

        public long Id { get;  set; }
        public string About { get;  set; }
        public Dictionary<string,object> Datas { get;  set; }
    }
}