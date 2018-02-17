using AspNetCoreCqrsSample.Application.Interfaces;
using AspNetCoreCqrsSample.Domain;
using AspNetCoreCqrsSample.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreCqrsSample.Infrastructure
{
    public static class ServicesExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookingDbContext>(options => options.UseSqlite("Filename=cqrs-sample.db"), ServiceLifetime.Transient);
            services.AddTransient<IUnitOfWork, BookingDbContextUnitOfWork>();
            services.AddTransient<IBookingRepository, BookingRepository>();

            using (var dbInstaller = new BookingDbContextInstaller(services.BuildServiceProvider().GetService<BookingDbContext>()))
            {
                dbInstaller.Install();
            }
        }
    }
}
