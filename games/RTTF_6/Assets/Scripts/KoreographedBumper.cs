using SonicBloom.Koreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoreographedScale : MonoBehaviour
{
    [EventID]
    public string eventID;
    public Transform moveTo;
    public Vector3 targetScale;

    private Vector3 startScale;

    // Start is called before the first frame update
    void Start()
    {
        Koreographer.Instance.RegisterForEventsWithTime(eventID, SetScale);
        startScale = transform.localScale;
    }

    void SetScale(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
    {
        if(evt.HasCurvePayload())
        {
            float curveValue = evt.GetValueOfCurveAtTime(sampleTime);
            transform.localScale = startScale * (1 - curveValue) + curveValue * targetScale; 
        }
    }
}
