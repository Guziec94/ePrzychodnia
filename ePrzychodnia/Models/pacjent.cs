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
    
    public partial class pacjent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public pacjent()
        {
            this.badanie = new HashSet<badanie>();
            this.recepta = new HashSet<recepta>();
        }
    
        public int id_pacjent { get; set; }
        public string nazwisko { get; set; }
        public string imie { get; set; }
        public Nullable<int> wiek { get; set; }
        public string pesel { get; set; }
        public string telefon { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<badanie> badanie { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<recepta> recepta { get; set; }
    }
}
