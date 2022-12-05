using Bot.Variables;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.Core.Services.Features
{
    public class FeatureControlEvents : Feature
    {
        private readonly DiscordClient dClient;
        private readonly FeatureManager featureManager;

        public FeatureControlEvents(DiscordClient dClient, FeatureManager featureManager)
        {
            this.dClient = dClient;
            this.featureManager = featureManager;
        }

        public override string Name => "FeatureControl";

        public override string Description => "Feature controls using embeds";

        public override bool Protected => true;

        public override Task Enable()
        {
            this.dClient.GuildAvailable += this.OnGuildAvailable;

            return base.Enable();
        }

        private static DiscordEmbed CreateFeatureEmbed(Feature feature)
        {
            return new DiscordEmbedBuilder
            {
                Title = feature.Name,
                Description = feature.Description,
                Timestamp = DateTime.UtcNow,
                Color = feature.Enabled ? new DiscordColor(21, 137, 255) : new DiscordColor(131, 126, 124),
            };
        }

        // Ignore timestamps
        private static bool IdenticalEmbed(DiscordEmbed a, DiscordEmbed b)
        {
            return a.Title == b.Title
                && a.Description == b.Description
                && a.Color == b.Color;
        }

        private async Task OnGuildAvailable(DiscordClient dClient, GuildCreateEventArgs args)
        {
            if (args.Guild.Id != Guilds.DodeDuke)
            {
                return;
            }

            await this.dClient.Guilds[Guilds.DodeDuke].GetEmojisAsync();

            DiscordChannel settingsChannel = await this.dClient.GetChannelAsync(Channels.Settings);
            IReadOnlyList<DiscordMessage> messages = await settingsChannel.GetMessagesAsync(this.featureManager.Features.Count);

            for (int i = 0; i < this.featureManager.Features.Count; i++)
            {
                Feature feature = this.featureManager.Features[i];
                DiscordMessage message = messages.Where(m => m.Embeds[0]?.Title == feature.Name).FirstOrDefault();
                DiscordEmbed newEmbed = CreateFeatureEmbed(feature);

                if (message == null)
                {
                    message = await settingsChannel.SendMessageAsync(embed: newEmbed);
                    await message.CreateReactionAsync(DiscordEmoji.FromGuildEmote(this.dClient, FeatureEmojis.ToggleOff));
                    await message.CreateReactionAsync(DiscordEmoji.FromGuildEmote(this.dClient, FeatureEmojis.ToggleOn));
                }
                else if (!IdenticalEmbed(message.Embeds[0], newEmbed))
                {
                    await message.ModifyAsync(embed: newEmbed);
                }
            }
        }
    }
}
