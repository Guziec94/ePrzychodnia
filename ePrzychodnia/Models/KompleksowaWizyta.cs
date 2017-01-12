using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ePrzychodnia.Models
{
    public class KompleksowaWizyta
    {
        [Key]
        public int id_wizyta { get; set; }
        public int? id_skierowanie{ get; set; }
        public int? id_recepta{ get; set; }
        public int? id_choroba{ get; set; }
        public int? id_badanie{ get; set; }
        public int id_lekarz { get; set; }
        public int id_pacjent { get; set; }
        public DateTime data_wizyty { get; set; }
        public DateTime? data_wystawienia_skierowanie{ get; set; }
        public DateTime? data_badania{ get; set; }
        public DateTime? data_wystawienia_recepta{ get; set; }
        public string opis_badania { get; set; }
        public string lek_i_dawkowanie { get; set; }
        public string nazwa_choroby { get; set; }
        public string objawy { get; set; }
        public string diagnoza { get; set; }
        public bool wypelnione_badanie { get; set; }
        public bool wypelnione_recepta { get; set; }
        public bool wypelnione_choroba { get; set; }
    }
}