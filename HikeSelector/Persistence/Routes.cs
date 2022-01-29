using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HikeSelector.Persistence;
using HikeSelector.Resources;
using Splat;
using SQLite;

namespace HikeSelector
{
    public class Routes
    {
        private readonly SQLiteAsyncConnection _database;

        public Routes(SQLiteAsyncConnection? database = null)
        {
            _database = database 
                        ?? Locator.Current.GetService<SQLiteAsyncConnection>()
                        ?? throw new ArgumentNullException(nameof(database));
            _database.CreateTableAsync<RouteEntity>().Wait();
#if DEBUG
            _database.DeleteAllAsync<RouteEntity>().Wait();
            _database.InsertAllAsync(GenerateRoutes()).Wait();
#endif
        }

#if DEBUG
        private IEnumerable<RouteEntity> GenerateRoutes()
        {
            return new List<RouteEntity>
            {
                new()
                {
                    Name = "Route 1",
                    Country = AppResources.Switzerland,
                    RouteLength = RouteLength.Short,
                    TravelTimeToRoute = TravelTimeToRoute.Short
                },
                new()
                {
                    Name = "Route 2",
                    Country = AppResources.Switzerland,
                    RouteLength = RouteLength.Medium,
                    TravelTimeToRoute = TravelTimeToRoute.None
                },
                new()
                {
                    Name = "Route 3",
                    Country = AppResources.Switzerland,
                    RouteLength = RouteLength.Long,
                    TravelTimeToRoute = TravelTimeToRoute.Short
                },
                new()
                {
                    Name = "Route 4",
                    Country = AppResources.Switzerland,
                    RouteLength = RouteLength.Medium,
                    TravelTimeToRoute = TravelTimeToRoute.DayTrip
                },
            };
        }
#endif

        public Task<List<RouteEntity>> Get() => _database.Table<RouteEntity>().ToListAsync();

        public Task Add(RouteEntity routeEntity)
        {
            var timeStamp = DateTimeOffset.Now;
            routeEntity.ChangedAt = timeStamp;
            routeEntity.CreatedAt = timeStamp;
            return _database.InsertAsync(routeEntity);
        }

        public Task Remove(int routeId) => _database.DeleteAsync<RouteEntity>(routeId);
    }
}