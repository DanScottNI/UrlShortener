﻿using Microsoft.EntityFrameworkCore;

#nullable disable

namespace UrlBitlyClone.Core.Context
{
    /// <summary>
    /// Entity Framework Core database context.
    /// </summary>
    public partial class UrlBitlyCloneContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBitlyCloneContext"/> class.
        /// </summary>
        public UrlBitlyCloneContext()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UrlBitlyCloneContext"/> class.
        /// </summary>
        /// <param name="options">The database context options.</param>
        public UrlBitlyCloneContext(DbContextOptions<UrlBitlyCloneContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the URL shortenings.
        /// </summary>
        public virtual DbSet<UrlShortForm> UrlShortForms { get; set; }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<UrlShortForm>(entity =>
            {
                entity.ToTable("UrlShortForm", "Url");

                entity.Property(e => e.FullUrl)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ShortenedUrl)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasIndex(e => e.ShortenedUrl).IsUnique();
            });
        }
    }
}
