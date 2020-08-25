namespace Bot.Models
{
    using System;
    using Microsoft.EntityFrameworkCore;

    public class BotContextFactory
    {
        public string ConnectionString
        {
            get
            {
                return $"Host={Environment.GetEnvironmentVariable("DB_HOST")};"
                + $"Port={Environment.GetEnvironmentVariable("DB_PORT")};"
                + $"Username={Environment.GetEnvironmentVariable("DB_USER")};"
                + $"Password={Environment.GetEnvironmentVariable("DB_PASS")};"
                + $"Database={Environment.GetEnvironmentVariable("DB_NAME")};"
                + $"SSL Mode=none";
            }
        }

        public BotContext Create()
        {
            DbContextOptionsBuilder<BotContext> optionsBuilder = new DbContextOptionsBuilder<BotContext>();
            optionsBuilder.UseMySql(this.ConnectionString);
            return new BotContext(optionsBuilder.Options);
        }
    }
}
