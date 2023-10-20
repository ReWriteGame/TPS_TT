using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroEffects : MonoBehaviour
{
    [SerializeField] private Hero hero;
    [SerializeField] private HeroAnimationEvents heroAnimationEvents;
    [SerializeField] private Vector3 offsetGroundEffect;

    private void Start() => Subscribe();
    private void OnDestroy() => Unsubscribe();

    private void Subscribe()
    {

    }

    private void Unsubscribe()
    {

    }

    private void CreateDestroyAndGlobalPosition(GameObject obj, Vector3 localSpawnPoint, float leaveTime)
    {
        GameObject clone = Instantiate(obj);
        clone.transform.position = transform.TransformPoint(localSpawnPoint);
        Destroy(clone, leaveTime);
    }

}
