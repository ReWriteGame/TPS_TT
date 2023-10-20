using System;
using UnityEngine;

public class TargetAim : MonoBehaviour
{
    public Action<TargetAim> OnDestroyTarget;

    private void OnDestroy() => OnDestroyTarget?.Invoke(this);
}
