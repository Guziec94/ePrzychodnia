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
    public partial class badania_view
    {
        [DisplayName("Numer użytkownika")]
        public string id_uzytkownika { get; set; }
        [DisplayName("Nazwisko lekarza")]
        public string Nazwisko_lekarza { get; set; }
        [DisplayName("Numer badania")]
        public int id_badanie { get; set; }
        [DisplayName("Data badania")]
        public Nullable<System.DateTime> Data_badania { get; set; }
        [DisplayName("Opis badania")]
        public string C_Opis_badania { get; set; }
    }
}
