//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace POS.APP_Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class TurnStileServer
    {
        public TurnStileServer()
        {
            this.Turnstiles = new HashSet<Turnstile>();
        }
    
        public int Id { get; set; }
        public string Description { get; set; }
        public string ServerIP { get; set; }
        public int Port { get; set; }
        public string UserDefinded { get; set; }
    
        public virtual ICollection<Turnstile> Turnstiles { get; set; }
    }
}
