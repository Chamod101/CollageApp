using CollageApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollageApp.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(e => e.StudentName).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Address).IsRequired().HasMaxLength(255);
            builder.Property(e => e.Email).IsRequired(false).HasMaxLength(200);

            builder.HasData(new List<Student>()
            {
                new Student
                {
                    Id = 1,
                    StudentName = "Admin",
                    Address="Polonnaruwa",
                    Email="Admin@gmail.com",
                    DOB=new DateTime(01/01/1999)
                }
            });
        }
    }
}
