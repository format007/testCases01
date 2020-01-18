using LinkResolver.Models.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkResolver.Models.Data
{
    public class LinkDBContext : DbContext
    {
        public LinkDBContext()
        {
        }

        public LinkDBContext(DbContextOptions<LinkDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Link> Links { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Link>(entity =>
            {
                entity.HasKey(e => e.Id).ForSqlServerIsClustered(false);

                entity.HasIndex(e => e.LinkHash).IsUnique();
                entity.HasIndex(e => e.ShortLink).IsUnique();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("(getdate())");
                entity.HasIndex(e => e.CreatedAt).ForSqlServerIsClustered();

                entity.Property(e => e.LinkHash).IsRequired().HasMaxLength(64).IsUnicode(false);
                entity.Property(e => e.LongLink).IsRequired().HasMaxLength(2000);

                entity.Property(e => e.ShortLink).IsRequired()
                    .HasMaxLength(6).HasColumnType("char(6)").IsUnicode(false);
            });
        }
    }
}
