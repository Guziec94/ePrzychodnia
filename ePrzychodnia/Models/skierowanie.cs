//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ePrzychodnia.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    public partial class skierowanie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public skierowanie()
        {
            this.wizyta = new HashSet<wizyta>();
        }
        [DisplayName("Numer skierowania")]
        public int id_skierowanie { get; set; }
        [DisplayName("Numer pacjenta")]
        public Nullable<int> id_pacjent { get; set; }
        [DisplayName("Numer badania")]
        public Nullable<int> id_badanie { get; set; }
        [DisplayName("Numer lekarza")]
        public Nullable<int> id_lekarz { get; set; }
        [DisplayName("Data wystawienia")]
        public Nullable<System.DateTime> data_wystawienia { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wizyta> wizyta { get; set; }
    }
}
