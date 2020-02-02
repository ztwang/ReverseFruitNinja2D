using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitDebug : MonoBehaviour
{
    private GameObject debugCanvas;

    public GameObject debugLabelPrefab;
    private Text debugText;
    // Start is called before the first frame update
    void Start()
    {
        debugCanvas = GameObject.Find("Canvas");
        var go = Instantiate(debugLabelPrefab);
        debugText = go.GetComponent<Text>();
        go.transform.SetParent(debugCanvas.transform);
        go.name += name;
        debugText.text = name;
        go.transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        SetPosition();
    }

    void SetPosition()
    {
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(gameObject.transform.position);
        Vector2 screenPosition = new Vector2(
        ((viewportPosition.x * debugCanvas.GetComponent<RectTransform>().sizeDelta.x) - (debugCanvas.GetComponent<RectTransform>().sizeDelta.x * 0.5f)),
        ((viewportPosition.y * debugCanvas.GetComponent<RectTransform>().sizeDelta.y) - (debugCanvas.GetComponent<RectTransform>().sizeDelta.y * 0.5f)));
        debugText.GetComponent<RectTransform>().anchoredPosition = screenPosition;
    }

    private void OnDestroy()
    {
        Destroy(debugText.gameObject);
    }
}
