using AutoMapper;
using ISPAN.Izakaya.Dtos;
using Izakayamvc.ViewModels.Vms;

namespace Izakayamvc.ViewModels.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // 定義 DemoDto 與 DemoVm 之間的對應關係, 可以雙向轉換
            // CreateMap<DemoDto, DemoVm>().ReverseMap();
            CreateMap<CartSettingDto, CartSettingVm>().ReverseMap();
            CreateMap<CartStatusDto, CartStatusVm>().ReverseMap();
            CreateMap<CombinedOrderDto, CombinedOrderVm>().ReverseMap();
            CreateMap<CouponDto, CouponVm>().ReverseMap();
            CreateMap<MemberDto, MemberVm>().ReverseMap();
            CreateMap<OrderDetailDto, OrderDetailVm>().ReverseMap();
            CreateMap<OrderDiscountDto, OrderDiscountVm>().ReverseMap();
            CreateMap<OrderPaymentDto, OrderPaymentVm>().ReverseMap();
            CreateMap<OrderDto, OrderVm>().ReverseMap();
            CreateMap<PaymentMethodDto, PaymentMethodVm>().ReverseMap();
            CreateMap<PaymentStatusDto, PaymentStatusVm>().ReverseMap();
            CreateMap<ProductDto, ProductVm>().ReverseMap();
            CreateMap<SeatCartDto, SeatCartVm>().ReverseMap();
            CreateMap<SeatDto, SeatVm>().ReverseMap();
            CreateMap<ActivityDto, ActivityVm>().ReverseMap();
            CreateMap<ArticleDto, ArticleVm>().ReverseMap();
            CreateMap<ArticleDto, ArticleVm>().ReverseMap();
            CreateMap<OrderPaymentDto, PaymentsHistoryVm>().ReverseMap();

            // Ctiy轉型為 CityVm,這時 DisplayOrder 會被無視,因為 CityVm 沒有 DisplayOrder
            //CreateMap<City, CityVm>();

            // ref https://docs.automapper.org/en/latest/Projection.html
            // 由CityVm轉型為City,這時 DisplayOrder 會被設定為10
            // 如果不寫這個, DisplayOrder 就會是預設值 0
            //CreateMap<CityVm, City>()
            //		.ForMember(dest => dest.DisplayOrder,
            //				option => option.MapFrom(src => 10));
        }
    }
}