﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCafe.BLL.DTOs
{
    public record OrderDTO(int Id, DateTime OrderDate,decimal TotalAmount, string Status);
    
}
