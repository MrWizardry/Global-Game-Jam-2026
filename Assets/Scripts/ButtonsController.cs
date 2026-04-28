using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    //[SerializeField] private GameObject optionsMenu;

#if UNITY_STANDALONE || UNITY_EDITOR
    public void OnStartButtonPressed(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnOptionsButtonPressed()
    {
        //optionsMenu.SetActive(true);
    }
    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
#endif

#if UNITY_WEBGL
    public void OnStartButtonPressed()
    {
        SceneManager.LoadScene("Modular_Scene");
    }

    public void OnOptionsButtonPressed()
    {
        //optionsMenu.SetActive(true);
    }
    public void OnExitButtonPressed()
    {
        Debug.Log("Thanks for playing!");
    }
#endif
}
