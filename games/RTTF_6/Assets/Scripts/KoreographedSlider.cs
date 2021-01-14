using SonicBloom.Koreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoreographedSlider : MonoBehaviour
{
    [EventID]
    public string eventID;
    public Transform moveTo;

    private Vector3 startPosition;
    private Vector3 endPosition;

    // Start is called before the first frame update
    void Start()
    {
        Koreographer.Instance.RegisterForEventsWithTime(eventID, SetPosition);
        startPosition = transform.position;
        endPosition = transform.GetChild(0).position;
    }

    void SetPosition(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
    {
        if(evt.HasCurvePayload())
        {
            float curveValue = evt.GetValueOfCurveAtTime(sampleTime);
            transform.position = startPosition * (1 - curveValue) + curveValue * endPosition; 
        }
    }
}
