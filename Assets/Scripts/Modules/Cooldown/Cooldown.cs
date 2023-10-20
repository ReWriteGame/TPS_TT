using UnityEngine;
using Class.Score;
using System;

namespace Class.Cooldown
{
    public class Cooldown // test
    {
        private Score.Score counter;
        private bool reloaded = true;
        private bool isRunning = false;

        private bool ignoreTimeScale;//todo 
        private float timeScale = 1;

        public bool Reloaded => reloaded;
        public float ReloadTime => counter.MaxValue;
        public float CurrentTime => counter.Value;

        public Cooldown(float duration, bool startStateReloaded = true, bool startStateRuning = false) : this(startStateReloaded, startStateRuning)
        {
            counter.SetData(new ScoreData(0, 0, duration));
        }

        public Cooldown(bool startStateReload = true, bool startStateRuning = false)
        {
            reloaded = startStateReload;
            isRunning = startStateRuning;
            counter = new Score.Score();
            counter.OnReachMaxValue += ResetAndUse;
            GlobalCooldown.AddToList(this);
        }

        ~Cooldown()
        {
            counter.OnReachMaxValue -= ResetAndUse;
            GlobalCooldown.RemoveFromList(this);
        }

        public void SpendTime(float time, bool useTimeScale = true)
        {
            time = useTimeScale ? time * timeScale : time;
            if (isRunning && !reloaded) counter.IncreaseValue(time);
        }

        public void SetDuration(float duration)
        {
            counter.SetData(new ScoreData(0, 0, duration));
            ResetAndUse();
        }

        public void Play()
        {
            if (!reloaded) isRunning = true;
        }

        public void Pause()
        {
            isRunning = false;
        }

        public void ResetAndUse()
        {
            Reset(true);
        }

        public void ResetAndUnuse()
        {
            Reset(false);
        }

        public void Reset(bool useState)
        {
            reloaded = useState;
            isRunning = false;
            counter.SetData(new ScoreData(0, 0, counter.MaxValue));
        }

        //////////////////////// wrapper methods //////////////////////// 
        public bool Activate(float duration)
        {
            if (reloaded)
            {
                SetDuration(duration);
                ResetAndUnuse();
                Play();
                return true;
            }
            return false;
        }
        public bool Activate()
        {
            if (reloaded)
            {
                ResetAndUnuse();
                Play();
                return true;
            }
            return false;
        }
    }
}

