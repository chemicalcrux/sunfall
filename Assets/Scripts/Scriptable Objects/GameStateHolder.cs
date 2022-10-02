using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game State Holder", fileName = "NewGameState")]
public class GameStateHolder : ScriptableObject
{
    public float score;
    public GameState state;
    public Player player;
    public PivotController pivot;
}
