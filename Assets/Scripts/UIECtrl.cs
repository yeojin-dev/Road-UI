using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DoozyUI;

public class UIECtrl : MonoBehaviour {

    public UIElement[] uiElements;

    public Image roadBar;

    private int index = 0;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void ShowNextUIE()
    {
        uiElements[index].Hide(false);  // true: 즉시, false: 애니메이션 처리
        index = (index + 1) > uiElements.Length - 1 ? 0 : index + 1;
        uiElements[index].Show(false);
    }

    public void ShowPrevUIE()
    {
        uiElements[index].Hide(false);
        index = (index - 1) < 0 ? uiElements.Length - 1 : index - 1;
        uiElements[index].Show(false);
    }
}
