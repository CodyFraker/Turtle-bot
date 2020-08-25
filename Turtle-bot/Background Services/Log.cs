namespace Bot.BackgroundServices
{
    using System;
    using DSharpPlus;
    using DSharpPlus.Entities;
    using Bot.Variables;

    public enum LogConsole
    {
        /// <summary>
        /// Unknown channel
        /// </summary>
        None = 0,

        /// <summary>
        /// <see cref="BotChannels.Console"/>
        /// </summary>
        Console = 1,

        /// <summary>
        /// <see cref="BotChannels.Commands"/>
        /// </summary>
        Commands = 2,

        /// <summary>
        /// <see cref="BotChannels.Jobs"/>
        /// </summary>
        Jobs = 3,

        /// <summary>
        /// <see cref="BotChannels.RoleEdits"/>
        /// </summary>
        RoleEdits = 4,

        /// <summary>
        /// <see cref="BotChannels.SBGUserInfo"/>
        /// </summary>
        UserInfo = 5,
    }

    public class BotLog
    {
        private readonly DiscordClient dClient;

        public BotLog(DiscordClient dClient)
        {
            this.dClient = dClient;
        }

        public async void Information(LogConsole consoleChannel, ulong emoji, string message)
        {
            DiscordChannel channel;

            switch (consoleChannel)
            {
                case LogConsole.Commands:
                    channel = await this.dClient.GetChannelAsync(Channels.Commands).ConfigureAwait(false);
                    break;
                case LogConsole.RoleEdits:
                    channel = await this.dClient.GetChannelAsync(Channels.RoleEdits).ConfigureAwait(false);
                    break;
                case LogConsole.UserInfo:
                    channel = await this.dClient.GetChannelAsync(Channels.Console).ConfigureAwait(false);
                    break;
                default:
                    channel = await this.dClient.GetChannelAsync(Channels.ExceptionReporting).ConfigureAwait(false);
                    break;
            }

            await channel.SendMessageAsync($"**[{DateTime.UtcNow}]** {DiscordEmoji.FromGuildEmote(this.dClient, emoji)} {message}").ConfigureAwait(false);
        }

        public async void Error(string message)
        {
            DiscordChannel logChannel = await this.dClient.GetChannelAsync(Channels.ExceptionReporting).ConfigureAwait(false);
            await logChannel.SendMessageAsync(message).ConfigureAwait(false);
        }
    }
}
