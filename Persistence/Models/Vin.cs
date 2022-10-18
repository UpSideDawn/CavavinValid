using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public class Vin
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Milesime { get; set; }

        public int Type { get; set; }

        public DateTime DateCreation { get; set; }
    }
}
