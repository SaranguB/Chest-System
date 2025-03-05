using UnityEngine;

namespace ChestSystem
{
    public class GenericMonoSingelton<T> : MonoBehaviour where T : GenericMonoSingelton<T>
    {
        private static T instance;
        public static T Instane { get { return instance; } }

        protected virtual void Awake()
        {
            if(instance == null)
            {
                instance = (T)this;
            }
            else
            {
                Destroy(instance);
            }
        }
    }
}
