using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float yThreshold = -8f;
    
    private GameObject godObject;

    // Start is called before the first frame update
    void Start()
    {
        godObject = GameObject.Find("GOD");
        if (godObject == null)
        {
            Debug.LogError("Can't find game object: GOD");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= yThreshold)
        {
            Destroy(gameObject);
            List<GameObject> fruitList = godObject.GetComponent<PieceGenerator>().ActiveFruitList;
            if (fruitList.Contains(gameObject))
            {
                fruitList.Remove(gameObject);
            }
        }
    }
}
