using Microsoft.EntityFrameworkCore;
using Bot.Features.FAQ.Models;

namespace Bot.Core.Contexts
{
    public partial class BotContext : DbContext
    {
        public DbSet<Faq> Faqs { get; set; }
    }
}
