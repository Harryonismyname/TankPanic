using System;

namespace PolearmStudios.Utils
{
    public class StateMachine<T>
    {
        public T State { get => state; }
        T state;

        public event Action<T> OnStateChanged;

        public void ChangeState(T newState)
        {
            state = newState;
            OnStateChanged?.Invoke(state);
        }
    }
}