using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBase : MonoBehaviour
{
    public enum FruitType
    {
        Apple = 0,
        Orange = 1,
        Watermelon =2,
    }

    public FruitType type;
    public int id;

    bool isGrabbed;
    int grabbedFingerId = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabBy(int fingerId)
    {
        if (isGrabbed)
        {
            Debug.LogWarning("Already grabbed by: " + grabbedFingerId);
            return;
        }

        isGrabbed = true;
        grabbedFingerId = fingerId;
        Debug.Log("Object grabbed by finger: " + fingerId);
    }

    public void Release()
    {
        if (!isGrabbed)
        {
            Debug.LogWarning("Not grabbed. can't be released.");
            return;
        }
        isGrabbed = false;
    }

    public bool IsGrabbedBy(int fingerId)
    {
        return isGrabbed && grabbedFingerId == fingerId;
    }
}
