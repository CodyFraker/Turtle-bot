namespace Bot.Guilds.Events
{
    using System.Threading.Tasks;
    using Bot.BackgroundServices.Models;
    using DSharpPlus;
    using DSharpPlus.EventArgs;

    public class ChannelEvents : IEventHandler
    {
        private const int PinWarningThreshold = 45;

        private readonly DiscordClient dClient;

        public ChannelEvents(DiscordClient dClient)
        {
            this.dClient = dClient;
        }

        public void RegisterListeners()
        {
            this.dClient.ChannelPinsUpdated += this.OnChannelPinsUpdatedAsync;
        }

        /// <summary>
        /// Checks channel's number of pinned posts and responds with a warning if close to 50 pins.
        /// Warning threshold is defined in <see cref="PinWarningThreshold"/>.
        /// </summary>
        /// <param name="args">Event args.</param>
        /// <returns>Task.</returns>
        private async Task OnChannelPinsUpdatedAsync(ChannelPinsUpdateEventArgs args)
        {
            int pinCount = (await args.Channel.GetPinnedMessagesAsync().ConfigureAwait(false)).Count;
            if (pinCount >= PinWarningThreshold)
            {
                await args.Channel.SendMessageAsync($"Approaching the pinned message limit [{pinCount}/50]!").ConfigureAwait(false);
            }
        }
    }
}
