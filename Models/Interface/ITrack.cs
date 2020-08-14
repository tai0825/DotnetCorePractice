using System;

namespace DotnetCorePractice.Models
{
    public interface ITrack
    {
        bool? IsDeleted { get; set; }
        DateTime? DateModified { get; set; }
    }
}