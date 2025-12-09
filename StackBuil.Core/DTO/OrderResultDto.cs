using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackBuildApi.Core.DTO
{
    public class OrderResultDto
    {
        public string OrderId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
