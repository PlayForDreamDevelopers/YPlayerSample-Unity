using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Events;
using UnityEngine.UI;
using YVR.Player;

[RequireComponent(typeof(YPlayer))]
public class PlayerDemo : MonoBehaviour
{
    private YPlayer m_YPlayer;

    [SerializeField] private Text m_TimeInfo;
    [SerializeField] private Text m_DebugInfo;

    [SerializeField] private Slider m_TimeSlider;
    [SerializeField] private Slider m_VolumeSlider;

    [SerializeField] private Button m_Play;
    [SerializeField] private Button m_Pause;
    [SerializeField] private Button m_Mute;
    [SerializeField] private Button m_Unmute;

    [SerializeField] private ToggleGroup m_ToggleGroup;

    public GameObject listItem;

    public List<string> playerLists = new()
    {
        "https://media.w3.org/2010/05/sintel/trailer.mp4"
    };

    public UnityEvent<string> videoSelected;

    private void Awake()
    {
        // Request permission to read video files on local disk
        Permission.RequestUserPermission(Permission.ExternalStorageRead);

        m_YPlayer = GetComponent<YPlayer>();
        m_YPlayer.Init();
    }

    private void Start()
    {
        var localVideosPath = Path.Combine(Application.persistentDataPath, "videos");

        if (!Directory.Exists(localVideosPath))
        {
            Directory.CreateDirectory(localVideosPath);
        }

        string[] files = Directory.GetFiles(localVideosPath, "*.*", SearchOption.AllDirectories);
        for (int i = 0; i < files.Length; i++)
        {
            Debug.Log($"local file is: {files[i]}");
        }

        playerLists.AddRange(files);
        for (int i = 0; i < playerLists.Count; i++)
        {
            var item = Instantiate(listItem, m_ToggleGroup.transform);
            var toggle = item.GetComponent<Toggle>();
            toggle.group = m_ToggleGroup;
            var label = item.GetComponentInChildren<Text>();
            var path = playerLists[i];
            label.text = path;
            toggle.onValueChanged.AddListener(isOn =>
            {
                if (isOn)
                {
                    videoSelected?.Invoke(path);
                }
            });
        }

        videoSelected.AddListener(OpenMedia);
        if (playerLists.Count > 0)
        {
            OpenMedia(playerLists.Last());
        }
    }

    private void Update()
    {
        bool isPlaying = m_YPlayer.IsPlaying();
        m_Play.gameObject.SetActive(!isPlaying);
        m_Pause.gameObject.SetActive(isPlaying);
        bool isMuted = m_YPlayer.IsMuted();
        m_Mute.gameObject.SetActive(!isMuted);
        m_Unmute.gameObject.SetActive(isMuted);

        if (isPlaying)
        {
            var current = m_YPlayer.GetCurrentTime();
            var fullDuration = m_YPlayer.GetDuration();
            var currentTime = TimeSpan.FromMilliseconds(current);
            var fullDurationTime = TimeSpan.FromMilliseconds(fullDuration);
            m_TimeInfo.text = $"{currentTime:hh\\:mm\\:ss}/{fullDurationTime:hh\\:mm\\:ss}";
            m_TimeSlider.SetValueWithoutNotify(Mathf.Clamp(current / (float)fullDuration, 0.0f, 1.0f));
        }

        m_VolumeSlider.SetValueWithoutNotify(m_YPlayer.GetVolume());

        UpdateDebugInfo();
    }

    private void UpdateDebugInfo()
    {
        m_DebugInfo.text = m_YPlayer.GetDebugInfo();
    }

    public void OpenMedia(string path)
    {
        m_YPlayer.OpenMedia(path);
    }

    public void Play()
    {
        m_YPlayer.Play();
    }

    public void Pause()
    {
        m_YPlayer.Pause();
    }

    public void Stop()
    {
        m_YPlayer.Stop();
    }

    public void SetLooping(bool b)
    {
        m_YPlayer.SetLooping(b);
    }

    public void SetHDR(bool b)
    {
        m_YPlayer.SetColorSpace((int)(b ? HDRType.HDR10 : HDRType.SDR));
    }

    public void Seek(float f)
    {
        m_YPlayer.Seek((long)(f * m_YPlayer.GetDuration()));
    }

    public void Mute()
    {
        m_YPlayer.MuteAudio(true);
    }

    public void Unmute()
    {
        m_YPlayer.MuteAudio(false);
    }

    public void SetVolume(float f)
    {
        m_YPlayer.SetVolume(f);
    }
}