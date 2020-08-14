using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DotnetCorePractice.Models
{
    public partial class ContosoUniversityContext : DbContext
    {
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = this.ChangeTracker.Entries();
            foreach (var entity in entries)
            {
                var department = entity.Entity as Department;
                if (department != null)
                {
                    switch (entity.State)
                    {
                        case EntityState.Added:
                            var addRes = Department.FromSqlInterpolated($"EXECUTE dbo.Department_Insert {department.Name},{department.Budget},{department.StartDate},{department.InstructorId}")
                              .IgnoreQueryFilters()
                              .AsEnumerable()
                              .FirstOrDefault();
                            department.DepartmentId = addRes.DepartmentId;
                            department.RowVersion = addRes.RowVersion;
                            break;
                        case EntityState.Modified:
                            var updateRes = Department.FromSqlInterpolated($"EXECUTE dbo.Department_Update {department.DepartmentId},'{department.Name}',{department.Budget},{department.StartDate},{department.InstructorId},{department.RowVersion}")
                              .IgnoreQueryFilters()
                              .AsEnumerable()
                              .FirstOrDefault();
                            department.RowVersion = updateRes.RowVersion;
                            break;
                        case EntityState.Deleted:
                            Department.FromSqlInterpolated($"EXECUTE dbo.Department_Update {department.DepartmentId}, {department.RowVersion}")
                              .IgnoreQueryFilters()
                              .AsEnumerable();
                            break;
                    }
                }
                else
                {
                  var tt = entity.Entity is ITrack;
                  var ttt = entity.Entity as ITrack;
                  if (entity.Entity is ITrack)
                  {
                      var track = entity.Entity as ITrack;
                      switch (entity.State)
                      {
                          case EntityState.Modified:
                            track.DateModified = DateTime.Now;
                            break;
                          case EntityState.Deleted:
                            entity.State = EntityState.Modified;
                            track.IsDeleted = true;
                            track.DateModified = DateTime.Now;
                            break;
                      }
                  }
                }
            }

            return base.SaveChangesAsync();
        }

    }
}