using IssuesApi.Classes.Base;
using IssuesApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssuesApi.Domain.EntityMappings;

public class TagMapping : BaseMapping<Tag>
{
    public override void ConfigureSpecific(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(x => x.Name);
    }
}
