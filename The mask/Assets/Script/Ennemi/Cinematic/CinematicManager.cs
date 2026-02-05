using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    public IronMaidenCinematic ironMaidenCinematic;
    public ClownCinematic clownCinematic;
    public Ennemi ennemi;
    
    public void PlayCinematic()
    {
        if (ironMaidenCinematic != null && ennemi.monster != null && ennemi != null && ennemi.monster.monsterID == 1)
        {
            ironMaidenCinematic.play();
        }

        if (clownCinematic != null && ennemi != null && ennemi.monster != null && ennemi.monster.monsterID == 2)
        {
            clownCinematic.play();
        }
    }
    
    
}
