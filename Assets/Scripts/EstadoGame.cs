using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class EstadoGame : MonoBehaviour
{
    public static EstadoGame estadoGame;
    public int scoreMax = 0;
    private string rutaArchivo;

    private void Awake()
    {
        rutaArchivo = Application.persistentDataPath + "/datos.dat";
        if (estadoGame == null)
        {
            estadoGame = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (estadoGame != this)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(rutaArchivo);

        SaveData datos = new SaveData();
        datos.scoreMax = scoreMax;

        bf.Serialize(file, datos);

        file.Close();
    }

   public void Load()
    {
        if (File.Exists(rutaArchivo))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(rutaArchivo, FileMode.Open);

            SaveData datos = (SaveData)bf.Deserialize(file);
            scoreMax = datos.scoreMax;

            file.Close();
        }
        else
        {
            scoreMax = 0;
        }
    }
}

[Serializable]
class SaveData
{
    public int scoreMax;
}