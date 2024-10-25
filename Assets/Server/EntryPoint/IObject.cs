using UnityEngine;

namespace Assets.EntryPoint
{
    public interface IObject 
    {
        T CreateObject<T>() where T : Behaviour;
        T CreateObject<T>(string tag) where T : Behaviour;
    }
}
