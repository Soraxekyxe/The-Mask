using UnityEngine;

public class PlayerMaskChecker : MonoBehaviour
{
    public int currentMaskID = -1;
    public int currentMonsterID = -1;

    public void SetMask(int maskID)
    {
        currentMaskID = maskID;
    }

    public void SetMonster(int monsterID)
    {
        currentMonsterID = monsterID;
    }

    public bool IsPlayerSafe()
    {
        return currentMaskID == currentMonsterID;
    }
}
