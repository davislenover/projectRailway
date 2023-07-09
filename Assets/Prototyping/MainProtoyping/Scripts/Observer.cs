
namespace Prototyping.MainProtoyping.Scripts
{
    public interface Observer<T> where T : Event
    {
        void handleUpdate(T eventFired);
    }
}