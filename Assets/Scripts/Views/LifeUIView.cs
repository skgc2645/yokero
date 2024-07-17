using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class LifeUIView : MonoBehaviour
{
    //hierarchy
    [SerializeField] Transform FullHeartPanel;
    [SerializeField] Transform DamagedHeartPanel;
    //spriteSource
    [SerializeField] Sprite FullHeartSprite;
    [SerializeField] Sprite DamagedHeartSprite;


    //íËêî
    Vector3 INIT_HEART_POS = new Vector3(-44f, 0.5f, 0f);
    float HEART_OFFSET = 22f;
    Vector3 HEART_SCALE = new Vector3(0.2f, 0.14f, 0.14f);

    List<GameObject> _fullSpriteObjectList    = new List<GameObject>();
    List<GameObject> _damagedSpriteObjectList = new List<GameObject>();


    public void Initialize()
    {
        //FullHeart sprite generate
        for (int i = 0; i < 5; i++)//MagicNumber
        {
            GameObject go = new GameObject("FullHeart_" + i);
            go.transform.parent = FullHeartPanel;
            go.transform.localPosition = new Vector3(INIT_HEART_POS.x + HEART_OFFSET * i, INIT_HEART_POS.y, INIT_HEART_POS.z);
            go.transform.localScale = HEART_SCALE;
            Image img = go.AddComponent<Image>();
            img.sprite = FullHeartSprite;
            _fullSpriteObjectList.Add(go);
        }
        //DamagedHeart sprite generate
        for (int i = 0; i < 5; i++)//MagicNumber
        {
            GameObject go = new GameObject("DamagedHeart_" + i);
            go.transform.parent = DamagedHeartPanel;
            go.transform.localPosition = new Vector3(INIT_HEART_POS.x + HEART_OFFSET * i, INIT_HEART_POS.y, INIT_HEART_POS.z);
            go.transform.localScale = HEART_SCALE;
            Image img = go.AddComponent<Image>();
            img.sprite = DamagedHeartSprite;
            go.SetActive(false);
            _damagedSpriteObjectList.Add(go);
        }
    }


    public void ChangeHeartSprite(int idx)
    {
        _fullSpriteObjectList[idx].gameObject.SetActive(false);
        _damagedSpriteObjectList[idx].gameObject.SetActive(true);
    }
}

