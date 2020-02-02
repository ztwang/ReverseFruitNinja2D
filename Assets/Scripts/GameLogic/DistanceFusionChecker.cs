using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceFusionChecker : MonoBehaviour
{
    public List<FruitBase> grabbedPieceList;

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
                    var p1 = grabbedPieceList[i].gameObject;
                    var p2 = grabbedPieceList[j].gameObject;
                    if (judge.CanRepairFruit(p1, p2))
                    {
                        grabbedPieceList.RemoveAt(i);
                        grabbedPieceList.RemoveAt(j);
                        judge.RepairFruit(p1, p2);
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
}
