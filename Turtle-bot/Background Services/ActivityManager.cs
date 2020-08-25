namespace Bot.BackgroundServices
{
    using System;
    using System.Net.WebSockets;
    using System.Threading.Tasks;
    using Bot.Variables;
    using DSharpPlus;
    using DSharpPlus.Entities;

    /// <summary>
    /// Manages Bot's activity and streaming statuses.
    /// </summary>
    public class ActivityManager
    {
        private const int AutoResetMs = 3000;
        private const string DefaultActivity = "Intruder on Steam!";

        private readonly DiscordClient dClient;

        private ulong streamOwnerID = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityManager"/> class.
        /// </summary>
        /// <param name="dClient">Discord socket client.</param>
        public ActivityManager(DiscordClient dClient)
        {
            this.dClient = dClient;
        }

        /// <summary>
        /// Clears the stream and returns to <see cref="DefaultActivity"/>.
        /// </summary>
        /// <returns>Awaitable task.</returns>
        public async Task ClearStreamAsync()
        {
            this.streamOwnerID = 0;
            await this.ResetActivityAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Checks whether a user is the current stream's owner.
        /// </summary>
        /// <param name="streamerID">Discord user id.</param>
        /// <returns>Boolean.</returns>
        public bool IsStreamOwner(ulong streamerID) => this.streamOwnerID == streamerID;

        /// <summary>
        /// Resets Bot's activity to <see cref="DefaultActivity"/>.
        /// </summary>
        /// <returns>Awaitable task.</returns>
        public Task ResetActivityAsync() => this.TrySetActivityAsync(DefaultActivity);

        /// <summary>
        /// Sets Bot's activity if not currently streaming.
        /// </summary>
        /// <param name="activity">Activity description.</param>
        /// <param name="activityType">Activity type.</param>
        /// <param name="autoReset">Automatically switch back to <see cref="DefaultActivity"/> after <see cref="AutoResetMs"/>.</param>
        /// <returns>Awaitable task.</returns>
        public async Task TrySetActivityAsync(string activity, ActivityType activityType = ActivityType.Playing, bool autoReset = false)
        {
            if ((this.dClient.CurrentUser.Presence.Activity.ActivityType == ActivityType.Streaming && activity != DefaultActivity)
                || Program.SocketState != WebSocketState.Open)
            {
                return;
            }

            await this.dClient.UpdateStatusAsync(new DiscordActivity
            {
                ActivityType = activityType,
                Name = activity,
            }).ConfigureAwait(false);

            if (autoReset)
            {
                await Task.Delay(AutoResetMs).ConfigureAwait(false);
                await this.ResetActivityAsync().ConfigureAwait(false);
            }
        }
    }
}
