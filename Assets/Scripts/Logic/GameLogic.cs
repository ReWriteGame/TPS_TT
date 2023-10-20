using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private Hero hero;
    [SerializeField] private Nightshade nightshade;
    [SerializeField] private MouseCursorState mouseCursorState;

    public UnityEvent OnDiedHero;
    public UnityEvent OnDiedEnemy;

    private void Awake()
    {
        mouseCursorState.Focus();
        Subscribe();
    }

    private void OnDestroy() => Unsubscribe();

    public void Subscribe()
    {
        hero.OnDied += OnDiedHero.Invoke;
        nightshade.OnDied += OnDiedEnemy.Invoke;
    }

    public void Unsubscribe()
    {
        hero.OnDied -= OnDiedHero.Invoke;
        nightshade.OnDied += OnDiedEnemy.Invoke;
    }
}
