﻿namespace Bot.Core.Services
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IServiceRegistrant
    {
        void ConfigureServices(IServiceCollection services);
    }
}
