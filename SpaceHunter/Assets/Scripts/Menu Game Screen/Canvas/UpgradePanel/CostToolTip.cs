using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostToolTip : MonoBehaviour
{
    public Text myInfo;

    [SerializeField]
    private Camera uiCamera;
    private Vector2 addCoords;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void OnShowTooltip(string name, int vol)
    {
        gameObject.SetActive(true);

        string finalText = " " + name + "\n" + " Цена: " + vol.ToString() + "$";
        myInfo.text = finalText;
        

        RectTransform rtr = gameObject.transform.GetComponent<RectTransform>();
        addCoords = new Vector2(rtr.rect.width / 3 + 5, rtr.rect.height / 3 + 5);
        addCoords = new Vector2(Mathf.Round(addCoords.x), Mathf.Round(addCoords.y));

        //Update();
    }

    public void OnCloseTooltip()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out Vector2 localPoint);
        localPoint = new Vector2(Mathf.Round(localPoint.x), Mathf.Round(localPoint.y));
        transform.localPosition = localPoint + addCoords;
        //transform.localPosition = localPoint;
    }
}
