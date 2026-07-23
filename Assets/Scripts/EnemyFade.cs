using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyFade : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent enemy;
    public Image vignetteImage;

    public float maxDistance = 20;
    public float minDistance = 2;
    public float fadeSpeed = 5;

    private float currentAlpha;

    void Start()
    {
        SetAlpha(0f);
        vignetteImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (player == null || vignetteImage == null)
            return;

        
        if (enemy == null)
        {
            FindClosestEnemy();
            FadeOutCompletely();
            return;
        }

        
        if (!enemy.gameObject.activeInHierarchy || !enemy.enabled)
        {
            enemy = null;
            FadeOutCompletely();
            return;
        }

        float distance = Vector3.Distance(enemy.transform.position, player.position);

        float targetAlpha = 1f - Mathf.InverseLerp(minDistance, maxDistance, distance);

        if (targetAlpha > 0.01f)
        {
            if (!vignetteImage.gameObject.activeSelf)
                vignetteImage.gameObject.SetActive(true);
        }

        currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * fadeSpeed);

        SetAlpha(currentAlpha);

        if (currentAlpha <= 0.01f)
        {
            vignetteImage.gameObject.SetActive(false);
        }
    }

    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject closest = null;
        float closestDist = Mathf.Infinity;

        foreach (GameObject e in enemies)
        {
            if (!e.activeInHierarchy) continue;

            float d = Vector3.Distance(player.position, e.transform.position);

            if (d < closestDist)
            {
                closestDist = d;
                closest = e;
            }
        }

        if (closest != null)
        {
            enemy = closest.GetComponent<NavMeshAgent>();
        }
    }

    void FadeOutCompletely()
    {
        currentAlpha = Mathf.Lerp(currentAlpha, 0f, Time.deltaTime * fadeSpeed);

        SetAlpha(currentAlpha);

        if (currentAlpha <= 0.01f)
        {
            vignetteImage.gameObject.SetActive(false);
        }
    }

    void SetAlpha(float alpha)
    {
        Color color = vignetteImage.color;
        color.a = alpha;
        vignetteImage.color = color;
    }
}