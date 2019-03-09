
using Authenticatzo.Data.Entities;
using Authenticatzo.Models;
using AutoMapper;
using System;

namespace Authenticatzo.Api.Mapping
{
    public class ApiProfile : Profile
    {
        public ApiProfile()
        {
            CreateMap<PlayList, TblYoutubeVideoGroups>()
                .ForMember(dst => dst.ChannelId, opt => opt.MapFrom(src => src.channelId))
                .ForMember(dst => dst.GenreId, opt => opt.MapFrom(src => src.genreId))
                .ForMember(dst => dst.GroupDescription, opt => opt.MapFrom(src => src.groupDescription))
                .ForMember(dst => dst.GroupImageUrl, opt => opt.MapFrom(src => src.groupImageUrl))
                .ForMember(dst => dst.GroupName, opt => opt.MapFrom(src => src.groupName))
                .ForMember(dst => dst.PlaylistId, opt => opt.MapFrom(src => src.playlistId))
                .ForMember(dst => dst.LanguageId, opt => opt.MapFrom(src => src.languageId))
                .ForMember(dst => dst.ParentGroupId, opt => opt.MapFrom(src => src.parentGroupId))
                .ForMember(dst => dst.PlayListType, opt => opt.MapFrom(src => src.playlistType))
                .ForMember(dst => dst.IsDeleted, opt => opt.MapFrom(src => false))
                .ForMember(dst => dst.CreatedDate, opt => opt.Ignore())
                .ForMember(dst => dst.Id, opt => opt.Ignore());

            CreateMap<Video, TblYoutubeVideos>()
                  .ForMember(dst => dst.VideoId, opt => opt.MapFrom(src => src.videoId))
                  .ForMember(dst => dst.VideoName, opt => opt.MapFrom(src => src.videoName))
                  .ForMember(dst => dst.SequenceNumber, opt => opt.MapFrom(src => src.sequenceNumber))
                  .ForMember(dst => dst.ImageUrl60, opt => opt.MapFrom(src => src.imageUrl60))
                  .ForMember(dst => dst.ImageUrl90, opt => opt.MapFrom(src => src.imageUrl90))
                  .ForMember(dst => dst.ImageUrl120, opt => opt.MapFrom(src => src.imageUrl120))
                   .ForMember(dst => dst.GroupId, opt => opt.MapFrom(src => src.groupId))
                  .ForMember(dst => dst.IsDeleted, opt => opt.MapFrom(src => false))
                  .ForMember(dst => dst.Id, opt => opt.Ignore())
                  .ForMember(dst => dst.CreatedDate, opt => opt.Ignore());


            CreateMap<TblYoutubeVideoGroups,PlayList > ()
               .ForMember(dst => dst.channelId, opt => opt.MapFrom(src => src. ChannelId))
               .ForMember(dst => dst.genreId, opt => opt.MapFrom(src => src.GenreId ))
               .ForMember(dst => dst.groupDescription, opt => opt.MapFrom(src => src.GroupDescription))
               .ForMember(dst => dst.groupImageUrl, opt => opt.MapFrom(src => src.GroupImageUrl))
               .ForMember(dst => dst.groupName, opt => opt.MapFrom(src => src.GroupName))
               .ForMember(dst => dst.playlistId, opt => opt.MapFrom(src => src.PlaylistId))
               .ForMember(dst => dst.languageId, opt => opt.MapFrom(src => src.LanguageId))
               .ForMember(dst => dst.parentGroupId, opt => opt.MapFrom(src => src.ParentGroupId))
               .ForMember(dst => dst.playlistType, opt => opt.MapFrom(src => src.PlayListType))
               .ForMember(dst => dst.id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dst=> dst.parentGroupSelectListItem,opt=>opt.Ignore());


            CreateMap<TblYoutubeVideos, Video>()
                 .ForMember(dst => dst.videoId, opt => opt.MapFrom(src => src.VideoId))
                 .ForMember(dst => dst.videoName, opt => opt.MapFrom(src => src.VideoName))
                 .ForMember(dst => dst.sequenceNumber, opt => opt.MapFrom(src => src.SequenceNumber))
                 .ForMember(dst => dst.imageUrl60, opt => opt.MapFrom(src => src.ImageUrl60))
                 .ForMember(dst => dst.imageUrl90, opt => opt.MapFrom(src => src.ImageUrl90))
                 .ForMember(dst => dst.imageUrl120, opt => opt.MapFrom(src => src.ImageUrl120))
                 .ForMember(dst => dst.groupId, opt => opt.MapFrom(src => src.GroupId))
                 .ForMember(dst => dst.id, opt => opt.MapFrom(src => src.Id));

            CreateMap<usp_GetDashboardPlaylist, DashboardPlaylist>();

            CreateMap<usp_GetPlaylistVideo, ViewPlaylist>();

            CreateMap<TblUser, UserModel>().ForMember(dst => dst.token, opt => opt.Ignore());
        }
    }
}
