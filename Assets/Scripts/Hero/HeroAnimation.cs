using UnityEngine;
using Extension.Mathf;

public class HeroAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Hero hero;

    private float animationBlend;

    // animation IDs
    private int animIDSpeed;
    private int animIDGrounded;
    private int animIDJump;
    private int animIDFreeFall;
    private int animIDMotionSpeed;
    private int animIDDied;

    private int animIDGroundPowerSkill;
    private int animIDActivationSkill;
    private int animIDShoot;

    private void Start() => AssignAnimationIDs();
    private void OnEnable() => Subscribe();
    private void OnDisable() => Unsubscribe();

    private void Update()
    {
        if (animator == null) return;
        AnimationMove();
        AnimationGround();
        AnimationFall();
    }

    private void AssignAnimationIDs()
    {
        animIDSpeed = Animator.StringToHash("MoveSpeed");
        animIDGrounded = Animator.StringToHash("Grounded");
        animIDJump = Animator.StringToHash("Jump");
        animIDFreeFall = Animator.StringToHash("FreeFall");
        animIDMotionSpeed = Animator.StringToHash("MotionSpeed");

        animIDGroundPowerSkill = Animator.StringToHash("GroundPowerSkill");
        animIDActivationSkill = Animator.StringToHash("ActivateSkill");
        animIDShoot = Animator.StringToHash("GunShoot");
        animIDDied = Animator.StringToHash("Died");
    }


    private void Subscribe()
    {
        hero.OnGroundPowerCastSkill += AnimationCastGroundPowerSkill;
        hero.OnActivateSkill += AnimationActivationSkill;
        hero.Character.OnJump += AnimationJump;
        hero.OnShootAction += AnimationShoot;
        hero.OnDied += AnimationDied;
    }

    private void Unsubscribe()
    {
        hero.OnGroundPowerCastSkill -= AnimationCastGroundPowerSkill;
        hero.OnActivateSkill -= AnimationActivationSkill;
        hero.Character.OnJump -= AnimationJump;
        hero.OnShootAction -= AnimationShoot;
        hero.OnDied -= AnimationDied;
    }

    private void AnimationMove()
    {
        animationBlend = Mathf.Lerp(animationBlend, hero.TargetSpeed, Time.deltaTime * hero.SpeedChangeRate);
        if (animationBlend < 0.01f) animationBlend = 0f;

        animator.SetFloat(animIDSpeed, animationBlend);
        animator.SetFloat(animIDMotionSpeed, ExtensionsMathf.NormalizedToOne(hero.inputUserDirection).magnitude);
    }

    private void AnimationGround()
    {
        animator.SetBool(animIDGrounded, hero.Character.Grounded);

        if (!hero.Character.Grounded) return;
    }

    private void AnimationJump()
    {
        animator.SetTrigger(animIDJump);
    }

    private void AnimationFall()
    {
        if (hero.Character.Grounded) return;

        if (hero.Character.FallTimeoutDelta < 0.0f)
            animator.SetBool(animIDFreeFall, true);
    }

    private void AnimationCastGroundPowerSkill()
    {
        animator.SetTrigger(animIDGroundPowerSkill);
    }

    private void AnimationActivationSkill()
    {
        animator.SetTrigger(animIDActivationSkill);
    }

    private void AnimationShoot()
    {
        animator.SetTrigger(animIDShoot);
    }

    private void AnimationDied()
    {
        animator.SetTrigger(animIDDied);
    }
}
