using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBase : MonoBehaviour
{
    public enum FruitType
    {
        Apple = 0,
        Orange,
        Banana,
        Strawberry,
        Pen,
        PenApple,
        ApplePen,
        Watermelon,
    }

    public enum FruitPiece
    {
        Whole = 0,
        Left = 1,
        Right = 2,
    }

    public FruitType fruitType;
    public FruitPiece fruitPiece;
    public int id;

    bool isGrabbed;
    int grabbedFingerId = -1;

    public Vector3 grabbedPosOffset;

    GameObject godObject;

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
        if (isGrabbed)
        {
            // TODO: don't use collision, check distance directly?
            //List<GameObject> fruitList = pieceGenerator.ActiveFruitList;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*if (collision.gameObject.GetComponent<FruitBase>() != null)
        {
            //Debug.Log("Colliding with another fruit:" + collision.gameObject.name);
            Judge judge = godObject.GetComponent<Judge>();
            if (judge.CanRepairFruit(gameObject, collision.gameObject))
            {
                Debug.Log("Repairing fruit!");
                Release();
                judge.RepairFruit(gameObject, collision.gameObject);
            }
        }*/
    }

    public void GrabBy(int fingerId, Vector2 touchPosition)
    {
        if (isGrabbed)
        {
            Debug.LogWarning("Already grabbed by: " + grabbedFingerId);
            return;
        }

        isGrabbed = true;
        grabbedFingerId = fingerId;
        Debug.Log("Object grabbed by finger: " + fingerId);

        DistanceFusionCheckerAdd();
        grabbedPosOffset = transform.position - new Vector3(touchPosition.x, touchPosition.y, 0f);
        GetComponent<SimpleRotate>().enabled = false;

        // DEBUG
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void Release()
    {
        if (!isGrabbed)
        {
            Debug.LogWarning("Not grabbed. can't be released.");
            return;
        }
        isGrabbed = false;
        DistanceFusionCheckerRemove();
        GetComponent<SimpleRotate>().enabled = true;

        // DEBUG
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public bool IsGrabbedBy(int fingerId)
    {
        return isGrabbed && grabbedFingerId == fingerId;
    }
    public bool IsGrabbed()
    {
        return isGrabbed;
    }

    private void DistanceFusionCheckerAdd()
    {
        DistanceFusionChecker checker = godObject.GetComponent<DistanceFusionChecker>();
        if (checker)
        {
            checker.AddGrabbedPiece(this);
        }
    }

    private void DistanceFusionCheckerRemove()
    {
        DistanceFusionChecker checker = godObject.GetComponent<DistanceFusionChecker>();
        if (checker)
        {
            checker.RemoveGrabbedPiece(this);
        }
    }
}
