using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGenerator : MonoBehaviour
{
    public List<Transform> leftGroup;
    public List<Transform> rightGroup;
    // TODO: create base fruit class
    public List<GameObject> pieceAList;
    public List<GameObject> pieceBList;

    public Vector3 leftInjectDirection;
    public Vector3 rightInjectDirection;

    public float injectSpeed = 1.0f;

    public float generateInterval = 2.0f;


    private float generateTimer;
    private InputHandler mainInputHandler;

    // Start is called before the first frame update
    void Start()
    {
        generateTimer = 0.0f;
        GameObject[] tempList = GameObject.FindGameObjectsWithTag("Main");
        Debug.Log("main object found: ");
        Debug.Log(tempList.Length);
        mainInputHandler = tempList[0].GetComponent<InputHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        generateTimer += Time.deltaTime;
        while (generateTimer >= generateInterval)
        {
            generateTimer -= generateInterval;
            generateAndInject();
        }
    }

    void generateAndInject()
    {
        Transform leftPos = leftGroup[Random.Range(0, leftGroup.Count)];
        Transform rightPos = rightGroup[Random.Range(0, rightGroup.Count)];
        GameObject pieceAPrefab = pieceAList[Random.Range(0, pieceAList.Count)];
        GameObject pieceBPrefab = pieceBList[Random.Range(0, pieceBList.Count)];
        GameObject pieceA = GameObject.Instantiate(pieceAPrefab);
        GameObject pieceB = GameObject.Instantiate(pieceBPrefab);
        pieceA.transform.position = leftPos.position;
        pieceB.transform.position = rightPos.position;
        pieceA.GetComponent<SimpleGravity>().velocity = leftInjectDirection * injectSpeed;
        pieceB.GetComponent<SimpleGravity>().velocity = rightInjectDirection * injectSpeed;

        mainInputHandler.ActiveFruitList.Add(pieceA);
        mainInputHandler.ActiveFruitList.Add(pieceB);
    }
}
