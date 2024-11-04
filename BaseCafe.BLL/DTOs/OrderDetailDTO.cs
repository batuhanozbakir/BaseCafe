﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCafe.BLL.DTOs
{
    public record OrderDetailDTO(int Id, int OrderId, int ProductId, int Quantity, decimal UnitPrice);
    
}
