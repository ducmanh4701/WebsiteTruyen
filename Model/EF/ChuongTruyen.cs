namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ChuongTruyen")]
    public partial class ChuongTruyen
    {
        [Key]
        public long Id_Chuong { get; set; }

        [StringLength(255)]
        public string Ten_Chuong { get; set; }

        [Column(TypeName = "ntext")]
        public string NoiDung_Chuong { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        public int? DisplayOrder { get; set; }

        public long? Id_Truyen { get; set; }

        public virtual Truyen Truyen { get; set; }
    }
}
