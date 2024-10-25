using UnityEngine;

namespace Assets.ObjectCreation
{
    public interface IObjectProvider
    {
        GameObject LoadResource<T>() where T : Behaviour;
        GameObject LoadResource<T>(string tag) where T : Behaviour;
    }
}
