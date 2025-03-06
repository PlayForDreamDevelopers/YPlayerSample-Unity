using System;
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

    public string[] videoPath =
    {
        "https://media.w3.org/2010/05/sintel/trailer.mp4",
        // 本地路径的使用参考：1. 需要本地访问权限；2. 需要将上面的链接下载到设备的根目录
        "/storage/emulated/0/trailer.mp4",
        "https://apisr.yvrdream.com/vrmc/vedios/cce01112-4d47-4ecc-977e-1b0efefad070.mp4",
        "https://apisr.yvrdream.com/vrmc/vedios/deaa4388-db76-4c23-8c0b-26167c727559.mp4",
        "https://apisr.yvrdream.com/vrmc/vedios/28b0542e-9e38-4275-be3c-3da7b206c913.mp4",
        "https://apisr.yvrdream.com/vrmc/vedios/75e5f448-eaec-4419-8c1e-33520cf366f0.mp4"
    };

    public UnityEvent<string> videoSelected;

    private void Awake()
    {
        // 请求读取本地磁盘中视频文件的权限
        Permission.RequestUserPermission(Permission.ExternalStorageRead);

        m_YPlayer = GetComponent<YPlayer>();
        m_YPlayer.Init();
    }

    private void Start()
    {
        for (int i = 0; i < videoPath.Length; i++)
        {
            var item = Instantiate(listItem, m_ToggleGroup.transform);
            var toggle = item.GetComponent<Toggle>();
            toggle.group = m_ToggleGroup;
            var label = item.GetComponentInChildren<Text>();
            var path = videoPath[i];
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
        if (videoPath.Length > 0)
        {
            OpenMedia(videoPath.Last());
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