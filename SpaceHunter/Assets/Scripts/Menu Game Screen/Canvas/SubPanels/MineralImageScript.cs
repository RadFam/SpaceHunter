using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MineralImageScript : MonoBehaviour, IPointerClickHandler
{
    private Image myImage;
    private Sprite mySprite;
    private int myNumber;
    private bool isActive;

    public Sprite MySprite
    {
        get { return mySprite; }
        set { mySprite = value; }
    }

    public int MyNumber
    {
        get { return myNumber; }
        set { myNumber = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        myImage = GetComponent<Image>();
        var tmpColor = myImage.color;
        tmpColor.a = 0.0f;
        myImage.color = tmpColor;

        isActive = false;
    }

    public void SetOn()
    {
        isActive = true;
        var tmpColor = myImage.color;
        tmpColor.a = 1.0f;
        myImage.color = tmpColor;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // Open window with text
        // ...
    }
}
