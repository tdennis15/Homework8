namespace Homework8.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Classification
    {
        [Key]
        public int CID { get; set; }

        public int AWID { get; set; }

        [Required]
        [StringLength(24)]
        public string Genre { get; set; }

        public virtual ArtWork ArtWork { get; set; }

        public virtual Genre Genre1 { get; set; }
    }
}
