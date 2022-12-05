namespace Bot.Core.Contexts
{
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;
    using System;
    using Bot.Core.Services.Features;
    using Bot.Core.Services.Jobs;
    using Bot.Features.FAQ.Models;
    using System.Text.RegularExpressions;

    public partial class BotContext : DbContext
    {
        public BotContext(DbContextOptions<BotContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Pulls variables stored in the .env file.
        /// </summary>
        public static string ConnectionString =>
            $"Host={Environment.GetEnvironmentVariable("DB_HOST")};"
            + $"Port={Environment.GetEnvironmentVariable("DB_PORT")};"
            + $"Username={Environment.GetEnvironmentVariable("DB_USER")};"
            + $"Password={Environment.GetEnvironmentVariable("DB_PASS")};"
            + $"Database={Environment.GetEnvironmentVariable("DB_NAME")};"
            + $"SSL Mode=none";

        public DbSet<Job> Jobs { get; set; }

        public DbSet<FeatureStatus> FeatureStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
            };

            modelBuilder.Entity<Faq>()
                .Property(s => s.Regex)
                .HasConversion(s => s.ToString(), s => new Regex(s, RegexOptions.Compiled | RegexOptions.IgnoreCase));

            base.OnModelCreating(modelBuilder);
        }
    }
}
