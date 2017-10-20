//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AFGT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Evento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Evento()
        {
            this.Likes = new HashSet<Like>();
            this.Artistas1 = new HashSet<Artista>();
        }
    
        public int EventosID { get; set; }
        public int OrgID { get; set; }
        public string NomeEvento { get; set; }
        public string Morada { get; set; }
        public string Descricao { get; set; }
        public System.DateTime Data { get; set; }
        public string Artistas { get; set; }
        public string Link { get; set; }
    
        public virtual Organizadore Organizadore { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Like> Likes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Artista> Artistas1 { get; set; }
    }
}