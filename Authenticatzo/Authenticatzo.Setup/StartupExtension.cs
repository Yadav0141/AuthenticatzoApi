

using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Authenticatzo.Data.Database;
using Authenticatzo.Domain.Services;
using Authenticatzo.Interfaces.IRepositories;
using Authenticatzo.Infrastructure.Repositories;
using Authenticatzo.Infrastructure.Helpers;

namespace Authenticatzo.Setup
{
    public static class StartupExtension
    {
        public static IServiceCollection AddDependencies(this IServiceCollection _iServiceCollection)
        {

            #region Helper
            _iServiceCollection.AddTransient<IPasswordHasher, PasswordHasher>();
            #endregion

            #region Services


            _iServiceCollection.AddTransient<YoutubeService,YoutubeService>();
            _iServiceCollection.AddTransient<DashboardService, DashboardService>();
            _iServiceCollection.AddTransient<PlaylistVideoService, PlaylistVideoService>();


            #endregion

            #region AutoMapper
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles("Authenticatzo" + ".Infrastructure","Authenticatzo" + ".Api");
            });
            #endregion

            #region Repositories
           
            _iServiceCollection.AddTransient<IUserRepository, UserRepository>();
            _iServiceCollection.AddTransient<IYoutubeRepository, YoutubeRepository>();
            _iServiceCollection.AddTransient<IDashboardRepository, DashboardRepository>();
            _iServiceCollection.AddTransient<IPlaylistVideoRepository, PlaylistVideoRepository>();

            #endregion

            return _iServiceCollection;
        }
    }
}
