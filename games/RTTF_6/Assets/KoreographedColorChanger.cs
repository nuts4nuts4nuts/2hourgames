using SonicBloom.Koreo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoreographedColorChanger : MonoBehaviour
{
    [EventID]
    public string eventID;
    public Color targetColor;

    private Color startColor;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        Koreographer.Instance.RegisterForEventsWithTime(eventID, SetColor);
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
    }

    void SetColor(KoreographyEvent evt, int sampleTime, int sampleDelta, DeltaSlice deltaSlice)
    {
        if(evt.HasCurvePayload())
        {
            float curveValue = evt.GetValueOfCurveAtTime(sampleTime);
            spriteRenderer.color = startColor * (1 - curveValue) + curveValue * targetColor; 
        }
    }
}
