using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Favorite
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int fav_id { get; set; }

        public virtual Viewer Viewer { get; set; }

        [StringLength(20)]
        public string username_Viewer { get; set; }

        public virtual Post Post { get; set; }

        public int? post_id { get; set; }
    }
}