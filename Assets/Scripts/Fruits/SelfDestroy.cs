using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public float yThreshold = 8f;
    public float xThreshold = 16f;
    
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
        if (transform.position.y < -yThreshold || transform.position.y > yThreshold
            || transform.position.x < -xThreshold || transform.position.x > xThreshold)
        {
            List<GameObject> fruitList = godObject.GetComponent<PieceGenerator>().ActiveFruitList;
            if (fruitList.Contains(gameObject))
            {
                fruitList.Remove(gameObject);
            }
            Destroy(gameObject);

            // Reset combo
            if (GetComponent<FruitBase>().fruitPiece != FruitBase.FruitPiece.Whole)
            {
                ComboManager comboManager = godObject.GetComponent<ComboManager>();
                if (comboManager)
                {
                    comboManager.Reset();
                }
            }
        }
    }
}
