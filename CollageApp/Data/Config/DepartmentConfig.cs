using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CollageApp.Data.Config
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(e => e.DepartmentName).IsRequired().HasMaxLength(255);
            builder.Property(e => e.DepartmentDescription).IsRequired(false).HasMaxLength(500);

            builder.HasData(new List<Department>()
            {
                new Department
                {
                    Id = 1,
                    DepartmentName = "CS",
                    DepartmentDescription = "Computer Science Department"
                },
                new Department
                {
                    Id=2,
                    DepartmentName="ITB",
                    DepartmentDescription="Information Technology Business Department"
                }
            });
        }
    }
}
