using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SaveManager : MonoBehaviour
{
    private static SaveManager _instance;
    public static SaveManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<SaveManager>();
            if (_instance == null)
                Debug.LogError($"{typeof(SaveManager)} Has not exist in current Scene");
            return _instance;
        }
    }

    [SerializeField] private AudioMixer _mixer;
    public SaveData data;

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);

        string json = PlayerPrefs.GetString("data", string.Empty);
        if (!string.IsNullOrEmpty(json))
        {
            data = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            data = new SaveData()
            {
                mapDatas = new List<MapInfo>(),
                BGMValue = 0.5f,
                senseValue = 0.5f,
                SFXValue = 0.5f,
            };
        }
        _mixer.SetFloat("SFX", data.SFXValue);
        _mixer.SetFloat("BGM", data.BGMValue);
    }
    [ContextMenu("DataReset")]
    public void ResetSave()
    {
        PlayerPrefs.SetString("data", string.Empty);
    }
    public MapInfo LoadMapInfoOrDefault(DifficultyType type)
    {
        foreach (MapInfo info in data.mapDatas)
        {
            if (info.difficultyType == type)
            {
                return info;
            }
        }
        return default(MapInfo);
    }
    public void SaveMapInfo(MapInfo newInfo)
    {
        for (int i = 0; i < data.mapDatas.Count; i++)
        {
            if (data.mapDatas[i].difficultyType == newInfo.difficultyType)
            {
                data.mapDatas[i] = newInfo;
                return;
            }
        }
    }
    private void OnDestroy()
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("data", json);
    }

}
