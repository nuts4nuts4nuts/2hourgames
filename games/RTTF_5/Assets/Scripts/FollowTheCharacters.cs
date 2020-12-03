using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheCharacters : MonoBehaviour
{
    private GameObject[] players;
    private Camera cam;
    public float minimumDistance = 3f;

    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        cam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float maxPlayerDistance = 0f;
        Vector3 averagePosition = Vector3.zero;
        foreach(var player in players)
        {
            averagePosition += player.transform.position;

            foreach (var other in players)
            {
                float distance = Vector3.Distance(player.transform.position, other.transform.position);
                if(distance > maxPlayerDistance)
                {
                    maxPlayerDistance = distance;
                }
            }
        }

        averagePosition /= players.Length;

        cam.transform.SetPositionAndRotation(averagePosition, cam.transform.rotation);
        cam.orthographicSize = Mathf.Max(maxPlayerDistance, minimumDistance);
    }
}
