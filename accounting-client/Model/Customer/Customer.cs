using System;
using System.ComponentModel.DataAnnotations;

namespace WS.Model
{
    public class Customer
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least 2 characters long.", MinimumLength = 2)]
        public string Name { get; set; }
        [Display(Name = "Lastname")]
        [StringLength(50, ErrorMessage = "The {0} must be at least 2 characters long.", MinimumLength = 2)]
        public string Lastname { get; set; }

        public Nullable<DateTime> Created { get; set; }
        public Nullable<DateTime> Updated { get; set; }
    }
}