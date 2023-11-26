namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BoSuuTap")]
    public partial class BoSuuTap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id_BoSuuTap { get; set; }

        public long? Id_User { get; set; }

        public long? Id_Truyen { get; set; }

        public virtual Truyen Truyen { get; set; }

        public virtual User User { get; set; }
    }
}
