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

    private float generateTimer;

    // Start is called before the first frame update
    void Start()
    {
        generateTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        generateTimer += Time.deltaTime;
        while (generateTimer >= generateInterval)
        {
            generateTimer -= generateInterval;
            generateAndLaunch();
        }
    }

    void generateAndLaunch()
    {
        int fruitIndex = Random.Range(0, fullFruitList.Count);
        int spawnIndex = Random.Range(0, leftSpawnGroup.Count);

        GameObject pieceA = GameObject.Instantiate(pieceAList[fruitIndex]);
        pieceA.transform.position = leftSpawnGroup[spawnIndex].transform.position;
        SpawnLocation leftSpawn = leftSpawnGroup[spawnIndex].GetComponent<SpawnLocation>();
        pieceA.GetComponent<SimpleGravity>().velocity =
            generateRandomVector2(leftSpawn.degreeMin, leftSpawn.degreeMax) * leftSpawn.launchSpeed;
        ActiveFruitList.Add(pieceA);

        GameObject pieceB = GameObject.Instantiate(pieceBList[fruitIndex]);
        pieceB.transform.position = rightSpawnGroup[spawnIndex].transform.position;
        SpawnLocation rightSpawn = rightSpawnGroup[spawnIndex].GetComponent<SpawnLocation>();
        pieceB.GetComponent<SimpleGravity>().velocity =
            generateRandomVector2(rightSpawn.degreeMin, rightSpawn.degreeMax) * rightSpawn.launchSpeed;
        ActiveFruitList.Add(pieceB);
    }

    // Generate a random vector between two angles. Normalized. 
    Vector2 generateRandomVector2(int minDegree, int maxDegree)
    {
        int randomDegree = Random.Range(minDegree, maxDegree);
        float radius = Mathf.PI * (float)randomDegree / 180.0f;
        return new Vector2(Mathf.Cos(radius), Mathf.Sin(radius));
    }
}
