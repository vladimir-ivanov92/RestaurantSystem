namespace RestaurantSystem.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using RestaurantSystem.Data;
    using RestaurantSystem.Data.Common.Repositories;
    using RestaurantSystem.Data.Models;
    using RestaurantSystem.Data.Repositories;
    using RestaurantSystem.Services.Mapping;
    using Xunit;

    public class DiscountsServiceTests
    {
        [Fact]
        public async Task TestCalculateDiscountFirstCase()
        {
            decimal discount = 0.00M;
            decimal sumPrice = 50;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var discountRepository = new EfRepository<Discount>(new ApplicationDbContext(options.Options));

            var discountService = new DiscountService(discountRepository);

            await discountService.CalculateDiscount(discount, sumPrice, "1");

            Assert.Equal(0.00M, discount);
        }

        [Fact]
        public async Task TestCalculateDiscountSecondCase()
        {
            decimal discount = 0.00M;
            decimal sumPrice = 100;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var discountRepository = new EfRepository<Discount>(new ApplicationDbContext(options.Options));

            var discountService = new DiscountService(discountRepository);

            discount = await discountService.CalculateDiscount(discount, sumPrice, "1");

            Assert.Equal(2.00M, discount);
        }

        [Fact]
        public async Task TestCalculateDiscountThirdCase()
        {
            decimal discount = 0.00M;
            decimal sumPrice = 200;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var discountRepository = new EfRepository<Discount>(new ApplicationDbContext(options.Options));

            var discountService = new DiscountService(discountRepository);

            discount = await discountService.CalculateDiscount(discount, sumPrice, "1");

            Assert.Equal(8.00M, discount);
        }

        [Fact]
        public async Task TestCalculateDiscountFourthCase()
        {
            decimal discount = 0.00M;
            decimal sumPrice = 1000;

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var discountRepository = new EfRepository<Discount>(new ApplicationDbContext(options.Options));

            var discountService = new DiscountService(discountRepository);

            discount = await discountService.CalculateDiscount(discount, sumPrice, "1");

            Assert.Equal(100.00M, discount);
        }

        [Fact]
        public async Task TestCalculateDiscountOneCodePerUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var discountRepository = new EfRepository<Discount>(new ApplicationDbContext(options.Options));

            var discountService = new DiscountService(discountRepository);

            var discount = new Discount
            {
                DiscountId = "5dc91833-2add-4a22-8ab9-663b563f6758",
                UserId = "60e060b7-4e1e-4521-bd16-46434ddaa425",
                IsApplied = false,
            };

            await discountRepository.AddAsync(discount);
            discountRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var discountDeciaml = await discountService.CalculateDiscount(0.00M, 100.00M, "60e060b7-4e1e-4521-bd16-46434ddaa425");

            Assert.Equal(1, discountRepository.All().Where(x => x.UserId == "60e060b7-4e1e-4521-bd16-46434ddaa425").Count());
        }

        [Fact]
        public async Task TestCalculateDiscountGiveCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var discountRepository = new EfRepository<Discount>(new ApplicationDbContext(options.Options));
            var discountService = new DiscountService(discountRepository);

            var discountDeciaml = await discountService.CalculateDiscount(0.00M, 100.00M, "60e060b7-4e1e-4521-bd16-46434ddaa425");

            Assert.True(discountRepository.All().Select(x => x.UserId == "60e060b7-4e1e-4521-bd16-46434ddaa425").FirstOrDefault());
        }

        [Fact]
        public async Task TestCalculateDiscountCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var discountRepository = new EfRepository<Discount>(new ApplicationDbContext(options.Options));
            var discountService = new DiscountService(discountRepository);

            var discountEntity = new Discount
            {
                DiscountId = "5dc91833-2add-4a22-8ab9-663b563f6758",
                UserId = "60e060b7-4e1e-4521-bd16-46434ddaa425",
                IsApplied = true,
            };

            await discountRepository.AddAsync(discountEntity);
            discountRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var discount = discountService.CalculateDiscountCode(100.00M, "60e060b7-4e1e-4521-bd16-46434ddaa425");

            Assert.Equal(10.00M, discount);
        }

        [Fact]
        public async Task TestCalculateDiscountCodeNotApplied()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var discountRepository = new EfRepository<Discount>(new ApplicationDbContext(options.Options));
            var discountService = new DiscountService(discountRepository);

            var discountEntity = new Discount
            {
                DiscountId = "5dc91833-2add-4a22-8ab9-663b563f6758",
                UserId = "60e060b7-4e1e-4521-bd16-46434ddaa425",
                IsApplied = false,
            };

            await discountRepository.AddAsync(discountEntity);
            discountRepository.SaveChangesAsync().GetAwaiter().GetResult();

            var discount = discountService.CalculateDiscountCode(100.00M, "60e060b7-4e1e-4521-bd16-46434ddaa425");

            Assert.Equal(0.00M, discount);
        }

        [Fact]
        public async Task TestCheckDiscountCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var discountRepository = new EfRepository<Discount>(new ApplicationDbContext(options.Options));
            var discountService = new DiscountService(discountRepository);

            var discountEntity = new Discount
            {
                DiscountId = "5dc91833-2add-4a22-8ab9-663b563f6758",
                UserId = "60e060b7-4e1e-4521-bd16-46434ddaa425",
                IsApplied = false,
            };

            await discountRepository.AddAsync(discountEntity);
            discountRepository.SaveChangesAsync().GetAwaiter().GetResult();

            bool check = await discountService.CheckDiscountCode(discountEntity.DiscountId);

            Assert.True(check);
        }

        [Fact]
        public async Task TestGetDiscountCode()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                  .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var discountRepository = new EfRepository<Discount>(new ApplicationDbContext(options.Options));
            var discountService = new DiscountService(discountRepository);

            var discountEntity = new Discount
            {
                DiscountId = "5dc91833-2add-4a22-8ab9-663b563f6758",
                UserId = "60e060b7-4e1e-4521-bd16-46434ddaa425",
                IsApplied = false,
            };

            await discountRepository.AddAsync(discountEntity);
            discountRepository.SaveChangesAsync().GetAwaiter().GetResult();

            string discountCode = discountService.GetDiscountCode("60e060b7-4e1e-4521-bd16-46434ddaa425");

            Assert.Equal("5dc91833-2add-4a22-8ab9-663b563f6758", discountCode);
        }
    }
}
