using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour
{
    public int comboCounter;

    private void SetComboCounter(int value)
    {
        comboCounter = value;
        comboLevel = GetLevel(value);
        RefreshUI();
    }

    public Dictionary<int, ComboLevel> threshold = new Dictionary<int, ComboLevel>()
    {
        { 0, ComboLevel.NONE},
        { 5, ComboLevel.GOOD},
        { 10, ComboLevel.EXCELLENT},
        { 20, ComboLevel.LEGENDARY},
    };

    ComboLevel GetLevel(int combo)
    {
        ComboLevel level = ComboLevel.NONE;
        foreach (KeyValuePair<int, ComboLevel> pair in threshold)
        {
            level = pair.Value;
            if (combo <= pair.Key)
            {
                break;
            }
        }
        return level;
    }

    public Text comboText;

    public enum ComboLevel
    {
        NONE = 0,
        GOOD,
        EXCELLENT,
        LEGENDARY
    }

    public ComboLevel comboLevel;

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Increment()
    {
        SetComboCounter(comboCounter+1);
    }

    void RefreshUI()
    {
        if (comboText)
        {
            string text = GetComboText();
            comboText.text = text;
            UIHeartBeatAnimation anim = comboText.GetComponent<UIHeartBeatAnimation>();
            if (anim)
            {
                anim.Animate();
            }
        }
    }

    string GetComboText()
    {
        if (comboCounter <= 0)
        {
            return "";
        }
        return string.Format("x{0}\n{1}", comboCounter, comboLevel.ToString());
    }

    public void Reset()
    {
        SetComboCounter(0);
    }
}
