using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CourseIndicator : MonoBehaviour
{
    public GameStateHolder state;
    Canvas canvas;
    public TMP_Text courseName;
    public TMP_Text score;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = Mathf.Round(state.score).ToString();
    }

    public void FlashName(string name)
    {
        courseName.text = name;
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        float start = Time.time;
        courseName.enabled = true;

        while(state.player.Falling)
            yield return null;
        courseName.enabled = false;

        yield return null;
    }
}
