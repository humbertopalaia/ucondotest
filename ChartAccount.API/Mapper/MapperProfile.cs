using AutoMapper;
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
                .ForMember(dest => dest.LevelCode, opt => opt.MapFrom(src => GetLevelCode(src.Code)))
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<ChartAccountBusiness.OperationResult, OperationResultModel>();

        }

        private int GetLevelCode(string code)
        {
            var codes = code.Split('.');

            if (codes.Length > 0)
                return Convert.ToInt32(codes.Last());
            else
                return Convert.ToInt32(code);

        }

    }
}
