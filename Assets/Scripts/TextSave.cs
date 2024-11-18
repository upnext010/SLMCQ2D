using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class TextSave : MonoBehaviour

{
    private List<string> textList = new List<string>();
    //public TextMeshProUGUI[] textObjects; 
    public TMP_InputField[] textObjects;
    //void Start()
    public void GetText()
    {
        foreach (var textObject in textObjects)
        {
            string text = textObject.text;

            textList.Add(text);
        }
        foreach (string collectedText in textList)
        {
            Debug.Log(collectedText);
        }
    }
}
