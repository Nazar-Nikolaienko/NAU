using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{

    [Serializable]
    public class Product
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public int Number { get; set; }
        public int Cost { get; set; }
        public Category Category { get; set; }
        public Provider Provider { get; set; }

        public override string ToString() => $"Name: {Name}\t Brand: {Brand}\t Price: {Cost}";
    }
}
