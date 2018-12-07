using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ссылки на добавленные компоненты (папка Plugins)
using Mono.Data.Sqlite;
using System.Data;
using System.IO;
using System;
using UnityEngine.UI;

public class DBcontroller : MonoBehaviour {

    // объявление необходимых переменных для работы с БД
    public SqliteConnection con_db;
    public SqliteCommand cmd_db; // команда с SQL запросом (для отправки в БД)
    public SqliteDataReader rdr; // для чтения данных из БД
    
    // путь для подключения
    private string path;

    Text text;

	// Use this for initialization
	void Start () {
        text = GameObject.Find("testText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // метод для подключения
    public void Connection()
    {
        // блок try-catch для отлова ошибок
        try
        {
            // если текущая платформа не android - путь к БД один (БД в папке Assets), если android - путь другой (БД для android в папке StreamingAssets)
            if (Application.platform != RuntimePlatform.Android)
            {
                // подключение к БД для Windows 
                path = Application.dataPath + "/ClothingShop.bytes";
            }
            else
            {
                path = Application.persistentDataPath + "/ClothingShop.bytes";
                if (!File.Exists(path))
                {
                    // если по указаному пути нет файла БД - будет создан класс
                    WWW load = new WWW("jar:file://" + Application.dataPath + "!/assets/" + "ClothingShop.bytes"); // копирование файла с БД
                    while (!load.isDone) { }
                    File.WriteAllBytes(path, load.bytes); // в путь path копируется БД (из уже упакованного apk файла)
                }
            }
            // установка соединения
            con_db = new SqliteConnection("URI = file:" + path);
            con_db.Open();
            if (con_db.State == ConnectionState.Open)
            {
                // если соединение открыто
                text.text = path.ToString() + " - is connected";
            }
            // создание команды
            cmd_db = new SqliteCommand("SELECT * FROM brand WHERE id_brand = 1", con_db); // указание запроса, указание созданного соединения
            rdr = cmd_db.ExecuteReader(); // чтение ответа на запрос
            while (rdr.Read()) // действия во время чтения
            {
                text.text = rdr[1].ToString(); // [n] - вывод последнего значения из n столбца (от 0)
            }
        }
        catch (Exception ex)
        {
            text.text = ex.ToString(); // отображение ошибки (при её наличии)
        }
    }
}
