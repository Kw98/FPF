using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine.UIElements;

public class BreedingSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] animalPrefabs;
    [SerializeField] private Transform[] posArray;
    [SerializeField] public int Max;
    public Dictionary<string, SaveFormat_Animal> animals = new Dictionary<string, SaveFormat_Animal>();
    private Manager manager;
    private Queue<Vector3> positions = new Queue<Vector3>();
    private string first = "";
    public int current = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("GameManager"))
        {
            Destroy(this);
            return;
        }
        foreach (Transform pos in posArray)
            positions.Enqueue(pos.position);
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        ClearDead();
    }

    public void NewAnimal(int animalId)
    {
        if (current >= Max)
            return;
        int key = Random.Range(-9999, 9999);
        while (animals.ContainsKey(key.ToString("D4")))
            key = Random.Range(-9999, 9999);

        Vector3 pos = positions.Dequeue();
        GameObject animal = Instantiate(animalPrefabs[animalId], pos, Quaternion.identity);
        animal.name = key.ToString("D4");
        animals.Add(key.ToString("D4"), animal.GetComponent<Animal>().stat);
        ++current;
    }

    public void Breed(string second)
    {
        if (first == "")
        {
            first = second;
            return;
        }

        if (!animals.ContainsKey(first) || !animals.ContainsKey(second))
        {
            first = "";
            return;
        }

        Animal a1 = GameObject.Find(first).GetComponent<Animal>();
        Animal a2 = GameObject.Find(second).GetComponent<Animal>();

        if (a1.stat.adult && a2.stat.adult && a1.stat.food >= 75 && a2.stat.food >= 75 && a1.stat.id == a2.stat.id && first != second)
            NewAnimal(a1.stat.id);

        first = "";
    }

    private void Load()
    {
        if (File.Exists(manager.data.farmDictionaryLocation))
        {
            var formatter = new BinaryFormatter();
            FileStream stream = new FileStream(manager.data.farmDictionaryLocation, FileMode.Open);
            animals = (Dictionary<string, SaveFormat_Animal>)formatter.Deserialize(stream);
        }
        foreach (var animal in animals)
        {
            GameObject go = Instantiate(animalPrefabs[animal.Value.id], new Vector3(animal.Value.posX, animal.Value.posY, animal.Value.posZ), Quaternion.identity);
            go.name = animal.Key;
            go.GetComponent<Animal>().stat = animal.Value;
        }
    }

    public void Save()
    {
        ClearDead();
        foreach (var animal in animals.ToList())
        {
            GameObject ago = GameObject.Find(animal.Key);
            if (ago)
                animals[animal.Key] = ago.GetComponent<Animal>().stat;
        }

        var formatter = new BinaryFormatter();
        FileStream stream = new FileStream(manager.data.farmDictionaryLocation, FileMode.OpenOrCreate);
        formatter.Serialize(stream, animals);
        stream.Close();
    }

    private void ClearDead()
    {
        foreach (var v in animals.Where(pair => GameObject.Find(pair.Key) == null).ToList())
        {
            positions.Enqueue(new Vector3(v.Value.posX, v.Value.posY, v.Value.posZ));
            --current;
            animals.Remove(v.Key);
        }
    }
}
