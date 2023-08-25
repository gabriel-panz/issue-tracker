using IssuesApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssuesApi.Domain.EntityMappings;

public class IssueTagMapping : IEntityTypeConfiguration<IssueTag>
{
    public void Configure(EntityTypeBuilder<IssueTag> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Tag)
            .WithMany(x => x.IssueTags)
            .HasForeignKey(x => x.TagId);

        builder.HasOne(x => x.IssueItem)
            .WithMany(x => x.IssueTags)
            .HasForeignKey(x => x.IssueId);
    }
}
