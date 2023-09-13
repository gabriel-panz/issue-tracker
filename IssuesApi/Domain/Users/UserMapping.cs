using IssuesApi.Classes.Base;
using IssuesApi.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssuesApi.Domain.EntityMappings;

public class UserMapping : BaseMapping<User>
{
    public override void ConfigureSpecific(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => x.Login)
            .IsUnique();

        builder.Property(x => x.Nickname)
            .HasMaxLength(63)
            .IsRequired(false);

        builder.Property(x => x.Email)
            .HasMaxLength(320)
            .IsRequired(false);

        builder.Property(x => x.Login)
            .HasMaxLength(320)
            .IsRequired(true);

        builder.Property(x => x.Password)
            .HasMaxLength(255)
            .IsRequired(true);
    }
}