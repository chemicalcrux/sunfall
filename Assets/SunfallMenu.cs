using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
public class SunfallMenu : MonoBehaviour
{
    public enum CurrentMenu
    {
        Main,
        Settings
    }

    public GameStateHolder state;
    public CurrentMenu currentMenu;
    public CanvasGroup canvasGroup;
    public CinemachineVirtualCamera mainMenuCam;

    public TMP_Dropdown postProcessDropdown;

    private float alphaVelocity;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Post Processing")) {
            PlayerPrefs.SetInt("Post Processing", 2);
        }

        postProcessDropdown.onValueChanged.AddListener((choice) => {
            PlayerPrefs.SetInt("Post Processing", choice);
        });
        postProcessDropdown.SetValueWithoutNotify(PlayerPrefs.GetInt("Post Processing"));
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.up * (state.pivot.radius + 100);
        float targetAlpha = state.state == GameState.Menu ? 1f : 0f;
        canvasGroup.alpha = Mathf.SmoothDamp(canvasGroup.alpha, targetAlpha, ref alphaVelocity, 1.0f);

        mainMenuCam.enabled = false;

        if (state.state == GameState.Menu && currentMenu == CurrentMenu.Main)
            mainMenuCam.enabled = true;
    }

    public void StartGame()
    {
        state.pivot.StartGame();
        state.state = GameState.Playing;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
