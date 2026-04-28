using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToCorredor : MonoBehaviour
{
    public string SceneName = "Corredor";
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Load the corredor scene
            SceneManager.LoadScene(SceneName);
        }
    }
}
