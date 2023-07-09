using System;

namespace Prototyping.MainProtoyping.Scripts
{
    // T must extend the Event base class
    public interface Observable<T> where T : Event
    {
        bool addObserver(Observer<T> observer);
        bool removeObserver(Observer<T> observer);
        void notifyObservers(T eventToFire);
    }
}