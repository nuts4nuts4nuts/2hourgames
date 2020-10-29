using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGameState : MonoBehaviour
{
    public abstract void DoStart();
    public abstract void DoUpdate();
    public abstract bool IsDone();
}
