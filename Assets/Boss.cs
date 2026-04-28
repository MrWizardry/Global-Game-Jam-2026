using UnityEngine;

public class Boss : MonoBehaviour
{
    private CozinheiroMissao cozinheiroMissao;
    public GameObject boss;

    private void Start()
    {
        cozinheiroMissao = Object.FindAnyObjectByType<CozinheiroMissao>();
        boss.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boss.SetActive(true);
        }
    }
}
