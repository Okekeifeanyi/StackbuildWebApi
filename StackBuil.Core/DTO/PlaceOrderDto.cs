using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackBuildApi.Core.DTO
{
    public class PlaceOrderDto
    {
        public List<PlaceOrderItemDto> Items { get; set; } = new();
    }

}
