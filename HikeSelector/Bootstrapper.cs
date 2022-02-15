using System;
using System.IO;
using AutoMapper;
using HikeSelector.Persistence;
using HikeSelector.ViewModels;
using HikeSelector.Views;
using ReactiveUI;
using Sextant;
using Sextant.XamForms;
using Splat;
using SQLite;

namespace HikeSelector
{
    public static class Bootstrapper
    {
        public static IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Route, RouteEntity>()
                    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.ChangedAt, opt => opt.Ignore())
                    .ReverseMap();
                cfg.CreateMap<RouteViewModel, RouteEntity>()
                    .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                    .ForMember(dest => dest.ChangedAt, opt => opt.Ignore())
                    .ReverseMap();
            });

            return mapperConfiguration.CreateMapper();
        }
        
        public const string DbName = "hikeSelector.db";

        public static readonly string DbPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DbName);

        public static void RegisterServices()
        {
            Locator.CurrentMutable.Register(CreateMapper);
            Locator.CurrentMutable.Register(() => new Routes());
            Locator.CurrentMutable.Register(() => new SQLiteAsyncConnection(DbPath));
            
            Locator.CurrentMutable.Register(() => new DashboardViewModel());
        }

        public static void RegisterNavigation()
        {
            Locator
                .CurrentMutable
                .RegisterNavigationView(() => new NavigationView(RxApp.MainThreadScheduler, RxApp.TaskpoolScheduler, ViewLocator.Current))
                .RegisterParameterViewStackService()
                .RegisterViewForNavigation(() => new DashboardPage(), () => new DashboardViewModel())
                .RegisterViewForNavigation(() => new EditRoutePage(), () => new RouteViewModel());
        }
    }
}