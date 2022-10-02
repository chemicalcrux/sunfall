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

    public float interval = 2000;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = Mathf.Round(state.score).ToString() + " meters";
        float progress = (state.score % interval) / interval;
        if (progress > 0.5f) {
            progress -= 1;
        }

        Debug.Log(progress);
        progress = Mathf.Abs(progress);

        float scale = 1f;
        if (progress < 0.05f) {
            progress *= 20;
            progress = Mathf.Pow(progress, 2);
            scale = 1 + 0.4f * (1 - progress);
        }

        score.rectTransform.localScale = scale * Vector3.one;
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
