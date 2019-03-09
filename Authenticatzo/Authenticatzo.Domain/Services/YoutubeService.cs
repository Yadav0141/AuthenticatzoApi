using Authenticatzo.Interfaces.IRepositories;
using Authenticatzo.Models;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Authenticatzo.Domain.Services
{
  public  class YoutubeService
    {
        public IYoutubeRepository _youtubeRepository;
        public YoutubeService(IYoutubeRepository youtubeRepository) {
            this._youtubeRepository = youtubeRepository;
        }
        public Models.Video GetVideoById(string videoId,string apiKey)
        {

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = this.GetType().ToString()
            });
            var videoSearchRequest =  youtubeService.Videos.List("snippet,contentDetails,statistics");
            videoSearchRequest.Id = videoId;
            var videoResponse = videoSearchRequest.Execute();
            Models.Video videoModel = new Models.Video();
            var youtubeVideo = videoResponse.Items.FirstOrDefault();
            videoModel.videoId = youtubeVideo.Id; 
            videoModel.videoName = youtubeVideo.Snippet.Title;
            videoModel.imageUrl60 = youtubeVideo.Snippet.Thumbnails?.Standard?.Url;
            videoModel.imageUrl90 = youtubeVideo.Snippet.Thumbnails?.Medium?.Url;
            videoModel.imageUrl120 = youtubeVideo.Snippet.Thumbnails?.High?.Url;

            return videoModel;


        }

        public SearchListResponse GetVideosBySearchTerm(string searchTerm, string apiKey)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = this.GetType().ToString()
            });

            var searchListRequest = youtubeService.Search.List("snippet,id");
            searchListRequest.Q = searchTerm;
            searchListRequest.MaxResults = 50;
            return  searchListRequest.Execute();

       
        }

        public PlaylistVideoModel GetVideosByPlaylistId(string playlistId,string apiKey)
        {
           
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = this.GetType().ToString()
            });
            
         
            List<PlaylistItemListResponse> lstPlaylistItemListResponse = new List<PlaylistItemListResponse>();
            var playListResponse = new PlaylistItemListResponse();
            var nextPageToken = "";
            while (nextPageToken != null)
            {
                var searchPlaylistRequest = youtubeService.PlaylistItems.List("snippet,contentDetails,id");
                searchPlaylistRequest.PlaylistId = playlistId;
                searchPlaylistRequest.PageToken = nextPageToken; 
                searchPlaylistRequest.MaxResults = 50;
                playListResponse = searchPlaylistRequest.Execute();
                lstPlaylistItemListResponse.Add(playListResponse);
                nextPageToken= playListResponse.NextPageToken;

            }
            var playlistVideoModel = new PlaylistVideoModel();
            playlistVideoModel.playlistDetail = new PlayList();
            playlistVideoModel.playlistVideos = new List<Models.Video>();
            var firstplaylist = 0;
            foreach (var playlist in lstPlaylistItemListResponse)
            {
                if (playlist.Items.Count > 0)
                {
                    if (firstplaylist == 0)
                    {
                   
                        var firstPlaylist = playlist.Items.First();
                        playlistVideoModel.playlistDetail.channelId = firstPlaylist.Snippet.ChannelId;
                        playlistVideoModel.playlistDetail.playlistId = firstPlaylist.Snippet.PlaylistId;
                        playlistVideoModel.playlistDetail.groupName = firstPlaylist.Snippet.Title;
                        playlistVideoModel.playlistDetail.groupDescription = firstPlaylist.Snippet.Description;
                        playlistVideoModel.playlistDetail.groupImageUrl = firstPlaylist.Snippet.Thumbnails?.Medium?.Url;
                        playlistVideoModel.totalResults = playlist.PageInfo.TotalResults;
                        playlistVideoModel.playlistDetail.languageId = -1;
                        playlistVideoModel.playlistDetail.genreId = -1;
                        
                    }
                    foreach (var item in playlist.Items)
                    {
                        Models.Video video = new Models.Video();
                        video.videoId = item.Snippet.ResourceId.VideoId;
                        video.videoName = item.Snippet.Title;
                        firstplaylist = firstplaylist + 1;
                        video.sequenceNumber = firstplaylist;
                        video.imageUrl60 = item.Snippet.Thumbnails?.Standard?.Url;
                        video.imageUrl90 = item.Snippet.Thumbnails?.Medium?.Url;
                        video.imageUrl120 = item.Snippet.Thumbnails?.High?.Url;
                        playlistVideoModel.playlistVideos.Add(video);

                    }
                }
               
            }
           return playlistVideoModel;
          }

        public List<SelectListItemModel> GetLanguageSelectList()
        {

            return this._youtubeRepository.GetLanguageSelectList();
        }

        public List<SelectListItemModel> GetGenreSelectList()
        {
            return this._youtubeRepository.GetGenreSelectList();
        }

        public Guid? SavePlaylist(PlaylistVideoModel model)
        {

            return this._youtubeRepository.SavePlaylist(model); 
        }

        public Guid? SaveVideo(Authenticatzo.Models.Video video)
        {
            return this._youtubeRepository.SaveVideo(video);


        }

        public PlaylistVideoModel GetPlaylistVideoModelById(Guid playlistId)
        {

            return this._youtubeRepository.GetPlaylistVideoModelById(playlistId);
        }


        public int deleteVideoById(Guid videoId)
        {
            return this._youtubeRepository.deleteVideoById(videoId);
        }

        public List<SelectListItemStringModel> GetParentPlaylistSelectItems(int playlistType, string text)
        {
            return this._youtubeRepository.GetParentPlaylistSelectItems(playlistType, text);
        }
    }
}
