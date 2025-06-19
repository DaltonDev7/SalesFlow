using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesFlow.Persistence.EntityConfiguration
{
    public class TableConfiguration : IEntityTypeConfiguration<Tables>
    {
        public void Configure(EntityTypeBuilder<Tables> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Tables");
        }
    }
}
