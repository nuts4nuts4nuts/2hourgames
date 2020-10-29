using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State2 : IGameState
{
    private bool hasSpawned = false;
    private bool hasBarked = false;

    [SerializeField]
    private AudioSource sound;

    public override void DoStart()
    {
        GameManager.Instance.spawner.Spawn();
    }

    public override void DoUpdate()
    {
        if (GameManager.Instance.dieCount >= 2 && !hasSpawned)
        {
            GameManager.Instance.timer.DoAfterDelay(1.5f, GameManager.Instance.spawner.Spawn);
            hasSpawned = true;
        }
        if (GameManager.Instance.dieCount >= 3 && !hasBarked)
        {
            GameManager.Instance.timer.DoAfterDelay(0.25f, sound.Play);
            hasBarked = true;
        }
    }

    public override bool IsDone()
    {
        return GameManager.Instance.dieCount >= 3;
    }
}
