using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    public IronMaidenCinematic ironMaidenCinematic;
    public ClownCinematic clownCinematic;
    public AngelCinematic angelCinematic;
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

        if (angelCinematic != null && ennemi != null && ennemi.monster != null && ennemi.monster.monsterID == 3)
        {
            angelCinematic.play();
        }
    }
    
    
}
