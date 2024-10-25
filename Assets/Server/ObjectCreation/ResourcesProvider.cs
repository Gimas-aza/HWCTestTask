using UnityEngine;

namespace Assets.ObjectCreation
{
    public class ResourcesProvider : IObjectProvider
    {
        public GameObject LoadResource<T>() where T : Behaviour
        {
            var result = Resources.Load<GameObject>(typeof(T).Name);
            if (result == null)
            {
                Debug.LogError("Not found resource: " + typeof(T).Name);
            }

            return result;
        }

        public GameObject LoadResource<T>(string tag) where T : Behaviour
        {
            var result = Resources.Load<GameObject>(tag);
            if (result == null)
            {
                Debug.LogError("Not found resource: " + typeof(T).Name);
            }

            return result;
        }
    }
}