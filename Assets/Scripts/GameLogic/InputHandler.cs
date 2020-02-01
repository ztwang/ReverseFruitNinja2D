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
            foreach (Touch touch in Touches)
            {
                Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        foreach (GameObject fruitPiece in ActiveFruitList)
                        {
                            if (fruitPiece.GetComponent<BoxCollider2D>().OverlapPoint(touchPosition))
                            {
                                // fruit is grabbed by the touch
                                fruitPiece.GetComponent<SimpleGravity>().velocity = Vector3.zero;
                                fruitPiece.GetComponent<SimpleGravity>().gravity = 0.0f;
                                fruitPiece.GetComponent<FruitBase>().GrabBy(touch.fingerId);
                            }
                        }
                        break;

                    case TouchPhase.Moved:
                        foreach (GameObject fruitPiece in ActiveFruitList)
                        {
                            if (fruitPiece.GetComponent<FruitBase>().IsGrabbedBy(touch.fingerId))
                            {
                                fruitPiece.transform.position =
                                        new Vector3(touchPosition.x, touchPosition.y, 0);
                            }
                        }
                        break;

                    case TouchPhase.Ended:
                        foreach (GameObject fruitPiece in ActiveFruitList)
                        {
                            if (fruitPiece.GetComponent<FruitBase>().IsGrabbedBy(touch.fingerId))
                            {
                                fruitPiece.GetComponent<FruitBase>().Release();
                                fruitPiece.GetComponent<SimpleGravity>().gravity = SimpleGravity.GRAVITY;                              
                            }
                        }
                        break;
                }
            }
        }
    }
}
