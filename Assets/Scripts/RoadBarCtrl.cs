using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RoadBarCtrl : MonoBehaviour {

    public Image roadBar;

    public UIECtrl _UIECtrl;
    public Button[] markers;
    public float[] markerValues;

    private int i;
    private Vector3 markerPos;

    // Use this for initialization
    void Start () {
        roadBar.fillAmount = 0.01f;

        i = 0;
        foreach (Button marker in markers)
        {
            markerPos = new Vector3(14.4f * markerValues[i] * 100.0f, 20.0f, 0.0f);
            marker.GetComponent<RectTransform>().position = markerPos;
            marker.onClick.AddListener(LoadContent);
            i++;
        }
	}
	
    public void LoadContent()
    {
        roadBar.fillAmount = (EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position.x) / 1440.0f;
        _UIECtrl.ShowTargetUIE(0);
    }
}
