using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etrade
{
   public static class ExtensionMethos
    {
        public static string Serialize(this object _object)
        {
            return JsonConvert.SerializeObject(_object);
        }
        public static T Deserialize<T>(this string jsonData)
        {
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
    }
}
