using hospitalApp.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace hospitalApp.Models.ViewModels
{
    public class UpdateCheckUpViewModel
    {
        public int id { get; set; }
        [DisplayName("نوع الكشف")]
        [Required(ErrorMessage = "الحقل اجباري")]
        public string CheckType { get; set; }
        [Required(ErrorMessage = "الحقل اجباري")]

        public int DoctorId { get; set; }
        [Required(ErrorMessage = "الحقل اجباري")]


        public int PatientId { get; set; }
        public virtual doctor doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}