using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class VinDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Milesime { get; set; }

        public int Type { get; set; }
    }
}
