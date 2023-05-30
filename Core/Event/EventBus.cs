using App.Interface;

namespace App.Core.Event
{
    public class EventBus
    {
        private Dictionary<Type, List<Action<object>>> eventSubscriptions;

        private EventBus()
        {
            eventSubscriptions = new Dictionary<Type, List<Action<object>>>();
        }

        private static EventBus? instance;

        public static EventBus GetInstance()
        {
            if (instance == null)
                instance = new EventBus();
            return instance;
        }

        public void Subscribe<T>(Action<T> handler) where T : IEvent
        {                                                                           
            Type eventType = typeof(T);

            if (!eventSubscriptions.ContainsKey(eventType))
            {
                eventSubscriptions[eventType] = new List<Action<object>>();
            }

            eventSubscriptions[eventType].Add(obj => handler((T)obj));
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            Type eventType = typeof(T);

            if (eventSubscriptions.ContainsKey(eventType))
                foreach (var handler in eventSubscriptions[eventType])
                    handler(@event);
        }
    }

}
