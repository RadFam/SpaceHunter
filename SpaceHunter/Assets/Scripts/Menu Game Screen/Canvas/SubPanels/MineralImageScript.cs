using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MineralImageScript : MonoBehaviour, IPointerClickHandler
{
    public CollectionPanelScaleScript cpss;

    private Image myImage;
    private Sprite mySprite;
    [SerializeField]
    private int myNumber;
    [SerializeField]
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
        /*
        myImage = GetComponent<Image>();
        var tmpColor = myImage.color;
        tmpColor.a = 0.0f;
        myImage.color = tmpColor;
        
        myNumber = -1;
        isActive = false;
        */
    }

    public void SetOn(Sprite sprite, int num)
    {
        isActive = true;
        mySprite = sprite;
        myNumber = num;

        //Debug.Log("ON   num: " + myNumber.ToString() + "   isActive: " + isActive);
        myImage = GetComponent<Image>();
        var tmpColor = myImage.color;
        tmpColor.a = 1.0f;
        myImage.color = tmpColor;

        myImage.sprite = mySprite;
        //Debug.Log("ON   num: " + myNumber.ToString() + "   isActive: " + isActive);
    }
    public void SetOff()
    {
        //Debug.Log("OFF   num: " + myNumber.ToString() + "   isActive: " + isActive);
        isActive = false;
        myImage = GetComponent<Image>();
        var tmpColor = myImage.color;
        tmpColor.a = 0.0f;
        myImage.color = tmpColor;

        //Debug.Log("OFF   num: " + myNumber.ToString() + "   isActive: " + isActive);
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        // Open window with text
        Debug.Log("Me clicked: " + myNumber);
        if (isActive)
        {
            //Debug.Log("True");
            cpss.OpenMineralInfoPanel(myNumber);
        }
    }
}
