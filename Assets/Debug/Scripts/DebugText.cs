using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
    
    private static DebugText Instance;
    public static void ShowText(string text)
    {
        Instance.AddText(text);
    }

    private TextMeshProUGUI _textMesh;

    private List<string> _texts;

    void Start()
    {
        Instance = this;

        _textMesh = GetComponent<TextMeshProUGUI>();
        _textMesh.text = "";
        _texts = new List<string>();
    }
    
    private void AddText(string text)
    {
        _texts.Add(text);
        UpdateText();

        StartCoroutine(RemoveTextCoroutine(text));
    }

    private IEnumerator RemoveTextCoroutine(string text)
    {
        yield return new WaitForSeconds(5f);
        _texts.Remove(text);
        UpdateText();
    }

    private void UpdateText()
    {
        string text = "";

        foreach (var item in _texts)
        {
            text += item + "\n";
        }

        _textMesh.text = text;
    }

}
