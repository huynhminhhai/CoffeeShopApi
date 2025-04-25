using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopApi.Dto.Category
{
    public class CreateCategoryRequestDto
    {
        public string Name { get; set; } = string.Empty;
    }
}