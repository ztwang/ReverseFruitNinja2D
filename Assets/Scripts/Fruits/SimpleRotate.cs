using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    private Vector2 speedRange = new Vector2(-100f, 100f);

    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(speedRange.x, speedRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}
