using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fithub.Database.Models.Configurations
{
    class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
    {
        public void Configure(EntityTypeBuilder<Exercise> builder)
        {
            builder.HasKey(e => new { e.Id, e.CategoryId, e.UserId });

            builder.HasOne(e => e.Category)
                .WithMany(c => c.Exercises)
                .HasForeignKey(e => new { e.CategoryId, e.UserId });

            builder.Property(e => e.Id).ValueGeneratedOnAdd();
            builder.Property(e => e.Start).IsRequired();
        }
    }
}
