using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace etrade.entities
{
   public class Language
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Id { get; set; }
        public bool IsNatural { get; set; }
    }
}
