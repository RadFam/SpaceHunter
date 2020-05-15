using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerResourceValueScript : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    private float lifetime = 0.5f;
    private float scaleFactor = 1.0f;
    private Vector3 moveYSpeed = new Vector3(0.0f, 20.0f, 0.0f);
    private float timer = 0.0f;
    private Color textColor;
    
    // Start is called before the first frame update
    public static PlayerResourceValueScript Create(int value, Transform parent)
    {
        Transform textMeshTr = Instantiate(ConstGameCtrl.instance.playerTMPro_01, Vector3.zero, Quaternion.identity);
        textMeshTr.parent = parent;
        textMeshTr.transform.localPosition = new Vector3(0f, -7f, 0f);
        textMeshTr.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        textMeshTr.transform.localScale = new Vector3(1f, 1f, 1f);
        PlayerResourceValueScript PRVS = textMeshTr.GetComponent<PlayerResourceValueScript>();
        PRVS.Setup(value);

        return PRVS;
    }

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshProUGUI>();
    }

    public void Setup(int value)
    {
        string txt = "+" + value.ToString();
        textMesh.text = txt;

        textColor = textMesh.color;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += moveYSpeed * Time.deltaTime;
        moveYSpeed -= moveYSpeed * 4f * Time.deltaTime;

        timer += Time.deltaTime;

        if (timer < lifetime*0.8f)
        {
            transform.localScale += Vector3.one * scaleFactor * Time.deltaTime;
        }
        else
        {
            transform.localScale -= Vector3.one * scaleFactor * Time.deltaTime;
        }

        if (timer >= lifetime)
        {
            float dissapearSpeed = 4.0f;
            textColor.a -= dissapearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
