using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    [Serializable]
    public class Provider
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
        public override string ToString() => $"Provider: {Name} {Surname}";
    }
}
