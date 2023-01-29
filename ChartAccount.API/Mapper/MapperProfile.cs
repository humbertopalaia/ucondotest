﻿using AutoMapper;
using ChartAccountAPI.Models;
using ChartAccountAPI.Models.ChartAccount;

namespace ChartAccount.API.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ChartAccountDomain.ChartAccount, ChartAccountListModel>();
            CreateMap<ChartAccountInsertModel, ChartAccountDomain.ChartAccount>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ChartAccountBusiness.OperationResult, OperationResultModel>();

            //    .ForMember(dest => dest.BearStyle, opt => opt.MapFrom(src => src.Name));


            //CreateMap<Duffnization.Spotify.Domains.Search.Item, Domain.Playlist>()
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            //    .ForMember(dest => dest.Tracks, opt => opt.Ignore());

            //CreateMap<Duffnization.Spotify.Domains.GetTracksPlaylist.Item, Duffnization.Domain.Track>()
            //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Track.Name))
            //    .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => GetArtists(src.Track.Artists)))
            //    .ForMember(dest => dest.Link, opt => opt.MapFrom(src => src.Track.ExternalUrls.Spotify));

        }

        //private string GetArtists(List<Artist> artistsList)
        //{
        //    string artists = string.Empty;

        //    foreach (var artist in artistsList)
        //    {
        //        artists += artist.Name + ",";
        //    }

        //    artists.Substring(0, artists.Length - 1);

        //    return artists;
        //}
    }
}
