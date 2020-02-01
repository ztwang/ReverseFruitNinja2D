using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float yThreshold = -8f;
    
    private InputHandler mainInputHandler;

    // Start is called before the first frame update
    void Start()
    {
        mainInputHandler = GameObject.FindGameObjectsWithTag("Main")[0].GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= yThreshold)
        {
            Destroy(gameObject);
            if (mainInputHandler.ActiveFruitList.Contains(gameObject))
            {
                mainInputHandler.ActiveFruitList.Remove(gameObject);
                Debug.Log("Fruit object destroyed and removed.");
            }
        }
    }
}
