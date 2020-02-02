using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceFusionChecker : MonoBehaviour
{
    public List<FruitBase> grabbedPieceList;

    public float fusionDistance = 2f;

    private Judge judge;
    // Start is called before the first frame update
    void Start()
    {
        grabbedPieceList = new List<FruitBase>();
        judge = GameObject.Find("GOD").GetComponent<Judge>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < grabbedPieceList.Count; i++)
        {
            for (int j = i+1; j < grabbedPieceList.Count; j++)
            {
                if (judge)
                {
                    var p1 = grabbedPieceList[i];
                    var p2 = grabbedPieceList[j];
                    if (WithinDistance(p1, p2) && (judge.CanRepairFruit(p1.gameObject, p2.gameObject)))
                    {
                        grabbedPieceList.Remove(p1);
                        grabbedPieceList.Remove(p2);
                        judge.RepairFruit(p1.gameObject, p2.gameObject);
                    }
                }
            }
        }
    }

    public void AddGrabbedPiece(FruitBase piece)
    {
        if (grabbedPieceList.Contains(piece))
        {
            Debug.LogErrorFormat("Grabbed same piece in {0}", this.name);
        } else
        {
            grabbedPieceList.Add(piece);
        }
    }

    public void RemoveGrabbedPiece(FruitBase piece)
    {
        if (grabbedPieceList.Contains(piece))
        {
            grabbedPieceList.Remove(piece);
        }
    }

    bool WithinDistance(FruitBase p1, FruitBase p2)
    {
        return Vector2.Distance(p1.transform.position, p2.transform.position) <= fusionDistance ;
    }
}
