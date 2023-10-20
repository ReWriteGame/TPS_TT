using UnityEngine;

public class GunVisual : MonoBehaviour
{
    [SerializeField] private Gun gun;
    [SerializeField] private ParticleSystem fireEffect;

    private void OnEnable() => Subscribe();
    private void OnDisable() => Unsubscribe();


    private void Subscribe()
    {
        gun.OnShoot += Fire;
    }

    private void Unsubscribe()
    {
        gun.OnShoot -= Fire;
    }

    private void Fire()
    {
        fireEffect.Play();
    }

}
