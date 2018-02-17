using System.Threading.Tasks;

namespace AspNetCoreCqrsSample.Domain
{
    public interface IBookingRepository
    {
        Task<Booking> Get(long id);

        Task Save(Booking booking);
    }
}
