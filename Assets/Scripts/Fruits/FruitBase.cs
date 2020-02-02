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

    PieceGenerator pieceGenerator;

    // Start is called before the first frame update
    void Start()
    {
        pieceGenerator = GameObject.Find("GOD").GetComponent<PieceGenerator>();
        if (pieceGenerator == null)
        {
            Debug.LogError("Can't find game object: GOD pieceGenerator");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbed)
        {
            List<GameObject> fruitList = pieceGenerator.ActiveFruitList;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<FruitBase>() != null)
        {
            //Debug.Log("Colliding with another fruit:" + collision.gameObject.name);
            if (isGrabbed && type == collision.gameObject.GetComponent<FruitBase>().type)
            {
                Debug.Log("Repair fruit!");
                GameObject apple = GameObject.Instantiate(pieceGenerator.fullFruitList[0]);
                apple.transform.position =
                        (gameObject.transform.position + collision.gameObject.transform.position) / 2;
                Release();

                pieceGenerator.ActiveFruitList.Remove(gameObject);
                pieceGenerator.ActiveFruitList.Remove(collision.gameObject);
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
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
