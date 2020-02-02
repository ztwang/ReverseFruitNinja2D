using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHeartBeatAnimation : MonoBehaviour
{
    //Animation
    public enum AnimState
    {
        IDLE = 0,
        GROW,
        FADE
    }
    public AnimState currentState;
    public float startScale = 1f;
    public float peakScale = 1.2f;
    public float endScale = 1f;
    public float growSpeed = 10f;
    public float fadeLerp = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        currentState = AnimState.IDLE;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case AnimState.GROW:
                if (transform.localScale.x >= peakScale)
                {
                    currentState = AnimState.FADE;
                }
                else
                {
                    transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
                }
                break;
            case AnimState.FADE:
                if (transform.localScale.x <= endScale + 0.01f)
                {
                    transform.localScale = Vector3.one * endScale;
                    currentState = AnimState.IDLE;
                }
                else
                {
                    transform.localScale = Vector3.one * Mathf.Lerp(transform.localScale.x, endScale, fadeLerp);
                }
                break;
            default:
                break;
        }
    }


    public void Animate()
    {
        currentState = AnimState.GROW;
    }
}
