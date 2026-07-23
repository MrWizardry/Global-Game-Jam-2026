using UnityEngine;

public class EnemyLimiter : MonoBehaviour
{
    public GameObject[] enemies;

    public bool alertActive = false;

    public bool CanActivate()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeInHierarchy)
                return false;
        }

        return true;
    }
}