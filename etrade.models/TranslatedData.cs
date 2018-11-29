using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etrade.models
{
   public class TranslatedData<T>
    {
        public Dictionary<int, T> LocalizedData { get; set; }
    }
}
