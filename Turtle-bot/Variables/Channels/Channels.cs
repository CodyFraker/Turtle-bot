namespace Bot.Variables
{
    public static class Channels
    {
        /// <summary>
        /// CHANGE THIS
        /// A channel where the bot should log its contents to.
        /// </summary>
        public const ulong loggingChannel = 123456123462134532;

        /// <summary>
        /// Settings channels for enabling and disabling features.
        /// </summary>
        public static ulong Settings { get; set; } = 123456123462134532;

        /// <summary>
        /// Channel for bot owners/trusted users.
        /// </summary>
        public static ulong BotMods { get; set; } = 771825700419141642;
    }
}
