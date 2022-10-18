using Persistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DTO
{
    public class EtagereDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<VinDTO> Vins { get; set; }

    }
}
