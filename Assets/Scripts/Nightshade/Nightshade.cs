using Class.Score;
using UnityEngine;
using System;

public class Nightshade : MonoBehaviour, IDamageble
{
    [SerializeField] private NightshadeAnimationEvents animationEvents;
    [SerializeField] private Score health;
    [SerializeField] private float damage = 1000;

    [SerializeField] private ZoneTargets zoneTargets;
    [SerializeField] private TargetAim mainTarget;


    public Action OnDied;
    public Action OnAttackAction;
    public Action<float> OnDamaged;

    public Score Health => health;

    private void Awake() => Subscribe();
    private void OnDestroy() => Unsubscribe();

    private void Subscribe()
    {
        health.OnReachMinValue += Died;
        health.OnDecreaseValue += OnDamaged;
        zoneTargets.OnTargetEnter += SelectTarget;
        animationEvents.OnAttackEvent += AttackWrapper;
    }

    private void Unsubscribe()
    {
        health.OnReachMinValue -= Died;
        health.OnDecreaseValue -= OnDamaged;
        zoneTargets.OnTargetEnter -= SelectTarget;
        animationEvents.OnAttackEvent -= AttackWrapper;
    }

    public void Damaged(float value)
    {
        health.DecreaseValueLimited(value);
    }
    
    public void Died()
    {
        OnDied?.Invoke();
    }

    public void SelectTarget(TargetAim newTarget)
    {
        if (newTarget.gameObject.TryGetComponent(out Hero hero))
        {
            mainTarget = newTarget;
            AttackAction();
        }
    }

    public void AttackWrapper(AnimationEvent animationEvent) => Attack();
    public void Attack()
    {
        if (mainTarget.gameObject.TryGetComponent(out IDamageble hero))
        {
            hero.Damaged(damage);
        }
    }

    public void AttackAction()
    {
        OnAttackAction?.Invoke();
    }

}
