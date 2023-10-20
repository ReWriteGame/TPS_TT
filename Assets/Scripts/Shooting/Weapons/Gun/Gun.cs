using System;
using UnityEngine;

public class Gun : MonoBehaviour, IHandUsable
{
    [SerializeField] private GunPatron patron;
    [SerializeField] private GunPatronData overrideDataGun;
    [SerializeField] private Transform flightStartingPoint;
    [SerializeField] private PlayerCrosshair crosshair;
    //[SerializeField] private float damageScale = 1;


    public Action OnShoot;
    
    private void OverridePatronData()
    {
        if (overrideDataGun == null) return;
        patron.PatronData = overrideDataGun;
    }

    public void PerformAttack()
    {
        OverridePatronData();
        Projectile projectile = Instantiate(patron, flightStartingPoint.position, flightStartingPoint.rotation);
       
        Vector3 moveDirection = crosshair.IsTargetHit ?
            (crosshair.TargetHitPoint - flightStartingPoint.position) :
            Camera.main.transform.forward * 100 - crosshair.TargetHitPoint;// it’s possible to come up with more precise logic if we don’t have a hitpoint 

        projectile.Rb.AddForce(moveDirection.normalized * patron.PatronData.ForceMove, ForceMode.Impulse);
    }

    public void UseHandObject()
    {
        PerformAttack();
        OnShoot?.Invoke();
    }
}
