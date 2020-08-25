namespace Bot.Guilds.Events
{
    using System.Threading.Tasks;
    using Bot.BackgroundServices.Models;
    using Bot.Variables;
    using DSharpPlus;
    using DSharpPlus.EventArgs;

    public class GuildEvents : IEventHandler
    {
        private readonly DiscordClient dClient;

        public GuildEvents(DiscordClient dClient)
        {
            this.dClient = dClient;
        }

        public void RegisterListeners()
        {
            this.dClient.GuildAvailable += this.OnGuildAvailableAsync;
        }

        private async Task OnGuildAvailableAsync(GuildCreateEventArgs args)
        {
            await args.Guild.RequestMembersAsync().ConfigureAwait(false);
        }
    }
}
