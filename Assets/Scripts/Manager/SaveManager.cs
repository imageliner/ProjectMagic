using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager singleton;

    public SaveData data;

    private string path;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        path = Path.Combine(Application.persistentDataPath, "save.json");
        LoadOnStart();
    }

    private void LoadOnStart()
    {
        if (File.Exists(path))
            data = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
        else
            data = new SaveData();
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
        Debug.Log("Saved data to " + path);
    }

    public SaveData LoadData()
    {
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<SaveData>(json);
        }

        Debug.LogWarning("No save file found");
        return null;
    }

}
