using DSharpPlus;
using Serilog;

namespace Bot.Variables
{
    public static class VariableManager
    {
        public static void ApplyVariableScopes(DiscordClient dClient)
        {
            bool isDev = dClient.CurrentUser.Id == Users.developmentBot;

            if (isDev)
            {
                Log.Warning("Development account detected, overriding variables.");
                Channels.Settings = Channels.Settings;
                Guilds.MockFakeStub();
                Users.MockFakeStub();
            }
        }
    }
}
