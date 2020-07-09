using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class LoadGameUI : MonoBehaviour
{
    [SerializeField] private GameObject gameSelectPrefab;
    [SerializeField] private GameObject viewerContent;
    void Start()
    {

        string[]    files = Directory.GetFiles(Application.persistentDataPath);
        BinaryFormatter formatter = new BinaryFormatter();
        for (int i = 0; i < files.Length; i++) {
            FileStream  stream = new FileStream(files[i], FileMode.Open);
            SaveFormat sf = formatter.Deserialize(stream) as SaveFormat;
            stream.Close();
            GameObject gs = Instantiate(gameSelectPrefab);
            gs.GetComponent<GameSelect>().Init(sf, files[i]);
            gs.transform.SetParent(viewerContent.transform, false);
        }
 
    }

}
