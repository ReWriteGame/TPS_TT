using UnityEngine;

public class HeroAudio : MonoBehaviour
{
    [SerializeField] private HeroAnimationEvents heroAnimationEvents;
    [SerializeField] private AudioClip landingAudioClip;
    [SerializeField] private AudioClip[] footstepAudioClips;
    [Range(0, 1)][SerializeField] private float footstepAudioVolume = 0.5f;

    private void OnEnable() => Subscribe();
    private void OnDisable() => Unsubscribe();

    private void Subscribe()
    {
        heroAnimationEvents.OnFootstepEvent += FootstepAudio;
        heroAnimationEvents.OnLandEvent += LandingAudio;
    }

    private void Unsubscribe()
    {
        heroAnimationEvents.OnFootstepEvent -= FootstepAudio;
        heroAnimationEvents.OnLandEvent -= LandingAudio;
    }

    private void FootstepAudio(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (footstepAudioClips.Length > 0)
            {
                int index = Random.Range(0, footstepAudioClips.Length);
                AudioSource.PlayClipAtPoint(footstepAudioClips[index], transform.position, footstepAudioVolume);
            }
        }
    }

    private void LandingAudio(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AudioSource.PlayClipAtPoint(landingAudioClip, transform.position, footstepAudioVolume);
        }
    }
}
