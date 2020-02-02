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
        CheckFusion();
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

    float Dist(FruitBase p1, FruitBase p2)
    {
        return Vector2.Distance(p1.transform.position, p2.transform.position);
    }

    void CheckFusion()
    {
        if (!judge)
        {
            return;
        }
        bool[] fused = new bool[grabbedPieceList.Count];
        for (int i = 0; i < grabbedPieceList.Count; i++)
        {
            if (fused[i])
            {
                continue;
            }

            var p1 = grabbedPieceList[i];
            int closest = -1;
            float minDist = fusionDistance;
            // find closest fusable piece
            for (int j = i + 1; j < grabbedPieceList.Count; j++)
            {
                if (fused[j])
                {
                    continue;
                }
                var p2 = grabbedPieceList[j];
                if ((judge.CanRepairFruit(p1.gameObject, p2.gameObject)))
                {
                    float dist = Dist(p1, p2);
                    if (closest == -1 || dist < minDist)
                    {
                        closest = j;
                        minDist = dist;
                    }
                }
            }
            if (closest != -1)
            {
                fused[i] = true;
                fused[closest] = true;
                judge.RepairFruit(grabbedPieceList[i].gameObject, grabbedPieceList[closest].gameObject);
            }
        }
        for (int i = fused.Length - 1; i >= 0; i++)
        {
            grabbedPieceList.RemoveAt(i);
        }
    }
}
