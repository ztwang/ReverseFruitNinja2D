using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGenerator : MonoBehaviour
{
    public List<GameObject> ActiveFruitList;
    
    public List<GameObject> leftSpawnGroup;
    public List<GameObject> rightSpawnGroup;

    public List<GameObject> pieceAList;
    public List<GameObject> pieceBList;
    public List<GameObject> fullFruitList;

    public float generateInterval = 2.0f;

    private bool isStarted;
    private float generateTimer;
    private int idCounter;

    // Start is called before the first frame update
    void Start()
    {
        generateTimer = 0.0f;
        idCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStarted) return;

        generateTimer += Time.deltaTime;
        while (generateTimer >= generateInterval)
        {
            generateTimer -= generateInterval;
            generateAndLaunch();
        }
    }

    public void StartGenerate()
    {
        isStarted = true;
        generateAndLaunch();
    }

    void generateAndLaunch()
    {
        int fruitIndex = Random.Range(0, pieceAList.Count);
        int spawnIndex = Random.Range(0, leftSpawnGroup.Count);

        GameObject pieceA = GameObject.Instantiate(pieceAList[fruitIndex]);
        pieceA.transform.position = leftSpawnGroup[spawnIndex].transform.position;
        SpawnLocation leftSpawn = leftSpawnGroup[spawnIndex].GetComponent<SpawnLocation>();
        pieceA.GetComponent<SimpleGravity>().velocity =
            generateRandomVector2(leftSpawn.degreeMin, leftSpawn.degreeMax) * leftSpawn.launchSpeed;
        ActiveFruitList.Add(pieceA);
        SetId(pieceA);

        GameObject pieceB = GameObject.Instantiate(pieceBList[fruitIndex]);
        pieceB.transform.position = rightSpawnGroup[spawnIndex].transform.position;
        SpawnLocation rightSpawn = rightSpawnGroup[spawnIndex].GetComponent<SpawnLocation>();
        pieceB.GetComponent<SimpleGravity>().velocity =
            generateRandomVector2(rightSpawn.degreeMin, rightSpawn.degreeMax) * rightSpawn.launchSpeed;
        ActiveFruitList.Add(pieceB);
        SetId(pieceB);
    }

    // Generate a random vector between two angles. Normalized. 
    Vector2 generateRandomVector2(int minDegree, int maxDegree)
    {
        int randomDegree = Random.Range(minDegree, maxDegree);
        float radius = Mathf.PI * (float)randomDegree / 180.0f;
        return new Vector2(Mathf.Cos(radius), Mathf.Sin(radius));
    }

    void SetId(GameObject obj)
    {
        obj.name += "[" + idCounter.ToString() + "]";
        obj.GetComponent<FruitBase>().id = idCounter;
        idCounter++;
    }
}
