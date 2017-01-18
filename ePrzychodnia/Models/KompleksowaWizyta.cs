using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ePrzychodnia.Models
{
    public class KompleksowaWizyta
    {
        [Key]
        [DisplayName("Numer wizyty")]
        public int id_wizyta { get; set; }
        [DisplayName("Numer Skierowania")]
        public int? id_skierowanie{ get; set; }
        [DisplayName("Numer recepty")]
        public int? id_recepta{ get; set; }
        [DisplayName("Numer choroby")]
        public int? id_choroba{ get; set; }
        [DisplayName("Numer badania")]
        public int? id_badanie{ get; set; }
        [DisplayName("Numer lekarza")]
        public int id_lekarz { get; set; }
        [DisplayName("Numer pacjenta")]
        public int id_pacjent { get; set; }
        [DisplayName("Data wizyty")]
        public DateTime data_wizyty { get; set; }
        [DisplayName("Data wystawienia skierowania")]
        public DateTime? data_wystawienia_skierowanie{ get; set; }
        [DisplayName("Data badania")]
        public DateTime? data_badania{ get; set; }
        [DisplayName("Data wystawienia recepty")]
        public DateTime? data_wystawienia_recepta{ get; set; }
        [DisplayName("Opis badania")]
        public string opis_badania { get; set; }
        [DisplayName("Leki i dawkowanie")]
        public string lek_i_dawkowanie { get; set; }
        [DisplayName("Nazwa choroby")]
        public string nazwa_choroby { get; set; }
        [DisplayName("Objawy")]
        public string objawy { get; set; }
        [DisplayName("Diagnoza")]
        public string diagnoza { get; set; }
        public bool wypelnione_badanie { get; set; }
        public bool wypelnione_recepta { get; set; }
        public bool wypelnione_choroba { get; set; }
    }
}