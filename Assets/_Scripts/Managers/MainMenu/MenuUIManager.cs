using DG.Tweening;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private List<RectTransform> uiWindows = new List<RectTransform>();
    [SerializeField] private SelectCharUIManager _selectCharUiManager;
    [SerializeField] private float touchSize;

    //inside class
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    private int currentWnd = 2;
    private bool canSwipe = true;


    private void Start()
    {
        canSwipe = true;
        uiWindows[0].DOAnchorPosX((-uiWindows[0].rect.width), 0f);
        uiWindows[1].DOAnchorPosX(-uiWindows[1].rect.width, 0f);
        uiWindows[2].DOAnchorPosX(0, 0f);
        uiWindows[3].DOAnchorPosX(uiWindows[3].rect.width, 0f);
        uiWindows[4].DOAnchorPosX(uiWindows[4].rect.width, 0f);
    }

    public void ShowWdn(int showWdn)
    {
        if (showWdn == currentWnd) return;
        if (!canSwipe) return;
        if (showWdn == 1)
        {
            uiWindows[1].gameObject.SetActive(false);
            uiWindows[1].gameObject.SetActive(true);
        }

        if (showWdn < currentWnd)
        {
            canSwipe = false;
            uiWindows[showWdn].DOAnchorPosX(-uiWindows[showWdn].rect.width, 0f);

            uiWindows[showWdn].DOAnchorPosX(0, 0.2f).SetDelay(0).OnComplete((() => canSwipe = true));

            uiWindows[currentWnd].DOAnchorPosX(uiWindows[currentWnd].rect.width, 0.2f).SetDelay(0)
                .OnComplete((() => canSwipe = true));
            currentWnd = showWdn;
        }
        else
        {
            uiWindows[showWdn].DOAnchorPosX(uiWindows[showWdn].rect.width, 0f);

            uiWindows[showWdn].DOAnchorPosX(0, 0.2f).SetDelay(0).OnComplete((() => canSwipe = true));

            uiWindows[currentWnd].DOAnchorPosX(-uiWindows[currentWnd].rect.width, 0.2f).SetDelay(0)
                .OnComplete((() => canSwipe = true));
            currentWnd = showWdn;
        }
    }

    private void SwipeWdn(int count)
    {
        if (!canSwipe) return;
        if (count == 1)
        {
            if (currentWnd == 4) return;
            canSwipe = false;

            if (currentWnd + 1 == 1)
            {
                uiWindows[1].gameObject.SetActive(false);
                uiWindows[1].gameObject.SetActive(true);
            }

            uiWindows[currentWnd + 1].DOAnchorPosX(uiWindows[currentWnd].rect.width, 0);
            uiWindows[currentWnd].DOAnchorPosX(-uiWindows[currentWnd].rect.width, 0.2f).SetDelay(0)
                .OnComplete((() => canSwipe = true));
            uiWindows[currentWnd + 1].DOAnchorPosX(0, 0.2f).SetDelay(0).OnComplete((() => canSwipe = true));
            currentWnd += 1;
        }
        else
        {
            if (currentWnd == 0) return;
            canSwipe = false;

            if (currentWnd -1 == 1)
            {
                uiWindows[1].gameObject.SetActive(false);
                uiWindows[1].gameObject.SetActive(true);
            }

            uiWindows[currentWnd - 1].DOAnchorPosX(-uiWindows[currentWnd].rect.width, 0);
            uiWindows[currentWnd].DOAnchorPosX(uiWindows[currentWnd].rect.width, 0.2f).SetDelay(0)
                .OnComplete((() => canSwipe = true));
            uiWindows[currentWnd - 1].DOAnchorPosX(0, 0.2f).SetDelay(0).OnComplete((() => canSwipe = true));
            currentWnd -= 1;
        }
    }

    private void Update()
    {
        Swipe();
    }

    private void Swipe()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                _selectCharUiManager.SetPressedChar(null);
            }

            // Debug.Log(EventSystem.current.currentSelectedGameObject.transform.name);
            //hide UI elements
        }

        if (Input.GetMouseButtonDown(0))
        {
            //save began touch 2d point
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            //save ended touch 2d point
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            //create vector from the two points
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);


            //normalize the 2d vector
            // currentSwipe.Normalize();
            

            //swipe left
            if (currentSwipe.x < -touchSize)
            {
                // NextWdn();
                SwipeWdn(1);
            }

            //swipe right
            if (currentSwipe.x > touchSize)
            {
                // PrevWdn();
                SwipeWdn(2);
            }
        }
    }
}