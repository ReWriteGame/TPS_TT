using UnityEngine;

public class GunPatron : Projectile
{
    [SerializeField] private GunPatronData patronData;

    public GunPatronData PatronData { get => patronData; set => patronData = value; }

    private void Start() => Subscribe();
    private void OnDestroy() => Unsubscribe();

    private void Subscribe()
    {
        OnCollision += ApplyHit;
    }

    private void Unsubscribe()
    {
        OnCollision -= ApplyHit;
    }

    private void ApplyHit(GameObject obj)
    {
        DeactivateCollision();
        StopMove();
        StopRotate();

        if (obj.TryGetComponent(out IDamageble damageble))
            damageble.Damaged(PatronData.Damage);
    }


}
