using System.Collections.Generic;
using UnityEngine;
using System;
using Extension.LayerMask;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class ZoneTargets : MonoBehaviour
{
    [SerializeField] private LayerMask searchlayerMask;
    [SerializeField] private List<TargetAim> targetsInZone;

    public List<TargetAim> TargetsInZone => targetsInZone;

    private Collider collider;
    private Rigidbody rb;

    public Action<TargetAim> OnTargetEnter;
    public Action<TargetAim> OnTargetExit;

    private void Awake() => Initialize();
    private void OnTriggerEnter(Collider other) => EnterTargetLogic(other);
    private void OnTriggerExit(Collider other) => ExitTargetLogic(other);

    private void Initialize()
    {
        collider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        collider.isTrigger = true;
    }

    private void EnterTargetLogic(Collider other)
    {
        if (!searchlayerMask.ContainLayer(other.gameObject.layer)) return;
        if (other.gameObject.TryGetComponent(out TargetAim targetAim))
        {
            AddToTargetList(targetAim);
        }
    }

    private void ExitTargetLogic(Collider other)
    {
        if (other.gameObject.TryGetComponent(out TargetAim targetAim))
        {
            RemoveFromTargetList(targetAim);
        }
    }

    private void AddToTargetList(TargetAim target)
    {
        if (targetsInZone.Contains(target)) return;
        target.OnDestroyTarget += RemoveFromTargetList;
        targetsInZone.Add(target);
        OnTargetEnter?.Invoke(target);
    }

    private void RemoveFromTargetList(TargetAim target)
    {
        if (!targetsInZone.Contains(target)) return;
        target.OnDestroyTarget -= RemoveFromTargetList;
        targetsInZone.Remove(target);
        OnTargetExit?.Invoke(target);
    }
}
