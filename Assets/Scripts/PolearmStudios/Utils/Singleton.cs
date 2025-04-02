namespace PolearmStudios.Utils
{
    public class Singleton<T> where T : UnityEngine.Object
    {
        public readonly T Instance;
        private Singleton() { }
        public Singleton(T instance)
        {
            if (Instance != null)
            {
                UnityEngine.Object.Destroy(instance);
                return;
            }
            Instance = instance;
        }
    }
}
