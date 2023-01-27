﻿using System.Net.Http.Headers;
using FWClient.Core.Archive;
using FWClient.Core.Authentication;
using FWClient.Core.BackgroundTasks;
using FWClient.Core.BackgroundTasks.RequestBuilder;
using FWClient.Core.BackgroundTasks.ResultFactory;
using FWClient.Core.Configuration;
using FWClient.Core.Renditions;
using FWClient.Core.Tracing;
using FWClient.Core.Uploads;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FWClient.Core
{
    /// <summary>
    /// Extension methods for adding services to an <see cref="IServiceCollection" />.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// FotoWeb HTTP client name.
        /// </summary>
        internal const string FwHttpClient = "FwHttpClient";
        private const string ApiSectionName = "FotoWeb";

        /// <summary>
        /// Registers FotoWeb services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="configuration">Configuration.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <exception cref="ArgumentNullException">Thrown if configuration is not provided.</exception>
        public static IServiceCollection AddFotoWebServices(this IServiceCollection services, IConfiguration configuration)
        {
            _ = configuration ?? throw new ArgumentNullException(nameof(configuration));

            services.Configure<FotoWebConfigs>(configuration.GetSection(ApiSectionName));
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<AuthHandler>();
            services.AddScoped<RequestIdHandler>();

            services
                .AddHttpClient(FwHttpClient, (sp, client) =>
                {
                    var configs = sp.GetRequiredService<IOptions<FotoWebConfigs>>().Value;
                    client.BaseAddress = new Uri(configs.BaseAddress);
                    client.DefaultRequestHeaders
                        .Accept
                        .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                })
                .AddHttpMessageHandler<RequestIdHandler>()
                .AddHttpMessageHandler<AuthHandler>();

            services.AddScoped<IRenditionManager, RenditionManager>();
            services.AddScoped<IArchiveManager, ArchiveManager>();
            services.AddScoped<IUploadManager, UploadManager>();
            services.AddScoped<IBackgroundTaskManager, BackgroundTaskManager>();
            services.AddScoped<IBackgroundTaskRequestBuilder, BackgroundTaskRequestBuilder>();
            services.AddScoped<IBackgroundTaskResultFactory, BackgroundTaskResultFactory>();

            return services;
        }
    }
}