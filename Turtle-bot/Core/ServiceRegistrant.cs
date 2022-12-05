using DSharpPlus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Extensions.Logging;
using System;
using Bot.Core.Contexts;
using Bot.Core.Discord;
using Bot.Core.Services;
using Bot.Core.Services.Jobs;
using Bot.Core.Services.Features;

namespace Bot.Core
{
    public class ServiceRegistrant : IServiceRegistrant
    {
        public void ConfigureServices(IServiceCollection services)
        {
            ServerVersion version = ServerVersion.AutoDetect(BotContext.ConnectionString);

            services.AddSingleton<BotLog>()
                .AddDbContextPool<BotContext>(options => options.UseMySql(BotContext.ConnectionString, version))
                .AddSingleton<ActivityManager>()

#pragma warning disable CA2000 // Dispose objects before losing scope
                .AddSingleton(new DiscordClient(new DiscordConfiguration
                {
                    Intents = DiscordIntents.DirectMessages | DiscordIntents.GuildBans | DiscordIntents.GuildMembers
                        | DiscordIntents.GuildMessageReactions | DiscordIntents.GuildMessages | DiscordIntents.GuildPresences
                        | DiscordIntents.Guilds,
                    LoggerFactory = new SerilogLoggerFactory(Log.Logger),
                    MessageCacheSize = 0,
                    Token = Environment.GetEnvironmentVariable("BOT_TOKEN"),
                    TokenType = TokenType.Bot,
                }))
#pragma warning restore CA2000 // Dispose objects before losing scope
                .AddSingleton<FeatureManager>()
                .AddSingleton<JobManager>();
        }
    }
}
