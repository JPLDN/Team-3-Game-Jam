using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
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
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public bool HasFlag(GameFlags flag) => gameFlags.Contains(flag);

    public bool SetFlag(GameFlags flag, bool value = true)
    {
        if (value)
        {
            return gameFlags.Add(flag);
        }
        else
        {
            return gameFlags.Remove(flag);
        }
    }

    public bool ToggleFlag(GameFlags flag)
    {
        if (gameFlags.Contains(flag))
        {
            gameFlags.Remove(flag);
            return false;
        }
        else
        {
            gameFlags.Add(flag);
            return true;
        }
    }

    public bool CanPlayMinigame(Minigames minigame)
    {
        var allFlags = Enum.GetValues(typeof(GameFlags))
                           .Cast<GameFlags>()
                           .ToList();

        string joinedFlags = string.Join(", ", allFlags.Select(f => f.ToString()));
        Debug.Log($"All GameFlags: {joinedFlags}");

        string activeFlags = string.Join(", ", gameFlags);
        Debug.Log($"Active GameFlags: {activeFlags}");

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
