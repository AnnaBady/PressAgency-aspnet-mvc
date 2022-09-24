using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            Editors = new HashSet<Editor>();
            Favorites = new HashSet<Favorite>();
            Questions = new HashSet<Question>();
        }


       

        [Key]
        
     
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int post_id { get; set; }

        [StringLength(20)]
        public string ArticleTitle { get; set; }

        [StringLength(100)]
        public string ArticleBody { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public string image { get; set; }

        [StringLength(20)]
        public string Type { get; set; }

        public bool? Approve { get; set; }

      
        public int? Likes { get; set; }

        public int? Dislikes { get; set; }

        public int? Viewers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Editor> Editors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Favorite> Favorites { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }
    }
}