using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SlideMenuUI : MonoBehaviour
{
    public static bool Block { get; set; }
    
    [SerializeField] private Transform toMove;
    [SerializeField] private float maxMoveOnInput = 490f;
    
    [Space]
    [SerializeField] private float maxDiff;
    [Range(0f, 1f)]
    [SerializeField] private float enoughDiffPercent = 0.5f, minDiffPercent = 0.1f;

    [Space] 
    [SerializeField] private float spaceForEachSlide = 1080f;
    [SerializeField] private int limitOnRight = 1;
    [SerializeField] private int limitOnLeft = 1;

    [Space] 
    [SerializeField] private Transform sliderToMove;
    [SerializeField] private float spaceSliderForEachSlide = 360f;

    private Vector3 startPosition, currentPosition;
    private float currentDiff, onStartToMoveX;

    private float diffPercent => currentDiff / maxDiff;
    private float currentMoveX => maxMoveOnInput * diffPercent - onStartToMoveX;
    private float currentMoveXForSlide => currentMoveX * (spaceSliderForEachSlide / spaceForEachSlide);
    
    private float targetMoveX;
    private float targetSliderMoveX;

    private int index = 0;
    
    void Update()
    {
        // Debug.Log("Pointer is over - " + PointerOverUI.IsOver);
        if (Input.GetMouseButtonDown(0) && !PointerOverUI.IsOver 
            && LobbyManagerUI.OnMain && !Block)
        {
            startPosition = Input.mousePosition;
            
            toMove.DOKill();
            toMove.localPosition = new Vector3(targetMoveX, toMove.localPosition.y, toMove.localPosition.z);
            sliderToMove.localPosition = new Vector3(targetSliderMoveX, sliderToMove.localPosition.y, sliderToMove.localPosition.z);
            
            onStartToMoveX = toMove.localPosition.x;
        }

        if (Input.GetMouseButton(0) && startPosition != Vector3.zero 
            && LobbyManagerUI.OnMain && !Block)
        {
            currentPosition = Input.mousePosition;
            currentDiff = Mathf.Clamp(startPosition.x - currentPosition.x, -maxDiff, maxDiff);

            if (Mathf.Abs(diffPercent) >= minDiffPercent)
            {
                toMove.localPosition = Vector3.Lerp(
                    toMove.localPosition, 
                    new Vector3(-currentMoveX, toMove.localPosition.y, toMove.localPosition.z), 
                    7f * Time.deltaTime);
            
                sliderToMove.localPosition = Vector3.Lerp(
                    sliderToMove.localPosition, 
                    new Vector3(currentMoveXForSlide, sliderToMove.localPosition.y, sliderToMove.localPosition.z), 
                    7f * Time.deltaTime);
            }
        }

        if (Input.GetMouseButtonUp(0) && diffPercent != 0 
            && LobbyManagerUI.OnMain && !Block)
        {
            if (Mathf.Abs(diffPercent) >= enoughDiffPercent)
            {
                int diff = index;
                
                if (Vector3.Dot(toMove.right, (startPosition - currentPosition).normalized) > 0)
                {
                    index = Mathf.Clamp(index + 1, -limitOnLeft, limitOnRight);
                }
                else
                {
                    index = Mathf.Clamp(index - 1, -limitOnLeft, limitOnRight);
                }
                
                diff -= index;
                targetMoveX = onStartToMoveX + spaceForEachSlide * diff;
                targetSliderMoveX = onStartToMoveX + spaceSliderForEachSlide * diff;
            }
            else
            {
                targetMoveX = onStartToMoveX;
            }
            
            SetPosToMove(targetMoveX);

            currentDiff = 0f;
            startPosition = Vector3.zero;
            currentPosition = Vector3.zero;
        }
    }

    public void SetPosToMove(float posX)
    {
        // toMove.DOKill();
        
        index = -(int)(posX / spaceForEachSlide);
        
        targetMoveX = posX;
        targetSliderMoveX = -posX * (spaceSliderForEachSlide / spaceForEachSlide);
        
        toMove.DOLocalMove(new Vector3(targetMoveX, toMove.localPosition.y, toMove.localPosition.z), 0.4f);
        sliderToMove.DOLocalMove(new Vector3(targetSliderMoveX, sliderToMove.localPosition.y, sliderToMove.localPosition.z), 0.4f);

        // toMove.localPosition = Vector3.Lerp(toMove.localPosition, new Vector3(posX, 0, 0), 5f * Time.deltaTime);
        // toMove.localPosition = new Vector3(posX, 0f, 0f);
    }
}
