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

    private float touchTime = 0.0f;
    private float nextTouch = 1.2f;

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

    private void Update()
    {
        touchTime += Time.deltaTime;
    }

    public void LoadContent()
    {
        if (touchTime < nextTouch) return;

        StartCoroutine(FillRoadBar((EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>().position.x) / 1440.0f));
        _UIECtrl.ShowTargetUIE(0);
        touchTime = 0.0f;
    }

    IEnumerator FillRoadBar(float amount)
    {
        float offset = (amount - roadBar.fillAmount) / 30.0f;

        for (i = 0; i < 30; i++)
        {
            roadBar.fillAmount += offset;
            yield return null;
        }
    }
}
