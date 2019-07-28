using Dictionary.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dictionary.Domain.DbMappings
{
    internal class DefinitionMapping : EntityConfiguration<Definition>
    {
        public override void AddConfiguration(EntityTypeBuilder<Definition> builder)
        {
            builder.ToTable("Definition").HasKey(k => k.Id);
            builder.HasMany(x => x.Translations).WithOne().HasForeignKey("DefinitionId").IsRequired();
            builder.Metadata.FindNavigation(nameof(Definition.Translations)).SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(x => x.Related).WithOne().HasForeignKey("DefinitionId").IsRequired();
            builder.Metadata.FindNavigation(nameof(Definition.Related)).SetPropertyAccessMode(PropertyAccessMode.Field);

            //builder.HasQueryFilter(x => x.ProductGroupId == ProductGroup.Schools.Id);
        }
    }
}
