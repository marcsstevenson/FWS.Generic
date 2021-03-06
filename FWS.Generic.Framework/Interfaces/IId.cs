﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWS.Generic.Framework.Interfaces
{
    public interface IId
    {
        [Key]
        dynamic Id { get; set; }

        bool Equals(IId id);
    }
}
