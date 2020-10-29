using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int dieCount = 0;

    public Spawner spawner;

    public Timer timer;

    [SerializeField]
    private IGameState[] states;

    private IGameState activeState
    {
        get
        {
            return states[iActiveState];
        }
    }

    private int iActiveState;

    private bool halted = false;

    public static GameManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        halted = false;
        Debug.Log("Game manager started");
        iActiveState = 0;
        activeState.DoStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (halted) return;

        activeState.DoUpdate();
        if (activeState.IsDone())
        {
            iActiveState++;
            if (iActiveState == states.Length)
            {
                Debug.Log("Game over");
                halted = true;
                return;
            }
            activeState.DoStart();
        }
    }

    void OnGuyDied()
    {
        dieCount++;
    }
}
