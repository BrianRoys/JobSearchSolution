//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JSSService
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contact
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contact()
        {
            this.Events = new HashSet<Event>();
            this.Opps = new HashSet<Opp>();
        }
    
        public int Id { get; set; }
        public System.Guid UserId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string EMailAddress { get; set; }
        public string AgencyName { get; set; }
        public string PhysicalAddress { get; set; }
        public string URL { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Opp> Opps { get; set; }
    }
}