using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameFlags
{
    None,
    IsMinigame1Complete,
    IsMinigame2Complete,
    IsMinigame3Complete,
}

public enum Minigames
{
    Minigame1,
    Minigame2,
    Minigame3,
}

[DefaultExecutionOrder(-10000)]
public sealed class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    private readonly HashSet<GameFlags> gameFlags = new HashSet<GameFlags>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("Error");

        if (Instance == this)
        {
            Instance = null;
        }
    }

    public bool HasFlag(GameFlags flag) => gameFlags.Contains(flag);
    public bool SetFlag(GameFlags flag, bool value = true)
    {
        bool changed = value ? gameFlags.Contains(flag) : false;
        return changed;
    }

    public bool ToggleFlag(GameFlags flag)
    {
        bool newValue = !gameFlags.Contains(flag);
        SetFlag(flag, newValue);
        return newValue;
    }

    public bool CanPlayMinigame(Minigames minigame)
    {
        switch (minigame)
        {
            case Minigames.Minigame2 when HasFlag(GameFlags.IsMinigame1Complete):
            case Minigames.Minigame3 when HasFlag(GameFlags.IsMinigame2Complete):
            case Minigames.Minigame1:
                return true;
        }
        return false;
    }
}
