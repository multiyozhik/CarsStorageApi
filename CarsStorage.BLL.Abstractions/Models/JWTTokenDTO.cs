﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsStorage.BLL.Abstractions.Models
{
    public class JWTTokenDTO
    {
		public string? AccessToken { get; set; }
		public string? RefreshToken { get; set; }
		public DateTime TokenExpires { get; set; }
	}
}
