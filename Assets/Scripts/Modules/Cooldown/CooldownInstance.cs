using UnityEngine;

namespace Class.Cooldown
{
    public class CooldownInstance : MonoBehaviour
    {
        private static CooldownInstance instance;
        public static CooldownInstance Instance => instance;

        private void Awake()
        {
            if (instance != null && instance != this)
                Destroy(this);

            instance = this;
        }

        private void Update() => GlobalCooldown.UpdateTime(Time.deltaTime);
    }
}