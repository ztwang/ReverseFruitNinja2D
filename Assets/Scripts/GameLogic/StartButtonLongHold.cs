using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButtonLongHold : MonoBehaviour
{
    public float longHoldTime = 3f;
    private float holdTimer;
    private bool holding;

    public GameFlowController gfController;
    // Start is called before the first frame update
    void Start()
    {
        holdTimer = 0f;
        holding = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (holding)
        {
            if (holdTimer < longHoldTime)
            {
                holdTimer += Time.deltaTime;
            }
            else
            {
                holding = false;

            }
        }
        else if (holdTimer >= longHoldTime)
        {
            holdTimer = 0f;
            LongHold();
        } else if (holdTimer > 0f)
        {
            OtherHold();
        }
    }

    public void StartHold()
    {
        if (!holding) {
            holding = true;
        }
    }

    public void EndHold()
    {
        if (holding)
        {
            holding = false;
        }
    }

    public void LongHold()
    {
        if (gfController)
        {
            gfController.GoToScene(3);
        }
    }

    public void OtherHold()
    {
        if (gfController)
        {
            gfController.GoToScene(1);
        }
    }
}
