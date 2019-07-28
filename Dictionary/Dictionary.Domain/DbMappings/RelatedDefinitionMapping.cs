//using Dictionary.Domain.Utils;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Dictionary.Domain.DbMappings
//{
//    internal class RelatedDefinitionMapping : EntityConfiguration<RelatedDefinition>
//    {
//        public override void AddConfiguration(EntityTypeBuilder<RelatedDefinition> builder)
//        {
//            builder.ToTable("RelatedDefinition");
//            builder.HasOne(x => x.RelatedDefinition).WithOne().HasForeignKey("RelatedDefinitionId").IsRequired();
//        }
//    }
//}
