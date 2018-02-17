using System.Collections;
using System.Collections.Generic;

namespace AspNetCoreCqrsSample.Domain
{
    public abstract class Aggregate
    {
        private readonly List<object> events = new List<object>();

        public void LoadEvents(IEnumerable events)
        {
            foreach (var @event in events)
            {
                Apply(@event, false);
            }
        }

        public IEnumerable Events => events.ToArray();

        protected void Apply(object @event, bool appendEvent = true)
        {
            GetType()
                .GetMethod(@event.GetType().Name, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                .Invoke(this, new object[] { @event });
            if (appendEvent)
            {
                events.Add(@event);
            }
        }
    }
}
