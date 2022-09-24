using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Admin
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        [Key]
        [StringLength(20)]
        public string username { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string Email { get; set; }

        public int? PhoneNO { get; set; }

        [Required]
        [StringLength(10)]
        public string Role { get; set; }

        [Required]
        public string Photo { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}