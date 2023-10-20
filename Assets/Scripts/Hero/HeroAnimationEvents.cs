using System;
using UnityEngine;


//the script must be on an object with an animator to work AnimationEvent
//these events invoke in FBX animation files
[RequireComponent(typeof(Animator))]
public class HeroAnimationEvents : MonoBehaviour
{
    public Action<AnimationEvent> OnLandEvent;
    public Action<AnimationEvent> OnFootstepEvent;
    public Action<AnimationEvent> OnShootEvent;

    //Actions and skills
    public Action<AnimationEvent> OnActivateSkill;
    public Action<AnimationEvent> OnGroundPowerSkillStart;
    public Action<AnimationEvent> OnGroundPowerSkillEnd;

    private void OnFootstep(AnimationEvent animationEvent)
    {
        OnFootstepEvent?.Invoke(animationEvent);
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        OnLandEvent?.Invoke(animationEvent);
    }

    private void OnGroundPowerSkillStartCast(AnimationEvent animationEvent)
    {
        OnGroundPowerSkillStart?.Invoke(animationEvent);
    }
    private void OnGroundPowerSkillEndCast(AnimationEvent animationEvent)
    {
        OnGroundPowerSkillEnd?.Invoke(animationEvent);
    }

    private void OnActivateSkillCast(AnimationEvent animationEvent)
    {
        OnActivateSkill?.Invoke(animationEvent);
    }

    private void OnShoot(AnimationEvent animationEvent)
    {
        OnShootEvent?.Invoke(animationEvent);
    }
}
