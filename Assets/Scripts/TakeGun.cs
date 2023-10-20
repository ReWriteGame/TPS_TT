using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// system for demonstration
public class TakeGun : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    private BoxCollider trigger;
    private void Awake()
    {
        trigger = gameObject.AddComponent<BoxCollider>();
        trigger.isTrigger = true;
        trigger.size = Vector3.one * 1.2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Hero hero))
        {
            hero.Slots.AddObject(gun);
            Destroy(trigger);
        }
    }
}
