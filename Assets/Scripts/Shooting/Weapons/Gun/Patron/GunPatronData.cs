using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "GunPatronData", menuName = "ScriptableObjects/GunPatronData")]
public class GunPatronData : ScriptableObject
{
    [SerializeField] private float forceMove = 10;
    [SerializeField] private float damage = 10;

    public float ForceMove => forceMove;
    public float Damage => damage;
}
