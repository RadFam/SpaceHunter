using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GeneralMenu : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GeneralSubMenu subPanel = gameObject.transform.GetChild(0).gameObject.GetComponent<GeneralSubMenu>();
        if (!subPanel.isOnMouse())
        {
            gameObject.SetActive(false);
        }
    }
}
