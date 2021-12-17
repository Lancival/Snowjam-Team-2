using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour
{
    [SerializeField] private Vector3 point1;
    [SerializeField] private Vector3 point2;
    [SerializeField] private int interpolationFramesCount = 180; // Number of frames to completely interpolate between the 2 positions
    private int elapsedFrames = 0;
    private bool forward = true;
    
    void FixedUpdate()
    {
        if (forward) {
            this.transform.position  = Vector3.Lerp(point1, point2, (float)elapsedFrames / interpolationFramesCount);
            elapsedFrames++;
            if(elapsedFrames == interpolationFramesCount) {
                elapsedFrames = 0;
                 forward = false;
            }
        }else{
            this.transform.position  = Vector3.Lerp(point2, point1, (float)elapsedFrames / interpolationFramesCount);
            elapsedFrames++;
            if(elapsedFrames == interpolationFramesCount) {
                elapsedFrames = 0;
                forward = true;
            }
        }
    }
}
