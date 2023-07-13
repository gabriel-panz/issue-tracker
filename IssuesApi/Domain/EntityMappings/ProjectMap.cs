using IssuesApi.Classes.Base;
using IssuesApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssuesApi.Domain.EntityMappings;

public class ProjectMap : BaseMapping<Project>
{
    public override void ConfigureSpecific(EntityTypeBuilder<Project> builder)
    {
        builder.Property(x => x.Title)
            .IsRequired(true);

        builder.Property(x => x.Description)
            .IsRequired(false);
    }
}
