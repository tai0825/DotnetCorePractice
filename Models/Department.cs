using System;
using System.Collections.Generic;

namespace DotnetCorePractice.Models
{
    public partial class Department : ITrack
    {
        public Department()
        {
            Course = new HashSet<Course>();
        }

        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }
        public int? InstructorId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual Person Instructor { get; set; }
        public virtual ICollection<Course> Course { get; set; }
        public bool? IsDeleted { get ; set; }
        public DateTime? DateModified { get; set; }
    }
}
