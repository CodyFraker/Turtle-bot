using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Core.Contexts;

namespace Bot.Core.Commands
{
    [SlashModuleLifespan(SlashModuleLifespan.Scoped)]
    internal class GeneralSlashCommands : ApplicationCommandModule
    {
        private readonly BotContext db;

        public GeneralSlashCommands(BotContext db)
        {
            this.db = db;
        }

        [SlashCommand("ping", "This command is to be used when you think the bot is frozen or stuck. It'll reply with **pong** {ms}")]
        public async Task PingPongAsync(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"pong! Latency: {ctx.Client.Ping}ms").AsEphemeral(true));
        }

        [SlashCommand("links", "Responds with a message containing important links for getting around the community.")]
        public async Task DevelopmentURLAsync(InteractionContext ctx)
        {
            DiscordEmbedBuilder linksEmbed = new DiscordEmbedBuilder
            {
                Footer = new DiscordEmbedBuilder.EmbedFooter
                {
                    Text = $"༼ つ ◕_◕ ༽つ GIBE LINKS",
                },
                Color = new DiscordColor(95, 95, 95),
                Timestamp = DateTime.UtcNow,
                Title = $"**Important Links**",
            };

            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().AddEmbed(linksEmbed.Build()).AsEphemeral(true));
        }

    }
}
