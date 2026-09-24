using UnityEngine;

public class EnterSpaceTest : MonoBehaviour
{
    public GameObject bankUI;
    void OnTriggerEnter(Collider other)
    {
        bankUI.SetActive(true);
    }
    void OnTriggerExit(Collider other)
    {
        bankUI.SetActive(false);
    }
}
