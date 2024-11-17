using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro; 
public class TextCollector3 : MonoBehaviour
{
    public TMP_Text question;
    public TMP_Text optionA;
    public TMP_Text optionB;
    public TMP_Text optionC;
    public TMP_Text optionD;
    private int currentIndex = 0;
    public Button changeTextButton;

    public List<GameObject> textObjects; 

    public List<string> textList = new List<string>();

       void Start()
        {
            
            textList.Add("Unity");
            textList.Add("Unity1");
            textList.Add("Unity2");
        if (textList.Count > 0)
            {
                question.text = textList[currentIndex];
            }

            changeTextButton.onClick.AddListener(SetText);
    }

        public void SetText()
        {
       
        currentIndex = (currentIndex + 1) % textList.Count;

        question.text = textList[currentIndex];

        foreach (string str in textList)
        {
            Debug.Log(str);
        }

    }
}













    
     


 

