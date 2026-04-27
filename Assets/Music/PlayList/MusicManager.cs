using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public enum PlayMode { Sequential, Random, Loop, LoopOne }

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Header("Playlist")]
    public PlayListData playlist;
    public PlayMode playMode = PlayMode.Sequential;

    [Header("Audio")]
    public AudioMixerGroup musicMixerGroup; // arraste o grupo "Music" do seu AudioMixer aqui

    [Header("Crossfade")]
    public float crossfadeDuration = 1.5f;

    private AudioSource[] _sources = new AudioSource[2];
    private int _activeSource = 0;
    private int _currentIndex = 0;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < 2; i++)
        {
            _sources[i] = gameObject.AddComponent<AudioSource>();
            _sources[i].playOnAwake = false;
            _sources[i].loop = false;
            _sources[i].volume = 0f;
            _sources[i].spatialBlend = 0f;
            _sources[i].priority = 0;
            _sources[i].outputAudioMixerGroup = musicMixerGroup; // <-- aqui a conexão
        }
    }

    void Start() => PlayCurrent();

    void Update()
    {
        // Avança automaticamente quando a faixa termina
        if (!_sources[_activeSource].isPlaying && playMode != PlayMode.LoopOne)
            Next();
    }

    public void PlayCurrent()
    {
        var track = playlist.tracks[_currentIndex];
        CrossfadeTo(track);
    }

    public void Next()
    {
        _currentIndex = playMode switch
        {
            PlayMode.Random     => Random.Range(0, playlist.tracks.Length),
            PlayMode.Loop       => (_currentIndex + 1) % playlist.tracks.Length,
            PlayMode.LoopOne    => _currentIndex,
            _                   => Mathf.Min(_currentIndex + 1, playlist.tracks.Length - 1)
        };
        PlayCurrent();
    }

    public void Previous()
    {
        _currentIndex = Mathf.Max(_currentIndex - 1, 0);
        PlayCurrent();
    }

    public void SetPlayMode(PlayMode mode) => playMode = mode;

    void CrossfadeTo(Track track)
    {
        int next = 1 - _activeSource;
        _sources[next].clip = track.clip;
        _sources[next].Play();
        StopAllCoroutines();
        StartCoroutine(Crossfade(_sources[_activeSource], _sources[next], track.volume));
        _activeSource = next;
    }

    IEnumerator Crossfade(AudioSource from, AudioSource to, float targetVolume)
    {
        float elapsed = 0f;
        float fromStart = from.volume;

        while (elapsed < crossfadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / crossfadeDuration;
            from.volume = Mathf.Lerp(fromStart, 0f, t);
            to.volume   = Mathf.Lerp(0f, targetVolume, t);
            yield return null;
        }

        from.Stop();
        from.volume = 0f;
    }
}