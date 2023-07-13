using IssuesApi.Classes.Base.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IssuesApi.Classes.Base;

public abstract class BaseMapping<T> : IEntityTypeConfiguration<T> 
    where T : class, IEntity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Id).IsUnique(true);

        builder.Property(x => x.CreatedAt)
            .IsRequired(true)
            .HasDefaultValue(DateTime.Now);
        
        builder.Property(x => x.UpdatedAt)
            .IsRequired(false);

        builder.Property(x => x.DeletedAt)
            .IsRequired(false);
        
        ConfigureSpecific(builder);
    }

    public abstract void ConfigureSpecific(EntityTypeBuilder<T> builder);
    
}
