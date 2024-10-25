using Assets.DI;
using Assets.EntryPoint;

namespace Assets.MVP
{
    public interface IModel
    {
        void SubscribeToEvents(IResolver container);
    }
}