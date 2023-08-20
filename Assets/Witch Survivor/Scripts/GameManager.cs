using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{    
    public Player player;
    [SerializeField]
    private SpawnData[] spawnDatas = null;
    [SerializeField]
    private Character[] characters = null;
    [SerializeField]
    private RuntimeAnimatorController[] controllers = null;

    public SpawnData GetSpawnData(int index)
    {
        return spawnDatas[index];
    }

    public Character GetCharacter(int index)
    {
        return characters[index];
    }

    public RuntimeAnimatorController GetController(int index)
    {
        return controllers[index];
    }
}
