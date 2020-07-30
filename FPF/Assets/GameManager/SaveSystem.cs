using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] private Manager    manager;

    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream  stream = new FileStream(manager.filePath, FileMode.Create);
        formatter.Serialize(stream, manager.data);
        stream.Close();
    }
}
