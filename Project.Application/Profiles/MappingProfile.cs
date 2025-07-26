using AutoMapper;
using Project.Application.DTOs.User;
using Project.Domain.Entities;
using NetTopologySuite.Geometries;
using System.Linq;
using System.Collections.Generic;
using System;
using Project.Application.DTOs.Product;
using Project.Domain.Entities.ProductModels;
using Project.Application.DTOs.DataTable;
using DNTPersianUtils.Core;
using Project.Application.DTOs.Category;
using Project.Application.Helpers;
using Project.Domain.Enums;
using Project.Application.DTOs.Pages;
using Project.Application.DTOs.MenuItem;
using Project.Application.DTOs.News;
using Project.Application.DTOs.NewsCategory;
using Project.Domain.Entities.News;
using Project.Application.DTOs.Ticket;
using Project.Application.DTOs.TicketMessage;
using Project.Application.DTOs.DiscountCode;
using Project.Application.DTOs.PurchaseRequest;
using Project.Application.DTOs.CartItem;
using Project.Application.DTOs.BakeryCategory;
using System.Reflection;

namespace Project.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile(GeometryFactory geometryFactory)
        {

            #region user
            CreateMap<JWTUser, UserDTO>().ReverseMap();
            CreateMap<UserDTO, UserProfile>().ReverseMap();
            CreateMap<JWTUser, EditProfile>().ReverseMap();
            CreateMap<JWTUser, UserProfile>()
                //.ForMember(dest => dest.Adverts, opt => opt.MapFrom(src => src.Adverts.Select(s => s.Title)))
                .ReverseMap();

            CreateMap<ApplicationUser, UserDataTable>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToPersianDateTextify(false)))
                .ReverseMap();
            #endregion

            #region Product
            CreateMap<UpsertProduct, Product>()
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.ProductType, opt => opt.Ignore())
                .ForMember(dest => dest.Url, opt => opt.Ignore())
                .ForMember(dest => dest.Attributes, opt => opt.Ignore())
                .ForMember(dest => dest.BannerUrl, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.TypeIsColor, opt => opt.Ignore())
                .ForMember(dest => dest.Galey, opt => opt.Ignore())
                .ForMember(dest => dest.ProductType, opt => opt.Ignore())
                .ForMember(dest => dest.Attributes, opt => opt.Ignore())
                .ForMember(dest => dest.BannerUrl, opt => opt.Ignore())
                .ForMember(dest => dest.BannerImage, opt => opt.MapFrom(src => src.BannerUrl))
                .ForMember(dest => dest.IsPrice, opt => opt.Ignore())
                .ForMember(dest => dest.DiscountPrice, opt => opt.MapFrom(src => (src.Discount != null ? src.price - ((src.Discount / 100) * src.price) : null)))
                .ForMember(dest => dest.TypeIsColor, opt => opt.MapFrom(src => src.Status == ProductTypeStatus.Color ? true : false))
                .ForMember(dest => dest.IsPrice, opt => opt.MapFrom(src => src.Status == ProductTypeStatus.Singleprice ? true : false))
                .ReverseMap();

            CreateMap<Product, ProductDataTable>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToPersianDateTextify(false)))
                .ReverseMap();
            #endregion

            #region pages

            CreateMap<Pages, PagesDTO>()
                .ReverseMap();

            CreateMap<Pages, UpsertPages>()
                .ReverseMap();
            #endregion
            #region menu item

            CreateMap<MenuItem, MenuItemDTO>()
                .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.Parent== null ? "ندارد" : src.Parent.Name))
                .ForMember(dest => dest.ChildsCount, opt => opt.MapFrom(src => src.Childs.Count()))
                .ReverseMap();

            CreateMap<MenuItem, UpsertMenuItem>()
                .ReverseMap();
            #endregion
            #region Category
            CreateMap<Category, CategoryDataTable>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToPersianDateTextify(false)))
                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId == null ? 0 : src.ParentId))
                .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.Parent == null ? "ندارد" : src.Parent.Name))
                .ForMember(dest => dest.ChildsCount, opt => opt.MapFrom(src => src.Childs.Where(w => w.IsActive == true).Count()))
                .ForMember(dest => dest.Rote, opt => opt.MapFrom(src => (src.Parent != null ? (src.Parent.Parent != null ? ("(" + src.Parent.Parent.Name + ") ") : ""):"") + (src.Parent != null ? ("(" + src.Parent.Name + ") ") : "") + src.Name ))
                .ReverseMap();
            CreateMap<Category, UpsertCategory>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            #endregion

            #region Discount Code
            CreateMap<DiscountCode, DiscountCodeDataTable>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToPersianDateTextify(false)))
                .ForMember(dest => dest.ExpireTime, opt => opt.MapFrom(src => src.ExpireTime.ToShortPersianDateString(false)))
                .ForMember(dest => dest.DiscountCodeTypeEnum, opt => opt.MapFrom(src => src.DiscountCodeType))
                .ForMember(dest => dest.DiscountCodeType, opt => opt.MapFrom(src => src.DiscountCodeType.GetDisplayAttributeFrom()))
                .ReverseMap();
            CreateMap<DiscountCode, DiscountCodeDTO>().ReverseMap();
            #endregion
            #region News Categroy
            CreateMap<NewsCategory, UpsertNewsCategory>().ReverseMap();
            CreateMap<NewsCategory, NewsCategoryDTO>()
                  .ForMember(dest => dest.NewsCount, opt => opt.MapFrom(src => src.News.Count()))
                  .ReverseMap();
            #endregion

            #region News
            CreateMap<News, NewsDTO>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();
            CreateMap<News, UpsertNews>()
                .ForMember(dest => dest.Time, opt => opt.Ignore())
                .ForMember(dest => dest.Date, opt => opt.Ignore())
                .ForMember(dest => dest.Image, opt => opt.Ignore())
                .ForMember(dest => dest.Url, opt => opt.Ignore())
                .ReverseMap();
            #endregion
            #region ticket

            CreateMap<Ticket, TicketDTO>()
                .ForMember(dest => dest.TotalMessages,
                    opt => opt.MapFrom(src => src.Messages == null ? 0 : src.Messages.Where(w => w.IsActive == true).Count()))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User == null ? "" : src.User.FirstName + " " + src.User.LastName))
                .ReverseMap();
            CreateMap<CreateTicket, Ticket>()
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore());

            #endregion

            #region ticketmessage

            CreateMap<TicketMessage, TicketMessageDTO>().ReverseMap();
            CreateMap<CreateTicketMessage, TicketMessage>()
                .ForMember(dest => dest.SeenByAdmin, opt => opt.Ignore())
                .ForMember(dest => dest.SeenByUser, opt => opt.Ignore())
                .ForMember(dest => dest.SentByUser, opt => opt.Ignore())
                .ForMember(dest => dest.AttachmentUrl, opt => opt.Ignore());

            #endregion


            #region purchaseRequest

            CreateMap<PurchaseRequest, PurchaseRequestFactorDTO>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToShortPersianDateTimeString(false)))
                .ForMember(dest => dest.CartItems, opt => opt.MapFrom(src => src.CartItems.Where(x => x.IsActive)))
                .ReverseMap();
            #endregion

            #region cartItem

            CreateMap<CartItem, CartItemDTO>()
                .ForMember(dest => dest.ItemId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();
            #endregion

            #region banner

            CreateMap<Banner, BannerDTO>()
                .ReverseMap();

            CreateMap<Banner, UpsertBanner>()
                .ReverseMap();
            #endregion

        }
    }
}
