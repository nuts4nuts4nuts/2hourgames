using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieWhenShot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnWasShot()
    {
        Debug.Log("OnWasShot received");
        Destroy(gameObject);
        SendMessageUpwards("OnGuyDied");
    }
}
