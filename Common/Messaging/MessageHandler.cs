namespace Common.Messaging
{
    public interface ICommandHandler<T> where T : ICommand
    {
        
    }
    
    public abstract class MessageHandler<T> : IMessageHandler<T>, IMessageHandler
        where T : IMessage
    {
        public abstract Result Handle(T @message);

        public Result Handle(IMessage message) => Handle((T)message);
    }
}