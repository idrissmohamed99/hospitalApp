
using hospitalApp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hospitalApp.Models.ViewModels
{
    public class CheckUpViewModel
    {
        public int id { get; set; }
        public string doctorName { get; set; }
        public string Patientname { get; set; }
        public System.DateTime CreateAt { get; set; }

        public string CheckType { get; set; }
        public virtual doctor doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}