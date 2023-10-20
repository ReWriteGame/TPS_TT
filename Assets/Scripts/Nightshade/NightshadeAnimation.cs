using UnityEngine;

public class NightshadeAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Nightshade nightshade;

    // animation IDs
    private int animIDDied;
    private int animIDDamaged;
    private int animIDAttack;

    private void Start() => AssignAnimationIDs();
    private void OnEnable() => Subscribe();
    private void OnDisable() => Unsubscribe();

    private void AssignAnimationIDs()
    {
        animIDDied = Animator.StringToHash("Died");
        animIDDamaged = Animator.StringToHash("Damaged");
        animIDAttack = Animator.StringToHash("Attack");
    }

    private void Subscribe()
    {
        nightshade.OnDied += AnimationDied;
        nightshade.OnDamaged += AnimationDamaged;
        nightshade.OnAttackAction += AnimationAttack;
    }

    private void Unsubscribe()
    {
        nightshade.OnDied -= AnimationDied;
        nightshade.OnDamaged -= AnimationDamaged;
        nightshade.OnAttackAction -= AnimationAttack;
    }

    private void AnimationDied()
    {
      animator.SetBool(animIDDied, true);
    }

    private void AnimationDamaged(float value)
    {
        animator.SetTrigger(animIDDamaged);
    }

    private void AnimationAttack()
    {
        animator.SetTrigger(animIDAttack);
    }
}
