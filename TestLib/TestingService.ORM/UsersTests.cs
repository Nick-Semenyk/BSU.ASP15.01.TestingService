//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestingService.ORM
{
    using System;
    using System.Collections.Generic;
    
    public partial class UsersTests
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UsersTests()
        {
            this.UsersAnswers = new HashSet<UsersAnswers>();
        }
    
        public int id { get; set; }
        public int TestId { get; set; }
        public int UserId { get; set; }
        public System.DateTime BeginningTime { get; set; }
        public Nullable<System.DateTime> EndingTime { get; set; }
        public Nullable<int> RightAnswersCount { get; set; }
        public Nullable<int> MarkForTest { get; set; }
    
        public virtual Tests Tests { get; set; }
        public virtual Users Users { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsersAnswers> UsersAnswers { get; set; }
    }
}
