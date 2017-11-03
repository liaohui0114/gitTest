using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

 
public class HPBurstBoard : MonoBehaviour
{
    public List<SpriteRenderer>  DigitRender;
    float scaleParam =5f;
    float distance = 1f;

    void OnEnable()
    {
 
        this.gameObject.transform.localScale = Vector3.one * scaleParam;

        StartCoroutine(DelayVanish(1.5f));
    }

    IEnumerator DelayVanish(float time)
    {
        float t = 0f;
        while(t < time)
        {
            Color tempColor = DigitRender[0].color;
            tempColor.a = Mathf.Clamp(1f - t / time, 0f, 1f);

            for (int i = 0; i < DigitRender.Count; i++)
            {
                DigitRender[i].color = tempColor;
            }
                   

            this.gameObject.transform.position += scaleParam * distance * Vector3.up * Time.deltaTime;
            //if (Camera.main != null)
            //    this.gameObject.transform.forward = (this.transform.position - Camera.main.transform.position).normalized;
            t += Time.deltaTime;
            yield return null;
        }
                                   
        this.gameObject.SetActive(false);
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetBit(int bit, Sprite sp)
    {
        DigitRender[bit].sprite = sp;
    }

    public void ClearBits()
    {
        for (int i = 0; i < DigitRender.Count; i++)
            DigitRender[i].sprite = null;
    }


    void OnDestroy()
    {
        Debugger.Log("hp board is destroied");
    }

}



