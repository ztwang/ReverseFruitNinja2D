using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // TODO: use base fruit class instead
    public List<GameObject> ActiveFruitList;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        List<Touch> Touches = TouchHelper.GetTouches();
        if (Touches.Count > 0)
        {
            Debug.Log("Detected a touch!");
            Touch touch = Touches[0];
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            foreach (GameObject fruitPiece in ActiveFruitList)
            {
                if (fruitPiece.GetComponent<BoxCollider2D>().OverlapPoint(touchPosition))
                {
                    // fruit is grabbed by the touch
                    fruitPiece.GetComponent<SimpleGravity>().velocity = Vector3.zero;
                }
            }
        }
    }
}
