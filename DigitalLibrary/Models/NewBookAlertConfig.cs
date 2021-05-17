using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalLibrary.Models
{
    public class NewBookAlertConfig
    {
        public bool IsActive { get; set; }
        public string AlertMessage { get; set; }
    }
}
