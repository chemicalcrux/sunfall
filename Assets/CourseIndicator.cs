using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CourseIndicator : MonoBehaviour
{
    Canvas canvas;
    TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        text = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FlashName(string name)
    {
        text.text = name;
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        float start = Time.time;
        canvas.enabled = true;

        while (Time.time - start < 1) {
            yield return null;
        }

        canvas.enabled = false;

        yield return null;
    }
}
