using UnityEngine;

public class GunPatronVisual : MonoBehaviour
{
    [SerializeField] private GunPatron patron;
    [SerializeField] private GameObject model;
    [SerializeField] private ParticleSystem hitPS;

    private void OnEnable() => Subscribe();
    private void OnDisable() => Unsubscribe();


    private void Subscribe()
    {
        patron.OnCollision += PlayHitEffet;
    }

    private void Unsubscribe()
    {
        patron.OnCollision -= PlayHitEffet;
    }

    private void PlayHitEffet(GameObject obj)
    {
        if (hitPS != null) hitPS.Play();
        model.SetActive(false);
    }
}
