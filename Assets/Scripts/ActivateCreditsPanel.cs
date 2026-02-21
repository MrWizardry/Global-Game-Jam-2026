using UnityEngine;
using UnityEngine.UI;

using UnityEngine.UIElements;

public class ActivateCreditsPanel : MonoBehaviour
{
    public GameObject creditsPanelON;
    private void Awake()
    {
        creditsPanelON.SetActive(false);
    }
    public void ActivatePanel()
    {
        if (!creditsPanelON.activeSelf)
            creditsPanelON.SetActive(true);
    }

    public void DeactivatePanel()
    {
        if (creditsPanelON.activeSelf)
            creditsPanelON.SetActive(false);
    }
}
