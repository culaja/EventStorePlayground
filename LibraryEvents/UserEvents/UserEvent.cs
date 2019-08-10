using Common.Messaging;

namespace LibraryEvents.UserEvents
{
    public abstract class UserEvent : DomainEvent
    {
        public UserEvent(string id) 
            : base("User", id)
        {
        }
    }
}