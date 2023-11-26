namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BinhLuan")]
    public partial class BinhLuan
    {
        [Key]
        public long Id_BinhLuan { get; set; }

        [StringLength(255)]
        public string NoiDung { get; set; }

        public DateTime? CreateTime { get; set; }

        public long? Id_Truyen { get; set; }

        public long? Id_User { get; set; }

        public virtual Truyen Truyen { get; set; }

        public virtual User User { get; set; }
    }
}
