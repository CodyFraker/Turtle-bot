namespace Bot.Guilds.Events
{
    using System.Threading.Tasks;
    using Bot.BackgroundServices;
    using Bot.BackgroundServices.Models;
    using Bot.Models;
    using DSharpPlus;
    using DSharpPlus.EventArgs;

    public class UserEvents : IEventHandler
    {
        private readonly DiscordClient dClient;
        private readonly BotContextFactory dbFactory;
        private readonly ActivityManager activityManager;

        public UserEvents(DiscordClient dClient, BotContextFactory dbFactory, BotLog log, ActivityManager activityManager)
        {
            this.dClient = dClient;
            this.dbFactory = dbFactory;
            this.activityManager = activityManager;
        }

        public void RegisterListeners()
        {
            this.dClient.GuildMemberAdded += this.OnGuildMemberAddedAsync;
            //this.dClient.GuildMemberRemoved += this.OnGuildMemberRemovedAsync;
            //this.dClient.PresenceUpdated += this.ManageNowPlayingAsync;
            //this.dClient.PresenceUpdated += this.ManageNowStreamingUsers;
        }

        private async Task OnGuildMemberAddedAsync(GuildMemberAddEventArgs args)
        {
            // Do stuff here

        }
    }
}
