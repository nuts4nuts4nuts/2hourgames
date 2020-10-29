using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class State1 : IGameState
{
    [SerializeField]
    private AudioSource sound;

    private bool setTimers = false;

    private bool done = false;

    override public bool IsDone()
    {
        return done;
    }

    override public void DoStart()
    {
        GameManager.Instance.spawner.Spawn();
        done = false;
        setTimers = false;
    }

    override public void DoUpdate()
    {
        if (!setTimers && GameManager.Instance.dieCount >= 1)
        {
            GameManager.Instance.timer.DoAfterDelay(0.5f, sound.Play);
            GameManager.Instance.timer.DoAfterDelay(0.5f, () => done = true);
            setTimers = true;
        }
    }
}
