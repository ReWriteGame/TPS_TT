using UnityEngine;
using System;
using Extension.LayerMask;


[SelectionBase]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SphereCollider collider;
    [SerializeField] private ProjectileDisposeType disposeType;
    [SerializeField] private bool useMultipleDispose = false;
    [SerializeField] private LayerMask collidinglayerMask;

    public Rigidbody Rb => rb;
    public SphereCollider Collider => collider;
    public bool UseMultipleDispose => useMultipleDispose;

    public Action<GameObject> OnCollision;
    public Action<GameObject> OnCollisionFirst;

    private bool isProjectileÑollided = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!useMultipleDispose && isProjectileÑollided) return;
        if (!collidinglayerMask.ContainLayer(other.gameObject.layer)) return;

        OnCollision?.Invoke(other.gameObject);
        if (!isProjectileÑollided) OnCollisionFirst?.Invoke(other.gameObject);
        isProjectileÑollided = true;
    }

    public void ActivateCollision() => collider.enabled = true;
    public void DeactivateCollision() => collider.enabled = false;
    public void StopMove() => rb.velocity = Vector3.zero;
    public void StopRotate() => rb.angularVelocity = Vector3.zero;

}
