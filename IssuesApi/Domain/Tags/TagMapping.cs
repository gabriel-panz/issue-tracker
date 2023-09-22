using IssuesApi.Classes.Base;
using IssuesApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssuesApi.Domain.EntityMappings;

public class TagMapping : BaseMapping<Tag>
{
    public override void ConfigureSpecific(EntityTypeBuilder<Tag> builder)
    {
        builder.Property(x => x.Name);
        builder.Property(x => x.CreatedByUserId).IsRequired(true);

        builder.HasOne(x => x.CreatedByUser)
            .WithMany(x => x.Tags)
            .HasForeignKey(x => x.CreatedByUserId);
    }
}
