using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    [SerializeField] private List<ScriptableObject> objectsToPersist;
    
    private void OnEnable()
    {
        for (int i = 0; i < objectsToPersist.Count; i++)
        {
            if (File.Exists(Application.persistentDataPath + string.Format("/{0}.mrvs", objectsToPersist[i].name)))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream =
                    File.Open(Application.persistentDataPath + string.Format("/{0}.mrvs", objectsToPersist[i].name),
                        FileMode.Open);
                JsonUtility.FromJsonOverwrite((string) formatter.Deserialize(stream), objectsToPersist[i]);
                stream.Close();
            }
            else
            {
                Debug.Log("Dont load");
            }
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < objectsToPersist.Count; i++)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream =
                File.Create(Application.persistentDataPath + string.Format("/{0}.mrvs", objectsToPersist[i].name));

            var json = JsonUtility.ToJson(objectsToPersist[i]);
            formatter.Serialize(stream, json);
            stream.Close();
        }
    }
    public void SaveData(ScriptableObject dataType)
    {
        foreach (var t in objectsToPersist)
        {
            if (dataType == t)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = File.Create(Application.persistentDataPath +
                                                string.Format("/{0}.mrvs", t.name));

                var json = JsonUtility.ToJson(t);
                formatter.Serialize(stream, json);
                stream.Close();
            }
        }
    }
}