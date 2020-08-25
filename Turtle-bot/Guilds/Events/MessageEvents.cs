namespace Bot.Guilds.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Bot.BackgroundServices.Models;
    using Bot.Models;
    using Bot.Variables;
    using DSharpPlus;
    using DSharpPlus.Entities;
    using DSharpPlus.EventArgs;

    public class MessageEvents : IEventHandler
    {

        private readonly DiscordClient dClient;
        private readonly BotContextFactory dbFactory;

        public MessageEvents(DiscordClient dClient, BotContextFactory dbFactory)
        {
            this.dClient = dClient;
            this.dbFactory = dbFactory;
        }

        public void RegisterListeners()
        {
            this.dClient.MessageCreated += this.OnMessageCreatedAsync;
        }

        private async Task OnMessageCreatedAsync(MessageCreateEventArgs args)
        {
            // Ignore the message if the author is a bot account.
            if (args.Author.IsBot == true)
            {
                return;
            }
        }
    }
}
