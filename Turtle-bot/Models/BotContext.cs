namespace Bot.Models
{
    using Bot.BackgroundServices.Models;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public class BotContext : DbContext
    {
        public BotContext(DbContextOptions<BotContext> options)
            : base(options)
        {
        }

        public DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.None,
            };

            base.OnModelCreating(modelBuilder);
        }
    }
}
