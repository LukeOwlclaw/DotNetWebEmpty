using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models.BO
{
    [Table("tblSample")]
    public class Sample
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable. - Only for Entity Framework
        [Obsolete("Only intended for de-serialization. Caller must make sure that non-nullable properties are properly initialized.")]
        private Sample()
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        {
        }

        public Sample(string test1024, DateTime? date = null)
        {
            Test1024 = test1024;
            Date = date ?? DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(1024)]
        public string Test1024 { get; set; }

        public DateTime Date { get; set; }
    }
}
