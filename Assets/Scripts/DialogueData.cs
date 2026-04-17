using UnityEngine;


[CreateAssetMenu(fileName = "Novo", menuName = "Dialogo")]

public class DialogueData : ScriptableObject
{
    [TextArea]
    public string[] lines;
}
