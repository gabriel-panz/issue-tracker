using IssuesApi.Classes.Base;
using IssuesApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssuesApi.Domain.EntityMappings;

public class IssueItemMap : BaseMapping<IssueItem>
{
    public override void ConfigureSpecific(EntityTypeBuilder<IssueItem> builder)
    {
        builder.Property(x => x.Title)
            .IsRequired(true);

        builder.Property(x => x.Description)
            .IsRequired(false);

        builder.Property(x => x.Status)
            .IsRequired(true)
            .HasDefaultValue<IssueStatus>(IssueStatus.OPEN);

        builder.HasOne(x => x.Project)
            .WithMany(x => x.ProjectItems)
            .HasForeignKey(x => x.ProjectId);
    }
}
