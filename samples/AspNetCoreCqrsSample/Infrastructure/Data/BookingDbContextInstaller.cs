using System;

namespace AspNetCoreCqrsSample.Infrastructure.Data
{
    public class BookingDbContextInstaller : IDisposable
    {
        private BookingDbContext dbContext;

        public BookingDbContextInstaller(BookingDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Dispose()
        {
            dbContext?.Dispose();
            dbContext = null;
        }

        public void Install()
        {
            dbContext.Database.EnsureCreated();
        }
    }
}
