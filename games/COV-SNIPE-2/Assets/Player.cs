using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            var ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hit))
            {
                hit.collider.gameObject.SendMessage("OnWasShot");
            }
        }
    }
}
