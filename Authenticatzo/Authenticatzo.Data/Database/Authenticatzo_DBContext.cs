/// Mohamed Ali NOUIRA
/// http://www.mohamedalinouira.com
/// https://github.com/medalinouira
/// Copyright © Mohamed Ali NOUIRA. All rights reserved.

using Authenticatzo.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authenticatzo.Data.Database
{
    public class Authenticatzo_DBContext : DbContext
    {
        public Authenticatzo_DBContext()
        {
        }

        public Authenticatzo_DBContext(DbContextOptions<Authenticatzo_DBContext> options)
            : base(options)
        {
        }


        public virtual DbSet<TblGenre> TblGenre { get; set; }
        public virtual DbSet<TblLanguage> TblLanguage { get; set; }
        public virtual DbSet<TblUser> TblUser { get; set; }
        public virtual DbSet<TblYoutubeVideoGroups> TblYoutubeVideoGroups { get; set; }
        public virtual DbSet<TblYoutubeVideos> TblYoutubeVideos { get; set; }

        public virtual DbQuery<usp_GetDashboardPlaylist> usp_GetDashboardPlaylist { get; set; }
        public virtual DbQuery<usp_GetPlaylistVideo> usp_GetPlaylistVideo { get; set; }
        public virtual DbQuery<usp_GetVideosByPlaylistId> usp_GetVideosByPlaylistId { get; set; }
        public virtual DbQuery<uspGetGroupIdHierarchy> uspGetGroupIdHierarchy { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblYoutubeVideoGroups>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblYoutubeVideos>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }

    
}
