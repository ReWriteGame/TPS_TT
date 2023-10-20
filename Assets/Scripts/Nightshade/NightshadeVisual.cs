using Class.Score.Visual;
using UnityEngine;

public class NightshadeVisual : MonoBehaviour
{
    [SerializeField] private NightshadeAnimationEvents animationEvents;
    [SerializeField] private Nightshade nightshade;
    [SerializeField] private ScoreVisualText visual;
    [SerializeField] private ParticleSystem attackEffect;

    private void Start()
    {
        visual.SetScoreCounter(nightshade.Health);
        animationEvents.OnAttackEvent += AttackEffect;
    }
    private void OnDestroy()
    {
        animationEvents.OnAttackEvent -= AttackEffect;
    }

    private void AttackEffect(AnimationEvent animationEvent)
    {
        attackEffect.Play();
    }
}
