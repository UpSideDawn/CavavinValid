using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Models
{
    public class Etagere
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Vin> Vins { get; set; }

        public DateTime DateCreation { get; set; }
    }
}
