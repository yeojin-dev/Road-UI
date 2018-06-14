using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using DoozyUI;

public class UIECtrl : MonoBehaviour {

    public UIElement[] uiContainers;  // Next, PrevContainer를 가리키는 함수 - [0], [1]은 Next / [2], [3]은 Prev
    public Transform[] uiContents;  // Container가 담는 하나의 Content 배열, [0]은 타이틀 화면

    public Image roadBar;  // 화면 최하단 Bar
    public Transform contentsContainer;  // Content들을 가지고 있는 게임오브젝트, 여기에 있으면 숨김 처리가 됨

    public int contentIndex = 0;  // 현재 화면에 보이는 Content의 인덱스
    private int[] containerIndex = { 0, 2 };  // containerIndex[0] is NextContainer index, [1] is Prev. 
    private int hideIndex;  // goToTemp() 메소드의 대상이 되는 Content의 인덱스

    private float touchTime = 0.0f;
    private float nextTouch = 1.2f;  // 스와이프 인식 딜레이 시간

    public RoadBarCtrl _RoadBarCtrl;

    public VideoPlayer vehicleVideo;
    public VideoPlayer subwayVideo;
    public VideoPlayer smitVideo;

    // Use this for initialization
    void Start () {
        foreach (UIElement uiContainer in uiContainers)
        {
            // 모든 Container는 OutAnim 종료 후 Content를 contentsContainer로 옮겨 숨김 처리
            // 이를 위한 UnityEvent 추가
            uiContainer.OnOutAnimationsFinish.AddListener(goToTemp);
        }
        // 첫 화면(타이틀)
        uiContents[contentIndex].SetParent(uiContainers[containerIndex[0]].transform, false);
	}
	
	// Update is called once per frame
	void Update () {
        touchTime += Time.deltaTime;
    }

    public void ShowNextUIE()
    {
        if (touchTime < nextTouch) return;

        hideIndex = contentIndex;
        uiContents[contentIndex].SetParent(uiContainers[containerIndex[0]].transform, false);  // 숨기려는 uiContent를 PrevContainer로 이동
        // true: 월드 좌표 반영, false: 미반영

        uiContainers[containerIndex[0]].Hide(false);  // true: 즉시, false: 애니메이션 처리

        containerIndex[0] = containerIndex[0] == 0 ? 1 : 0;  // NextContainer 0, 1 토글
        contentIndex = (contentIndex + 1) > uiContents.Length - 1 ? 0 : contentIndex + 1; // + 방향 wrap around

        VideoStart();
        StartCoroutine(_RoadBarCtrl.FillRoadBar(_RoadBarCtrl.markerValues[contentIndex]));
        uiContents[contentIndex].SetParent(uiContainers[containerIndex[0]].transform, false);
        uiContainers[containerIndex[0]].Show(false);

        touchTime = 0.0f;
    }

    public void ShowTargetUIE(int target)
    {
        if (touchTime < nextTouch) return;

        hideIndex = contentIndex;
        uiContents[contentIndex].SetParent(uiContainers[containerIndex[0]].transform, false);  // 숨기려는 uiContent를 PrevContainer로 이동
        // true: 월드 좌표 반영, false: 미반영

        uiContainers[containerIndex[0]].Hide(false);  // true: 즉시, false: 애니메이션 처리

        containerIndex[0] = containerIndex[0] == 0 ? 1 : 0;  // NextContainer 0, 1 토글
        contentIndex = target;

        VideoStart();
        StartCoroutine(_RoadBarCtrl.FillRoadBar(_RoadBarCtrl.markerValues[contentIndex]));
        uiContents[contentIndex].SetParent(uiContainers[containerIndex[0]].transform, false);
        uiContainers[containerIndex[0]].Show(false);

        touchTime = 0.0f;
    }

    public void ShowPrevUIE()
    {
        if (touchTime < nextTouch) return;

        hideIndex = contentIndex;
        uiContents[contentIndex].SetParent(uiContainers[containerIndex[1]].transform, false);  // 숨기려는 uiContent를 PrevContainer로 이동
        uiContainers[containerIndex[1]].Hide(false);  // true: 즉시, false: 애니메이션 처리

        containerIndex[1] = containerIndex[1] == 2 ? 3 : 2;  // PrevContainer 0, 1 토글
        contentIndex = (contentIndex - 1) < 0 ? uiContents.Length - 1 : contentIndex - 1; // - 방향 wrap around

        VideoStart();
        StartCoroutine(_RoadBarCtrl.FillRoadBar(_RoadBarCtrl.markerValues[contentIndex]));
        uiContents[contentIndex].SetParent(uiContainers[containerIndex[1]].transform, false);
        uiContainers[containerIndex[1]].Show(false);

        touchTime = 0.0f;
    }

    // uiContainer의 Hide() 함수 완료 후 uiContent 숨기기 위한 메소드
    public void goToTemp()
    {
        uiContents[hideIndex].SetParent(contentsContainer, false);
    }

    public void VideoStart()
    {
        switch (contentIndex)
        {
            case 2:
                Debug.Log(1);
                vehicleVideo.Play();
                break;

            case 4:
                Debug.Log(2);
                subwayVideo.Play();
                break;

            case 6:
                Debug.Log(3);
                smitVideo.Play();
                break;

            default:
                return;
        }
    }
    
}
