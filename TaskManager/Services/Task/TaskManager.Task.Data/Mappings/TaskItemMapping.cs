using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Tasks.Domain;

namespace TaskManager.Tasks.Data.Mappings
{
    public class TaskItemMapping : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Description)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(t => t.DueDate)
                .IsRequired(false);

            builder.Property(t => t.Status)
                .IsRequired()
                .IsRequired();
        }
    }
}
