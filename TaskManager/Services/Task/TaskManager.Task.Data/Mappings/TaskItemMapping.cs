using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Core.Shared.Task.Constants;
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
                .HasMaxLength(TasksConstants.TaskTitleMaxLength);

            builder.Property(t => t.Description)
                .IsRequired(false)
                .HasMaxLength(TasksConstants.TaskDescriptionMaxLength);

            builder.Property(t => t.DueDate)
                .IsRequired(false);

            builder.Property(t => t.Status)
                .IsRequired()
                .IsRequired();
        }
    }
}
