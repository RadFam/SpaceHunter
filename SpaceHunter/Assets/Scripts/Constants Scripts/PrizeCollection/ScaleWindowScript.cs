using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleWindowScript : MonoBehaviour
{
    RectTransform rectTransform;

    public GameObject Content;
    public GameObject CollectPanel;
    public List<GameObject> MineralImages = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScaleInnerContent()
    {
        float width = rectTransform.rect.width;
        float height = width * 2.0f;

        RectTransform rtContent = Content.GetComponent<RectTransform>();
        rtContent.sizeDelta = new Vector2(rtContent.sizeDelta.x, width * 2.0f);

        GridLayoutGroup glg = CollectPanel.GetComponent<GridLayoutGroup>();
        glg.cellSize = new Vector2(width * 0.96875f / 5, width * 0.96875f / 5);
        glg.padding.left = (int)(width * 0.015625f);
        glg.padding.top = (int)(width * 0.015625f);

        foreach (GameObject me in MineralImages)
        {
            LayoutElement le = me.GetComponent<LayoutElement>();
            le.flexibleHeight = (int)(width * 0.96875f / 5);
            le.flexibleWidth = (int)(width * 0.96875f / 5);
        }  
    }

    public void RedrawCollection()
    {
        List<GeoPrize> collection = ConstGameCtrl.instance.playerCollection;

        int ind = -1;
        for (int i = 0; i < 50; ++i)
        {
            GameObject me = MineralImages[i];
            Image img = me.GetComponent<Image>();
            ind = collection.FindIndex(x => x.prizeName == ConstGameCtrl.instance.allPrizes[i].prizeName);
            if (ind >= 0)
            {
                var tempColor = img.color;
                tempColor.a = 1f;
                img.color = tempColor;
                img.sprite = ConstGameCtrl.instance.allPrizes[i].prizeSprite;
            }
            else
            {
                var tempColor = img.color;
                tempColor.a = 0f;
                img.color = tempColor;
            }
        }
    }
}
