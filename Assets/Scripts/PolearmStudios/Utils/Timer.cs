using System;

namespace PolearmStudios.Utils
{
    public class Timer
    {
        public float TimeElapsed { get => current; }
        public float TimeRemaining { get => duration - current; }
        public float Duration {  get => duration; }
        public bool IsRunning { get => isActive; }

        float duration;
        float current;

        bool isActive;
        bool hasAutoReset;
        bool hasAutoRestart;
        bool hasAutoStop;

        public Action OnComplete;

        private Timer() { }

        public void Start()
        {
            isActive = true;
        }
        
        public void Pause()
        {
            if (!isActive) { return; }
            isActive = false;
        }

        public void Update(float deltaTime)
        {
            if(!isActive) { return; }
            current += deltaTime;
            if (current >= duration) 
            { 
                OnComplete?.Invoke();
                if (hasAutoReset) { Reset();  return;}
                if (hasAutoRestart) { Restart(); return; }
                if (hasAutoStop) { isActive = false; return; }
            }
        }

        public void Restart()
        {
            isActive = false;
            current = 0;
            Start();
        }

        public void Reset()
        {
            isActive = false;
            current = 0;
        }

        public void Clear()
        {
            isActive = false;
            current = 0;
            duration = 0;
        }

        public class Builder
        {
            readonly float duration;
            bool autoReset;
            bool autoRestart;
            bool autoStop;
            private Builder() { }
            public Builder(float _duration) => duration = _duration; 
            /// <summary>
            /// The retuned Timer will automatically stop and reset its current to 0
            /// </summary>
            /// <returns></returns>
            public Builder WithAutoReset() { autoReset = true; return this; }
            /// <summary>
            /// The retuned Timer will announce completion and then restart
            /// </summary>
            /// <returns></returns>
            public Builder WithAutoRestart() { autoRestart = true; return this; }
            /// <summary>
            /// The returned Timer will stop announcing completion on duration expiring
            /// </summary>
            /// <returns></returns>
            public Builder WithAutoStop() { autoStop = true; return this; }
            public Timer Build()
            {
                return new()
                {
                    duration = duration,
                    hasAutoReset = autoReset,
                    hasAutoRestart = autoRestart,
                    hasAutoStop = autoStop
                };
            }
        }
    }
}