using System.Threading.Tasks;
using AspNetCoreMessagingSample.Models;
using SMessaging.Abstractions;

namespace AspNetCoreMessagingSample.Application
{
    public class SampleMessageHandler : IHandleMessage<SampleMessage>
    {
        private readonly ISampleDbContext dbContext;

        public SampleMessageHandler(ISampleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<MessageResult> Handle(SampleMessage message)
        {
            // for example:
            // var entity = new SampleEntity { ... };
            // dbContext.SampleEntity.Add(entity);
            // await dbContext.SaveChangesAsync();
            // return Task.FromResult(new MessageResult(200, new { Id = entity.Id }));

            return Task.FromResult(new MessageResult(200, "OK"));
        }
    }
}
