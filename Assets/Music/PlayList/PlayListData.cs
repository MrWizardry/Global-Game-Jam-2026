using UnityEngine;


[CreateAssetMenu(fileName = "NewPlaylist", menuName = "Playlist")]
public class PlayListData : ScriptableObject
{
    public Track[] tracks;
}