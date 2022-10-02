using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    Menu,
    Playing,
    Dead
}

public class GameDirector : MonoBehaviour
{
    public Player player;
    public PivotController pivot;
    public GameStateHolder state;

    private float deathTime;
    
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 150, 100, 50), "Respawn")) {
            player.dead = false;
        }
    }

    void Awake()
    {
        state.score = 0;
        state.state = GameState.Menu;
        state.pivot = pivot;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state.state == GameState.Playing) {
            if (player.dead) {
                state.state = GameState.Dead;
                deathTime = Time.time;
                pivot.DestroyAllObstacles();
            }
        }

        if (state.state == GameState.Dead) {
            if (Time.time - deathTime > 4f)
                player.dead = false;
            if (!player.dead) {
                state.state = GameState.Menu;
                pivot.radius = 40000f;
                player.transform.position = Vector3.up * 40100f;
            }
        }
    }
}
