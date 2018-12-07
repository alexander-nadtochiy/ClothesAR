using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
// ссылки на добавленные компоненты (папка Plugins)
using Mono.Data.Sqlite;
using System.Data;
using System.IO;

public class ChangeClothes : MonoBehaviour {

    sbyte selectCategoryUP = 1; // стартовые параметры - без учёта рандома
    sbyte selectCategoryDOWN = 1;
    sbyte selectUP = 1;
    sbyte selectDOWN = 1;

    Text textUIcatUP;
    Text textUIcatDOWN;
    Text textUIUP;
    Text textUIDOWN;
    GameObject modelUPver1;
    GameObject modelUPver2;
    GameObject modelUPver3;
    GameObject modelDOWNver1;
    GameObject modelDOWNver2;
    float modelPosition = -1.2f;

    GameObject infoPanel;

    Text textMessage;
    int launch = 0;

    //string[][] valuesForClothes = new string[5][];
    Text selectTextureUP;
    Text selectTextureDOWN;

    Text prevCatUP;
    Text prevUP;
    Text prevCatDOWN;
    Text prevDOWN;
    Text nextCatUP;
    Text nextUP;
    Text nextCatDOWN;
    Text nextDOWN;

    /*Sprite sprite;
    Material material;*/

    /*Material matDOWNv1;
    Material matDOWNv2;
    Material matUPv1;
    Material matUPv2;
    Material matUPv3;*/

    // объявление необходимых переменных для работы с БД
    public SqliteConnection con_db;
    public SqliteCommand cmd_db; // команда с SQL запросом (для отправки в БД)
    public SqliteDataReader rdr; // для чтения данных из БД

    // путь для подключения
    private string path;

    // количество одежды в каждой категории
    /*byte amountClothesCatUP1;
    byte amountClothesCatUP2;
    byte amountClothesCatUP3;
    byte amountClothesCatDOWN1;
    byte amountClothesCatDOWN2;*/ // не работает - из-за неправильного назначения скриптов - обращаясь к ним в коде (после присвоения), они = 0

    Text CategoryClothesDOWN;
    Text NameClothesDOWN;
    Text PriceClothesDOWN;
    Text SizeAvailabilityClothesDOWN;
    //Sprite ClothesDOWNImageSprite;
    UnityEngine.UI.Image ClothesDOWNImage;
    Text BrandClothesDOWN;

    Text CategoryClothesUP;
    Text NameClothesUP;
    Text PriceClothesUP;
    Text SizeAvailabilityClothesUP;
    //Sprite ClothesUPImageSprite;
    UnityEngine.UI.Image ClothesUPImage;
    Text BrandClothesUP;

    string SizeOne;
    string SizeTwo;
    string SizeThree;

    // Use this for initialization
    void Start() {
        modelUPver1 = GameObject.Find("UPver1");
        modelUPver2 = GameObject.Find("UPver2");
        modelUPver3 = GameObject.Find("UPver3");
        modelDOWNver1 = GameObject.Find("DOWNver1");
        modelDOWNver2 = GameObject.Find("DOWNver2");
        textUIcatUP = GameObject.Find("Text CategoriesUP").GetComponent<Text>();
        textUIUP = GameObject.Find("Text DescriptionUP").GetComponent<Text>();
        textUIcatDOWN = GameObject.Find("Text CategoriesDOWN").GetComponent<Text>();
        textUIDOWN = GameObject.Find("Text DescriptionDOWN").GetComponent<Text>();

        selectTextureUP = GameObject.Find("Text SelectTextureUP").GetComponent<Text>();
        selectTextureDOWN = GameObject.Find("Text SelectTextureDOWN").GetComponent<Text>();

        infoPanel = GameObject.Find("infoPanel");

        /*matDOWNv1 = (Material)Resources.Load("Materials/humanLow_down-v1");
        matDOWNv2 = (Material)Resources.Load("Materials/humanLow_down-v2");
        matUPv1 = (Material)Resources.Load("Materials/humanLow_up-v1");
        matUPv2 = (Material)Resources.Load("Materials/humanLow_up-v2");
        matUPv3 = (Material)Resources.Load("Materials/humanLow_up-v3");*/

        /*modelUPver1.GetComponent<Renderer>().material.SetInt("_Cull", 2); // для избавления просвечивания у моделей - только через шейдер
        modelUPver2.GetComponent<Renderer>().material.SetInt("_Cull", 2);
        modelUPver3.GetComponent<Renderer>().material.SetInt("_Cull", 2);
        modelDOWNver1.GetComponent<Renderer>().material.SetInt("_Cull", 2);
        modelDOWNver1.GetComponent<Renderer>().material.SetInt("_Cull", 2);
        modelHuman.GetComponent<Renderer>().material.SetInt("_Cull", 2);*/

        /*Т*Е*С*Т*О*В*Ы*Й**М*А*С*С*И*В*/
        /*valuesForClothes[0] = new string[] { "up1", "1up_cat1", "2up_cat1", "3up_cat1", "4up_cat1", "5up_cat1" }; // 1 категория верхней одежды
        valuesForClothes[1] = new string[] { "up2", "1up_cat2", "2up_cat2", "3up_cat2", "4up_cat2", "5up_cat2", "6up_cat2", "7up_cat2" };
        valuesForClothes[2] = new string[] { "up3", "1up_cat3", "2up_cat3", "3up_cat3", "4up_cat3", "5up_cat3", "6up_cat3" };
        valuesForClothes[3] = new string[] { "down1", "1down_cat1", "2down_cat1", "3down_cat1", "4down_cat1" }; // 1 категория нижней
        valuesForClothes[4] = new string[] { "down2", "1down_cat2", "2down_cat2", "3down_cat2", "4down_cat2", "5down_cat2", "6down_cat2", "7down_cat2" };*/
        
        /*
        valuesForClothes[0] = new string[] { "Pullover", "Weekday 02007960", "Time of Style 498F005", "Time of Style 498F001", "Time of Style 498F013", "Colin’s CLTTRMPLV026203018-0201" }; // 1 категория (с индексом 0 в массиве) верхней одежды (одно название категории, остальное - названия вещей)
        valuesForClothes[1] = new string[] { "T-shirt", "Fruit of the loom Valueweight 061036041", "United Colors of Benetton 3CNBJ1E46-907", "Piazza Italia 99253-13", "Koton 8YAM14243OK5", "Jack & Jones 5713026396112", "MR520 MR 125 1238 0517", "H&M 0499402" };
        valuesForClothes[2] = new string[] { "Shirt", "Piazza Italia 94997-261", "Piazza Italia 95008-58064", "Levi’s Barstow Western Gray 658160163", "Garcia Jeans L51228-1567", "Colin’s CL1033215GRA", "MR520 MR 123 1330 0817" };
        valuesForClothes[3] = new string[] { "Jeans", "Levi’s 501 Original Fit Stonewash 00501-0114", "MONTANA 1015802 Stone Bleached", "G-Star Raw 3301 Tapered 51003.7209", "Levi’s 504 Regular Straight Avatar Worn" }; // 1 категория нижней
        valuesForClothes[4] = new string[] { "Shorts", "Piazza Italia 95483-56 Verde", "Piazza Italia 95483-62", "PEAK F352165-KHA", "PEAK F352951-BLU", "Weekday 02004472", "Under Armour Ua Launch Sw 7 Print Short 1300057-016", "No Excess NX 858110384 019" };
        */
        // отсчёт длины массива начинается с 1

        textMessage = GameObject.Find("Text Message").GetComponent<Text>();
        
        prevCatUP = GameObject.Find("Text previous category UP").GetComponent<Text>();
        prevUP = GameObject.Find("Text previous UP").GetComponent<Text>();
        prevCatDOWN = GameObject.Find("Text previous category DOWN").GetComponent<Text>();
        prevDOWN = GameObject.Find("Text previous DOWN").GetComponent<Text>();
        nextCatUP = GameObject.Find("Text next category UP").GetComponent<Text>();
        nextUP = GameObject.Find("Text next UP").GetComponent<Text>();
        nextCatDOWN = GameObject.Find("Text next category DOWN").GetComponent<Text>();
        nextDOWN = GameObject.Find("Text next DOWN").GetComponent<Text>();

        CategoryClothesDOWN = GameObject.Find("TextCategoryClothesDOWN").GetComponent<Text>();
        NameClothesDOWN = GameObject.Find("TextNameClothesDOWN").GetComponent<Text>();
        PriceClothesDOWN = GameObject.Find("TextPriceClothesDOWN").GetComponent<Text>();
        SizeAvailabilityClothesDOWN = GameObject.Find("TextSizeAvailabilityClothesDOWN").GetComponent<Text>();
        ClothesDOWNImage = GameObject.Find("ClothesDOWNImage").GetComponent<UnityEngine.UI.Image>();
        BrandClothesDOWN = GameObject.Find("TextBrandClothesDOWN").GetComponent<Text>();

        CategoryClothesUP = GameObject.Find("TextCategoryClothesUP").GetComponent<Text>();
        NameClothesUP = GameObject.Find("TextNameClothesUP").GetComponent<Text>();
        PriceClothesUP = GameObject.Find("TextPriceClothesUP").GetComponent<Text>();
        SizeAvailabilityClothesUP = GameObject.Find("TextSizeAvailabilityClothesUP").GetComponent<Text>();
        ClothesUPImage = GameObject.Find("ClothesUPImage").GetComponent<UnityEngine.UI.Image>();
        BrandClothesUP = GameObject.Find("TextBrandClothesUP").GetComponent<Text>();

        Debug.Log(selectCategoryUP + " selectCatUP," + selectUP + " selectUP," + selectCategoryDOWN + " selectCatDOWN," + selectDOWN + " selectDOWN");
        //StartRandom(); // random одежда при старте приложения
    }

	// Update is called once per frame
	void Update () {

    }

    // отображение инфо о выбранной одежде
    public void ShowInfo()
    {
        transform.localPosition = new Vector2(transform.localPosition.x, 0);
        //ChangeTextureClothesUP(); // необходимо - если пользователь решит изменить размеры на выбранной одежде - для правильного отображения размеров
        //ChangeTextureClothesDOWN();
    }
    public void CloseInfo()
    {
        transform.position = new Vector2(transform.position.x, -1200);
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
                Debug.Log(path.ToString() + " - is connected");
            }
        }
        catch (Exception ex)
        {
           Debug.Log(ex.ToString()); // отображение ошибки (при её наличии)
        }
    }

    public void AfterFirstHideMenu()
    {
        if (launch == 0)
        {
            Connection();

            /*
            // присваивание первоначальных значений после скрытия меню (без учёта рандома) для одежды со сцены по умолчанию - значения из массива
            textUIcatUP.text = valuesForClothes[0][0];
            textUIUP.text = valuesForClothes[0][1];
            textUIcatDOWN.text = valuesForClothes[3][0];
            textUIDOWN.text = valuesForClothes[3][1];
            selectTextureUP.text = "(" + selectUP + "/" + (valuesForClothes[0].Length - 1) + ")";
            selectTextureDOWN.text = "(" + selectDOWN + "/" + (valuesForClothes[3].Length - 1) + ")";
            */

            // создание команд
            // вывод названия определённой категории
            cmd_db = new SqliteCommand("SELECT name FROM category WHERE name = 'Pullover'", con_db); // указание запроса, указание созданного соединения
            rdr = cmd_db.ExecuteReader(); // чтение ответа на запрос
            while (rdr.Read()) // действия во время чтения
            {
                // text.text = rdr[1].ToString(); // [n] - вывод последнего значения из n столбца (от 0)
                textUIcatUP.text = rdr[0].ToString();
            }
            
            // вывод названия бренда и одежды
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE c.name = '02007960'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }

            cmd_db = new SqliteCommand("SELECT name FROM category WHERE name = 'Jeans'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIcatDOWN.text = rdr[0].ToString();
            }
            
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE c.name = '501 Original Fit Stonewash 00501-0114'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIDOWN.text = rdr[0].ToString();
            }

            // подсчёт кол-ва одежды в определённой категории
            cmd_db = new SqliteCommand("SELECT count(*) FROM clothes c INNER JOIN category cat ON c.fk_category = cat.id_category WHERE cat.name = 'Pullover'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                selectTextureUP.text = "(" + selectUP + "/" + rdr[0].ToString() + ")";

                //amountClothesCatUP1 = Convert.ToByte(rdr[0].ToString());
            }
            cmd_db = new SqliteCommand("SELECT count(*) FROM clothes c INNER JOIN category cat ON c.fk_category = cat.id_category WHERE cat.name = 'Jeans'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                selectTextureDOWN.text = "(" + selectDOWN + "/" + rdr[0].ToString() + ")";

                //amountClothesCatDOWN1 = Convert.ToByte(rdr[0].ToString());
            }

            GetSize();

            // отображение подробной инфо о выбранной одежде
            CategoryClothesUP.text = textUIcatUP.text; // чтобы не обращаться к базе - присвоение значения из текста элемента интерфейса, отображающего текущую выбранную категорию верхней одежды
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE c.name = '02007960'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = ""; // если запрос ничего не вернёт - для подстраховки, если вернёт, то значение "" будет перезаписано
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size INNER JOIN clothes c ON cs.id_clothes = c.id_clothes WHERE c.name = '02007960' AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            /*cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 1 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }*/
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v1_1");
            /*cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 1", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }*/

            CategoryClothesDOWN.text = textUIcatDOWN.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE c.name = '501 Original Fit Stonewash 00501-0114'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesDOWN.text = rdr[0].ToString();
                NameClothesDOWN.text = rdr[1].ToString();
            }
            PriceClothesDOWN.text = "";
            SizeAvailabilityClothesDOWN.text = "Для выбранной одежды размер '" + SizeThree + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size INNER JOIN clothes c ON cs.id_clothes = c.id_clothes WHERE c.name = '501 Original Fit Stonewash 00501-0114' AND s.size_number = '" + SizeThree + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesDOWN.text = rdr[0].ToString() + " грн.";
                }
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesDOWN.text = "Ваш размер '" + SizeThree + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesDOWN.text = "Вашего размера '" + SizeThree + "' нет в наличии";
                }
            }
            /*cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 19 AND s.size_number = '" + SizeThree + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesDOWN.text = "Ваш размер '" + SizeThree + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesDOWN.text = "Вашего размера '" + SizeThree + "' нет в наличии";
                }
            }*/
            ClothesDOWNImage.sprite = Resources.Load<Sprite>("Textures/images_down-v1_1");
            /*cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 19", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesDOWN.text = rdr[0].ToString();
            }*/

            // определение кол-ва одежды в остальных категориях - неправильное назначение скриптов к объектам
            /*cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 2", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                amountClothesCatUP2 = Convert.ToByte(rdr[0].ToString());
            }
            cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 3", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                amountClothesCatUP3 = Convert.ToByte(rdr[0].ToString());
            }
            cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 5", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                amountClothesCatDOWN2 = Convert.ToByte(rdr[0].ToString());
            }*/

            // скрытие надписи по центру
            Destroy(textMessage, 4);
            launch++;

            // скрытие надписей в интерфейсе через некоторое время
            Destroy(prevCatUP, 55);
            Destroy(prevUP, 55);
            Destroy(prevCatDOWN, 55);
            Destroy(prevDOWN, 55);
            Destroy(nextCatUP, 55);
            Destroy(nextUP, 55);
            Destroy(nextCatDOWN, 55);
            Destroy(nextDOWN, 55);
        }
        // вместо прописываний значений вручную (вверху) можно обратиться в методы + обращение помогает выводить подробную инфо об одежде по умолчанию (чтобы тоже не прописывать вручную инфо и про него здесь)
        // также вызовы методов полезны - если изменять значения в настройках на одной выбранной одежде - будет отображаться правильная информация
        //ChangeCategoryUP(); // пропадает модель маникена
        //ChangeCategoryDOWN();
        //ChangeTextureClothesUP();
        //Debug.Log(selectUP + " selectUP, " + selectCategoryUP + " selectCatUP"); // происходит возвращение к первой категории и первой одежде в этой категории при закрытии настроек + отображение не верной подробной инфо
        /*ChangeClothesUPLeft(); // имитация кликов для возвращения выбранной одежды
        ChangeClothesUPRight();
        ChangeClothesDOWNLeft();
        ChangeClothesDOWNRight();*/
        //ChangeTextureClothesDOWN();
    }

    public void StableHideMenu() // для избежания возвращения к первым категориям и первой одежде в этих категориях после закрытия меню
    {
        /*ChangeClothesUPLeft();
        ChangeClothesUPRight();
        ChangeClothesDOWNLeft();
        ChangeClothesDOWNRight();*/
    }

    /*public class MyDefaultTrackableEventHandler : DefaultTrackableEventHandler
    {
        Text textMessage;
        void Start()
        {
            textMessage = GameObject.Find("Text Message").GetComponent<Text>();           
        }
        private void OnTrackingFound() // выполнение при нахождении заданного изображения
        {
            textMessage.transform.position = new Vector3(635, 1555, transform.position.z);
        }
        private void OnTrackingLost() // при потери
        {
            textMessage.transform.position = new Vector3(635, 360, transform.position.z);
        }
    }*/

    // Кнопка рандом - разобраться и далее восстановить скрипт с рандомом
    /*******N*E*W**R*A*N*D*O*M*******/ // О Ш И Б К А: не срабатывает перехват значений при выборе категорий и текстуры с помощью рандома и использование в дальнейшем этих значений UI
    public void StartRandom() // random одежды при нажатии на кнопку
    {
        /*Debug.Log(selectCategoryUP + " selectCatUP," + selectUP + " selectUP," + selectCategoryDOWN + " selectCatDOWN," + selectDOWN + " selectDOWN");
        selectCategoryUP = Convert.ToSByte(UnityEngine.Random.Range(1, 4)); // Верхняя одежда - 3 типа
        if (selectCategoryUP == 1)
        {
            selectUP = Convert.ToSByte(UnityEngine.Random.Range(1, valuesForClothes[0].Length)); // 1 тип - 5 видов
        }
        else if (selectCategoryUP == 2)
        {
            selectUP = Convert.ToSByte(UnityEngine.Random.Range(1, valuesForClothes[1].Length)); // 2 тип - 4 вида
        }
        else if (selectCategoryUP == 3)
        {
            selectUP = Convert.ToSByte(UnityEngine.Random.Range(1, valuesForClothes[2].Length)); // 3 тип - 6 видов
        }
        ChangeCategoryUP();
        selectCategoryDOWN = Convert.ToSByte(UnityEngine.Random.Range(1, 3)); // Нижняя часть - 2 типа
        if (selectCategoryDOWN == 1)
        {
            selectDOWN = Convert.ToSByte(UnityEngine.Random.Range(1, valuesForClothes[3].Length)); // 1 тип - 4 вида
        }
        else if (selectCategoryDOWN == 2)
        {
            selectDOWN = Convert.ToSByte(UnityEngine.Random.Range(1, valuesForClothes[4].Length)); // 2 тип - 7 видов
        }
        ChangeCategoryDOWN();
        Debug.Log(selectCategoryUP + " selectCatUP," + selectUP + " selectUP," + selectCategoryDOWN + " selectCatDOWN," + selectDOWN + " selectDOWN");*/
    }

    /********S*E*T**C*O*O*R*D********/
    public void SetStandardCoordHuman()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0); // установка модели тела в начальное положение
        GetComponent<ModelRotator>().RotatorStop(); // вызов метода с другого скрипта для остановки вращения
    }
    public void SetStandardCoordUP()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0); // верхней части тела
        GetComponent<ModelRotator>().RotatorStop();
    }
    public void SetStandardCoordDOWN()
    {
        transform.rotation = Quaternion.Euler(-90, 0, 0);
        GetComponent<ModelRotator>().RotatorStop();
    }

    /**C*H*A*N*G*E**C*A*T*E*G*O*R*Y**/ // for UI - Buttons
    public void ChangeCategoryUPLeft()
    {
        Debug.Log(selectCategoryUP + " selectCatUP," + selectUP + " selectUP," + selectCategoryDOWN + " selectCatDOWN," + selectDOWN + " selectDOWN");
        selectCategoryUP--;
        if (selectCategoryUP < 1)
        {
            selectCategoryUP = 3; // max UP значение
        }
        selectUP = 1; // установка значения для текстуры - при выборе новой категории будет отображена первая текстура из этой категории
        ChangeCategoryUP();
    }
    public void ChangeCategoryUPRight()
    {
        Debug.Log(selectCategoryUP + " selectCatUP," + selectUP + " selectUP," + selectCategoryDOWN + " selectCatDOWN," + selectDOWN + " selectDOWN");
        selectCategoryUP++;
        if (selectCategoryUP > 3) // max UP значение
        {
            selectCategoryUP = 1;
        }
        selectUP = 1;
        ChangeCategoryUP();
    }
    public void ChangeCategoryDOWNLeft()
    {
        Debug.Log(selectCategoryUP + " selectCatUP," + selectUP + " selectUP," + selectCategoryDOWN + " selectCatDOWN," + selectDOWN + " selectDOWN");
        selectCategoryDOWN--;
        if (selectCategoryDOWN < 1)
        {
            selectCategoryDOWN = 2; // max DOWN значение
        }
        selectDOWN = 1;
        ChangeCategoryDOWN();
    }
    public void ChangeCategoryDOWNRight()
    {
        Debug.Log(selectCategoryUP + " selectCatUP," + selectUP + " selectUP," + selectCategoryDOWN + " selectCatDOWN," + selectDOWN + " selectDOWN");
        selectCategoryDOWN++;
        if (selectCategoryDOWN > 2) // max DOWN значение
        {
            selectCategoryDOWN = 1;
        }
        selectDOWN = 1;
        ChangeCategoryDOWN();
    }

    /******F*R*O*M**R*A*N*D*O*M******/
    /*public void RandomParamCategoryUP(sbyte catUP)
    {
        selectCategoryUP = catUP;
        Debug.Log("Значение категории верхней одежды переданное из другого скрипта при рандоме - " + selectCategoryUP);
        ChangeCategoryUP();
    }
    public void RandomParamCategoryDOWN(sbyte catDOWN)
    {
        selectCategoryDOWN = catDOWN;
        Debug.Log("Значение категории нижней одежды переданное из другого скрипта при рандоме - " + selectCategoryDOWN);
        ChangeCategoryDOWN();
    }*/

    /**C*H*A*N*G*E**C*A*T*E*G*O*R*Y**/ // models & UI
    public void ChangeCategoryUP() // изменение модели при смене категории + отображение названия выбранной категории
    {
        Connection();

        if (selectCategoryUP == 1) // UPver1
        {
            modelUPver1.transform.position = new Vector3(transform.position.x, transform.position.y, modelPosition); // отображение (подъём / приближение модели) UPver1
            modelUPver2.transform.position = new Vector3(transform.position.x, transform.position.y, -420); // скрытие (опускание / отдаление моделей) UPver2, UPver3
            modelUPver3.transform.position = new Vector3(transform.position.x, transform.position.y, -420);
            //textUIcatUP.text = valuesForClothes[0][0]; // "up1" - вернуть тестовые значения при тесте рандома

            cmd_db = new SqliteCommand("SELECT name FROM category WHERE id_category = 1", con_db); // указание запроса, указание созданного соединения
            rdr = cmd_db.ExecuteReader(); // чтение ответа на запрос
            while (rdr.Read()) // действия во время чтения
            {
                // text.text = rdr[1].ToString(); // [n] - вывод последнего значения из n столбца (от 0)
                textUIcatUP.text = rdr[0].ToString();
            }
        }
        else if (selectCategoryUP == 2) // UPver2
        {
            modelUPver2.transform.position = new Vector3(transform.position.x, transform.position.y, modelPosition); // отображение UPver2
            modelUPver1.transform.position = new Vector3(transform.position.x, transform.position.y, -420); // скрытие UPver1, UPver3
            modelUPver3.transform.position = new Vector3(transform.position.x, transform.position.y, -420);
            //textUIcatUP.text = valuesForClothes[1][0]; // up2

            cmd_db = new SqliteCommand("SELECT name FROM category WHERE id_category = 2", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIcatUP.text = rdr[0].ToString();
            }
        }
        else if (selectCategoryUP == 3) // UPver3
        {
            modelUPver3.transform.position = new Vector3(transform.position.x, transform.position.y, modelPosition); // отображение UPver3
            modelUPver1.transform.position = new Vector3(transform.position.x, transform.position.y, -420); // скрытие UPver1, UPver2
            modelUPver2.transform.position = new Vector3(transform.position.x, transform.position.y, -420);
            //textUIcatUP.text = valuesForClothes[2][0]; // up3

            cmd_db = new SqliteCommand("SELECT name FROM category WHERE id_category = 3", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIcatUP.text = rdr[0].ToString();
            }
        }
        ChangeTextureClothesUP();
    }
    public void ChangeCategoryDOWN()
    {
        Connection();

        if (selectCategoryDOWN == 1) // DOWNver1
        {
            modelDOWNver1.transform.position = new Vector3(transform.position.x, transform.position.y, modelPosition); // отображение DOWNver1
            modelDOWNver2.transform.position = new Vector3(transform.position.x, transform.position.y, -420); // скрытие DOWNver2
            //textUIcatDOWN.text = valuesForClothes[3][0]; // down1

            cmd_db = new SqliteCommand("SELECT name FROM category WHERE id_category = 4", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIcatDOWN.text = rdr[0].ToString();
            }
        }
        else if (selectCategoryDOWN == 2) // DOWNver2
        {
            modelDOWNver2.transform.position = new Vector3(transform.position.x, transform.position.y, modelPosition); // отображение DOWNver2
            modelDOWNver1.transform.position = new Vector3(transform.position.x, transform.position.y, -420); // скрытие DOWNver1
            //textUIcatDOWN.text = valuesForClothes[4][0]; // down2

            cmd_db = new SqliteCommand("SELECT name FROM category WHERE id_category = 5", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIcatDOWN.text = rdr[0].ToString();
            }
        }
        ChangeTextureClothesDOWN();
    }

    /***C*H*A*N*G*E**C*L*O*T*H*E*S***/ // for UI - Buttons
    public void ChangeClothesUPLeft() // если выбрали верхнюю одежду, которая идёт перед индексом 1 - перебрасывание в конец списка доступной верхней одежды
    {
        Connection();

        Debug.Log("Значение UP - " + selectUP + " -1");
        selectUP--;
        if (selectUP < 1 && selectCategoryUP == 1)
        {
            //selectUP = Convert.ToSByte(valuesForClothes[0].Length - 1); // равно (5) кол-ву текстур для верхней части первой категории

            cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 1", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                selectUP = Convert.ToSByte(rdr[0].ToString());
            }
        }
        else if (selectUP < 1 && selectCategoryUP == 2)
        {
            //selectUP = Convert.ToSByte(valuesForClothes[1].Length - 1);

            cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 2", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                selectUP = Convert.ToSByte(rdr[0].ToString());
            }
        }
        else if (selectUP < 1 && selectCategoryUP == 3)
        {
            //selectUP = Convert.ToSByte(valuesForClothes[2].Length - 1);

            cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 3", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                selectUP = Convert.ToSByte(rdr[0].ToString());
            }
        }
        ChangeTextureClothesUP();
    }
    public void ChangeClothesUPRight()
    {
        Connection();

        byte amountClothesCatUP1 = 0;
        byte amountClothesCatUP2 = 0;
        byte amountClothesCatUP3 = 0;

        cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 1", con_db);
        rdr = cmd_db.ExecuteReader();
        while (rdr.Read())
        {
            amountClothesCatUP1 = Convert.ToByte(rdr[0].ToString());
        }
        cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 2", con_db);
        rdr = cmd_db.ExecuteReader();
        while (rdr.Read())
        {
            amountClothesCatUP2 = Convert.ToByte(rdr[0].ToString());
        }
        cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 3", con_db);
        rdr = cmd_db.ExecuteReader();
        while (rdr.Read())
        {
            amountClothesCatUP3 = Convert.ToByte(rdr[0].ToString());
        }

        Debug.Log("Значение UP - " + selectUP + " +1");
        selectUP++;
        if (selectUP > Convert.ToSByte(/*valuesForClothes[0].Length - 1*/amountClothesCatUP1) && selectCategoryUP == 1)
        {
            selectUP = 1;
        }
        else if (selectUP > Convert.ToSByte(/*valuesForClothes[1].Length - 1*/amountClothesCatUP2) && selectCategoryUP == 2)
        {
            selectUP = 1;
        }
        else if (selectUP > Convert.ToSByte(/*valuesForClothes[2].Length - 1*/amountClothesCatUP3) && selectCategoryUP == 3)
        {
            selectUP = 1;
        }
        ChangeTextureClothesUP();
    }
    public void ChangeClothesDOWNLeft()
    {
        Connection();

        Debug.Log("Значение DOWN - " + selectDOWN + " -1");
        selectDOWN--;
        if (selectDOWN < 1 && selectCategoryDOWN == 1)
        {
            //selectDOWN = Convert.ToSByte(valuesForClothes[3].Length - 1);

            cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 4", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                selectDOWN = Convert.ToSByte(rdr[0].ToString());
            }
        }
        else if (selectDOWN < 1 && selectCategoryDOWN == 2)
        {
            //selectDOWN = Convert.ToSByte(valuesForClothes[4].Length - 1);

            cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 5", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                selectDOWN = Convert.ToSByte(rdr[0].ToString());
            }
        }
        ChangeTextureClothesDOWN();
    }
    public void ChangeClothesDOWNRight()
    {
        Connection();

        byte amountClothesCatDOWN1 = 0;
        byte amountClothesCatDOWN2 = 0;

        cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 4", con_db);
        rdr = cmd_db.ExecuteReader();
        while (rdr.Read())
        {
            amountClothesCatDOWN1 = Convert.ToByte(rdr[0].ToString());
        }
        cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 5", con_db);
        rdr = cmd_db.ExecuteReader();
        while (rdr.Read())
        {
            amountClothesCatDOWN2 = Convert.ToByte(rdr[0].ToString());
        }

        Debug.Log("Значение DOWN - " + selectDOWN + " +1");
        selectDOWN++;
        if (selectDOWN > Convert.ToSByte(/*valuesForClothes[3].Length - 1*/amountClothesCatDOWN1) && selectCategoryDOWN == 1)
        {
            selectDOWN = 1;
        }
        else if (selectDOWN > Convert.ToSByte(/*valuesForClothes[4].Length - 1*/amountClothesCatDOWN2) && selectCategoryDOWN == 2)
        {
            selectDOWN = 1;
        }
        ChangeTextureClothesDOWN();
    }

    /******F*R*O*M**R*A*N*D*O*M******/
    /*public void RandomParamTextureUP(sbyte UP)
    {
        selectUP = UP;
        Debug.Log("Значение вида верхней одежды переданное из другого скрипта при рандоме - " + selectUP);
        ChangeTextureClothesUP();
    }
    public void RandomParamTextureDOWN(sbyte DOWN)
    {
        selectDOWN = DOWN;
        Debug.Log("Значение вида нижней одежды переданное из другого скрипта при рандоме - " + selectDOWN);
        ChangeTextureClothesDOWN();
    }*/

    /***C*H*A*N*G*E**C*L*O*T*H*E*S***/ // textures & UI
    public void ChangeTextureClothesUP()
    {
        /* Обращение к материалу / замена текстуры */
        /*material = GetComponent<Material>().SetTexture*/
        /*modelUPver1.material.SetTexture("humanLow_up-v1_2", sprite.texture);*/

        /*changeTexture = (Texture3D)Resources.Load(""); // изменение текстуры на материале
        modelUPver1.GetComponent<Renderer>().material("human").mainTexture = changeTexture;*/

        //matUPv1.SetTexture("_Albedo", (Texture)Resources.Load("Textures/humanLow_up-v1_1"));

        /*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*/
        /*if (selectCategoryUP == 1 && selectUP <= valuesForClothes[0].Length)
        {
            modelUPver1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v" + selectCategoryUP + "_" + selectUP);
        }
        else if (selectCategoryUP == 1 && selectUP > valuesForClothes[0].Length)
        {
            Debug.Log(selectUP + " имеет большее значение для selCatUP0");
        }*/
        /*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*/
        /*if (selectCategoryUP == 2 && selectUP <= valuesForClothes[1].Length)
        {
            modelUPver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v" + selectCategoryUP + "_" + selectUP);
        }
        else if (selectCategoryUP == 2 && selectUP > valuesForClothes[1].Length)
        {
            Debug.Log(selectUP + " имеет большее значение для selCatUP1");
        }*/
        /*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*/
        /*if (selectCategoryUP == 3 && selectUP <= valuesForClothes[2].Length)
        {
            modelUPver3.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v" + selectCategoryUP + "_" + selectUP);
        }
        else if (selectCategoryUP == 3 && selectUP > valuesForClothes[2].Length)
        {
            Debug.Log(selectUP + " имеет большее значение для selCatUP2");
        }*/
        /*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*/
        /*try
        {
            textUIUP.text = valuesForClothes[selectCategoryUP - 1][selectUP];
        }
        catch
        {
            Debug.Log(selectCategoryUP - 1 + " - массив и размер массива - " + selectUP + ". Ошибка!");
        }
        selectTextureUP.text = "(" + selectUP + "/" + (valuesForClothes[selectCategoryUP - 1].Length - 1) + ")";*/

        Connection();

        byte amountClothesCatUP1 = 0;
        byte amountClothesCatUP2 = 0;
        byte amountClothesCatUP3 = 0;

        cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 1", con_db);
        rdr = cmd_db.ExecuteReader();
        while (rdr.Read())
        {
            amountClothesCatUP1 = Convert.ToByte(rdr[0].ToString());
        }
        cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 2", con_db);
        rdr = cmd_db.ExecuteReader();
        while (rdr.Read())
        {
            amountClothesCatUP2 = Convert.ToByte(rdr[0].ToString());
        }
        cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 3", con_db);
        rdr = cmd_db.ExecuteReader();
        while (rdr.Read())
        {
            amountClothesCatUP3 = Convert.ToByte(rdr[0].ToString());
        }

        GetSize();

        if (selectCategoryUP == 1 && selectUP == 1)
        {
            modelUPver1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v1_1");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 1", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP1 + ")";

            // отображение подробной инфо о выбранной одежде в методе изменения текстур верхней одежды (чтобы не создавать ещё один метод с конструкциями if)
            /* 1 */ CategoryClothesUP.text = textUIcatUP.text; // чтобы не обращаться к базе - присвоение значения из текста элемента интерфейса, отображающего текущую выбранную категорию верхней одежды
            /* 2 */ cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 1", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            /* 3 */ PriceClothesUP.text = ""; // если запрос ничего не вернёт - для подстраховки, если вернёт, то значение "" будет перезаписано
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 1 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
                //else //if (/* запрос не вернулся */)
                /*{
                    
                }*/
            }
            /* 4 */ SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 1 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            //var sprite = Resources.Load<Sprite>("Textures/images_up-v1_1");
            //Debug.Log(sprite); // проверка - если ли спрайт в переменной
            /* 5 */ ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v1_1");
            /* 6 */ cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 1", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        if (selectCategoryUP == 1 && selectUP == 2)
        {
            modelUPver1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v1_2");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 2", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP1 + ")";
            
            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 2", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 2 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 2 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v1_2");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 2", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        if (selectCategoryUP == 1 && selectUP == 3)
        {
            modelUPver1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v1_3");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 3", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP1 + ")";

            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 3", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 3 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 3 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v1_3");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 3", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        if (selectCategoryUP == 1 && selectUP == 4)
        {
            modelUPver1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v1_4");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 4", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP1 + ")";

            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 4", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 4 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 4 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v1_4");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 4", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        if (selectCategoryUP == 1 && selectUP == 5)
        {
            modelUPver1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v1_5");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 5", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP1 + ")";

            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 5", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 5 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 5 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v1_5");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 5", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        /*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*/
        if (selectCategoryUP == 2 && selectUP == 1)
        {
            modelUPver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v2_1");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 6", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP2 + ")";

            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 6", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 6 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 6 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v2_1");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 6", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        if (selectCategoryUP == 2 && selectUP == 2)
        {
            modelUPver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v2_2");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 7", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP2 + ")";

            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 7", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 7 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 7 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v2_2");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 7", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        if (selectCategoryUP == 2 && selectUP == 3)
        {
            modelUPver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v2_3");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 8", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP2 + ")";

            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 8", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 8 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 8 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v2_3");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 8", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        if (selectCategoryUP == 2 && selectUP == 4)
        {
            modelUPver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v2_4");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 9", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP2 + ")";

            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 9", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 9 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 9 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v2_4");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 9", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        if (selectCategoryUP == 2 && selectUP == 5)
        {
            modelUPver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v2_5");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 10", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP2 + ")";

            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 10", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 10 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 10 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v2_5");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 10", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        if (selectCategoryUP == 2 && selectUP == 6)
        {
            modelUPver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v2_6");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 11", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP2 + ")";

            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 11", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 11 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 11 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v2_6");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 11", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        if (selectCategoryUP == 2 && selectUP == 7)
        {
            modelUPver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v2_7");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 12", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP2 + ")";

            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 12", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 12 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 12 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v2_7");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 12", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        /*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*/
        if (selectCategoryUP == 3 && selectUP == 1)
        {
            modelUPver3.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v3_1");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 13", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP3 + ")";

            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 13", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 13 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 13 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v3_1");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 13", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        if (selectCategoryUP == 3 && selectUP == 2)
        {
            modelUPver3.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v3_2");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 14", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP3 + ")";

            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 14", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 14 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 14 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v3_2");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 14", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        if (selectCategoryUP == 3 && selectUP == 3)
        {
            modelUPver3.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v3_3");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 15", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP3 + ")";

            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 15", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 15 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 15 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v3_3");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 15", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        if (selectCategoryUP == 3 && selectUP == 4)
        {
            modelUPver3.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v3_4");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 16", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP3 + ")";

            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 16", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 16 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 16 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v3_4");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 16", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        if (selectCategoryUP == 3 && selectUP == 5)
        {
            modelUPver3.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v3_5");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 17", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP3 + ")";

            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 17", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 17 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 17 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v3_5");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 17", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
        if (selectCategoryUP == 3 && selectUP == 6)
        {
            modelUPver3.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_up-v3_6");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 18", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIUP.text = rdr[0].ToString();
            }
            selectTextureUP.text = "(" + selectUP + "/" + amountClothesCatUP3 + ")";

            CategoryClothesUP.text = textUIcatUP.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 18", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesUP.text = rdr[1].ToString();
            }
            PriceClothesUP.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 18 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesUP.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesUP.text = "Для выбранной одежды размер '" + SizeOne + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 18 AND s.size_number = '" + SizeOne + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesUP.text = "Ваш размер '" + SizeOne + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesUP.text = "Вашего размера '" + SizeOne + "' нет в наличии";
                }
            }
            ClothesUPImage.sprite = Resources.Load<Sprite>("Textures/images_up-v3_6");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 18", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesUP.text = rdr[0].ToString();
            }
        }
    }
    public void ChangeTextureClothesDOWN()
    {
        /*if (selectCategoryDOWN == 1)
        {
            for (int i = 1; i == selectDOWN; i++)
            {
                modelDOWNver1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v1_" + Convert.ToString(i));
                textUIDOWN.text = valuesForClothes[3][i];
            }
        }*/
        /*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*/
        /*if (selectCategoryDOWN == 1 && selectDOWN == 1)
        {
            modelDOWNver1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v1_1");
            textUIDOWN.text = valuesForClothes[3][1]; // 1down_cat1
            selectTextureDOWN.text = "(" + selectDOWN + "/" + (valuesForClothes[3].Length - 1) + ")";
        }
        if (selectCategoryDOWN == 1 && selectDOWN == 2)
        {
            modelDOWNver1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v1_2");
            textUIDOWN.text = valuesForClothes[3][2]; // 2down_cat1
            selectTextureDOWN.text = "(" + selectDOWN + "/" + (valuesForClothes[3].Length - 1) + ")";
        }
        if (selectCategoryDOWN == 1 && selectDOWN == 3)
        {
            modelDOWNver1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v1_3");
            textUIDOWN.text = valuesForClothes[3][3]; // 3down_cat1
            selectTextureDOWN.text = "(" + selectDOWN + "/" + (valuesForClothes[3].Length - 1) + ")";
        }
        if (selectCategoryDOWN == 1 && selectDOWN == 4)
        {
            modelDOWNver1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v1_4");
            textUIDOWN.text = valuesForClothes[3][4]; // 4down_cat1
            selectTextureDOWN.text = "(" + selectDOWN + "/" + (valuesForClothes[3].Length - 1) + ")";
        }*/
        /*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*/
        /*if (selectCategoryDOWN == 2 && selectDOWN == 1)
        {
            modelDOWNver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v2_1");
            textUIDOWN.text = valuesForClothes[4][1]; // 1down_cat2
            selectTextureDOWN.text = "(" + selectDOWN + "/" + (valuesForClothes[4].Length - 1) + ")";
        }
        if (selectCategoryDOWN == 2 && selectDOWN == 2)
        {
            modelDOWNver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v2_2");
            textUIDOWN.text = valuesForClothes[4][2]; // 2down_cat2
            selectTextureDOWN.text = "(" + selectDOWN + "/" + (valuesForClothes[4].Length - 1) + ")";
        }
        if (selectCategoryDOWN == 2 && selectDOWN == 3)
        {
            modelDOWNver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v2_3");
            textUIDOWN.text = valuesForClothes[4][3]; // 3down_cat2
            selectTextureDOWN.text = "(" + selectDOWN + "/" + (valuesForClothes[4].Length - 1) + ")";
        }
        if (selectCategoryDOWN == 2 && selectDOWN == 4)
        {
            modelDOWNver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v2_4");
            textUIDOWN.text = valuesForClothes[4][4]; // 4down_cat2
            selectTextureDOWN.text = "(" + selectDOWN + "/" + (valuesForClothes[4].Length - 1) + ")";
        }
        if (selectCategoryDOWN == 2 && selectDOWN == 5)
        {
            modelDOWNver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v2_5");
            textUIDOWN.text = valuesForClothes[4][5]; // 5down_cat2
            selectTextureDOWN.text = "(" + selectDOWN + "/" + (valuesForClothes[4].Length - 1) + ")";
        }
        if (selectCategoryDOWN == 2 && selectDOWN == 6)
        {
            modelDOWNver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v2_6");
            textUIDOWN.text = valuesForClothes[4][6]; // 6down_cat2
            selectTextureDOWN.text = "(" + selectDOWN + "/" + (valuesForClothes[4].Length - 1) + ")";
        }
        if (selectCategoryDOWN == 2 && selectDOWN == 7)
        {
            modelDOWNver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v2_7");
            textUIDOWN.text = valuesForClothes[4][7]; // 7down_cat2
            selectTextureDOWN.text = "(" + selectDOWN + "/" + (valuesForClothes[4].Length - 1) + ")";
        }*/
        /*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*/

        Connection();

        byte amountClothesCatDOWN1 = 0;
        byte amountClothesCatDOWN2 = 0;

        cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 4", con_db);
        rdr = cmd_db.ExecuteReader();
        while (rdr.Read())
        {
            amountClothesCatDOWN1 = Convert.ToByte(rdr[0].ToString());
        }
        cmd_db = new SqliteCommand("SELECT count(*) FROM clothes WHERE fk_category = 5", con_db);
        rdr = cmd_db.ExecuteReader();
        while (rdr.Read())
        {
            amountClothesCatDOWN2 = Convert.ToByte(rdr[0].ToString());
        }

        GetSize();

        if (selectCategoryDOWN == 1 && selectDOWN == 1)
        {
            modelDOWNver1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v1_1");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 19", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIDOWN.text = rdr[0].ToString();
            }
            selectTextureDOWN.text = "(" + selectDOWN + "/" + amountClothesCatDOWN1 + ")";

            CategoryClothesDOWN.text = textUIcatDOWN.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 19", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesDOWN.text = rdr[1].ToString();
            }
            PriceClothesDOWN.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 19 AND s.size_number = '" + SizeThree + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesDOWN.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesDOWN.text = "Для выбранной одежды размер '" + SizeThree + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 19 AND s.size_number = '" + SizeThree + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesDOWN.text = "Ваш размер '" + SizeThree + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesDOWN.text = "Вашего размера '" + SizeThree + "' нет в наличии";
                }
            }
            ClothesDOWNImage.sprite = Resources.Load<Sprite>("Textures/images_down-v1_1");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 19", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesDOWN.text = rdr[0].ToString();
            }
        }
        if (selectCategoryDOWN == 1 && selectDOWN == 2)
        {
            modelDOWNver1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v1_2");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 20", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIDOWN.text = rdr[0].ToString();
            }
            selectTextureDOWN.text = "(" + selectDOWN + "/" + amountClothesCatDOWN1 + ")";

            CategoryClothesDOWN.text = textUIcatDOWN.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 20", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesDOWN.text = rdr[1].ToString();
            }
            PriceClothesDOWN.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 20 AND s.size_number = '" + SizeThree + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesDOWN.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesDOWN.text = "Для выбранной одежды размер '" + SizeThree + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 20 AND s.size_number = '" + SizeThree + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesDOWN.text = "Ваш размер '" + SizeThree + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesDOWN.text = "Вашего размера '" + SizeThree + "' нет в наличии";
                }
            }
            ClothesDOWNImage.sprite = Resources.Load<Sprite>("Textures/images_down-v1_2");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 20", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesDOWN.text = rdr[0].ToString();
            }
        }
        if (selectCategoryDOWN == 1 && selectDOWN == 3)
        {
            modelDOWNver1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v1_3");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 21", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIDOWN.text = rdr[0].ToString();
            }
            selectTextureDOWN.text = "(" + selectDOWN + "/" + amountClothesCatDOWN1 + ")";

            CategoryClothesDOWN.text = textUIcatDOWN.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 21", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesDOWN.text = rdr[1].ToString();
            }
            PriceClothesDOWN.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 21 AND s.size_number = '" + SizeThree + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesDOWN.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesDOWN.text = "Для выбранной одежды размер '" + SizeThree + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 21 AND s.size_number = '" + SizeThree + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesDOWN.text = "Ваш размер '" + SizeThree + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesDOWN.text = "Вашего размера '" + SizeThree + "' нет в наличии";
                }
            }
            ClothesDOWNImage.sprite = Resources.Load<Sprite>("Textures/images_down-v1_3");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 21", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesDOWN.text = rdr[0].ToString();
            }
        }
        if (selectCategoryDOWN == 1 && selectDOWN == 4)
        {
            modelDOWNver1.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v1_4");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 22", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIDOWN.text = rdr[0].ToString();
            }
            selectTextureDOWN.text = "(" + selectDOWN + "/" + amountClothesCatDOWN1 + ")";

            CategoryClothesDOWN.text = textUIcatDOWN.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 22", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesDOWN.text = rdr[1].ToString();
            }
            PriceClothesDOWN.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 22 AND s.size_number = '" + SizeThree + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesDOWN.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesDOWN.text = "Для выбранной одежды размер '" + SizeThree + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 22 AND s.size_number = '" + SizeThree + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesDOWN.text = "Ваш размер '" + SizeThree + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesDOWN.text = "Вашего размера '" + SizeThree + "' нет в наличии";
                }
            }
            ClothesDOWNImage.sprite = Resources.Load<Sprite>("Textures/images_down-v1_4");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 22", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesDOWN.text = rdr[0].ToString();
            }
        }
        /*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*/
        if (selectCategoryDOWN == 2 && selectDOWN == 1)
        {
            modelDOWNver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v2_1");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 23", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIDOWN.text = rdr[0].ToString();
            }
            selectTextureDOWN.text = "(" + selectDOWN + "/" + amountClothesCatDOWN2 + ")";

            CategoryClothesDOWN.text = textUIcatDOWN.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 23", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesDOWN.text = rdr[1].ToString();
            }
            PriceClothesDOWN.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 23 AND s.size_number = '" + SizeTwo + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesDOWN.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesDOWN.text = "Для выбранной одежды размер '" + SizeTwo + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 23 AND s.size_number = '" + SizeTwo + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesDOWN.text = "Ваш размер '" + SizeTwo + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesDOWN.text = "Вашего размера '" + SizeTwo + "' нет в наличии";
                }
            }
            ClothesDOWNImage.sprite = Resources.Load<Sprite>("Textures/images_down-v2_1");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 23", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesDOWN.text = rdr[0].ToString();
            }
        }
        if (selectCategoryDOWN == 2 && selectDOWN == 2)
        {
            modelDOWNver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v2_2");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 24", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIDOWN.text = rdr[0].ToString();
            }
            selectTextureDOWN.text = "(" + selectDOWN + "/" + amountClothesCatDOWN2 + ")";

            CategoryClothesDOWN.text = textUIcatDOWN.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 24", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesDOWN.text = rdr[1].ToString();
            }
            PriceClothesDOWN.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 24 AND s.size_number = '" + SizeTwo + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesDOWN.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesDOWN.text = "Для выбранной одежды размер '" + SizeTwo + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 24 AND s.size_number = '" + SizeTwo + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesDOWN.text = "Ваш размер '" + SizeTwo + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesDOWN.text = "Вашего размера '" + SizeTwo + "' нет в наличии";
                }
            }
            ClothesDOWNImage.sprite = Resources.Load<Sprite>("Textures/images_down-v2_2");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 24", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesDOWN.text = rdr[0].ToString();
            }
        }
        if (selectCategoryDOWN == 2 && selectDOWN == 3)
        {
            modelDOWNver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v2_3");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 25", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIDOWN.text = rdr[0].ToString();
            }
            selectTextureDOWN.text = "(" + selectDOWN + "/" + amountClothesCatDOWN2 + ")";

            CategoryClothesDOWN.text = textUIcatDOWN.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 25", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesDOWN.text = rdr[1].ToString();
            }
            PriceClothesDOWN.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 25 AND s.size_number = '" + SizeTwo + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesDOWN.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesDOWN.text = "Для выбранной одежды размер '" + SizeTwo + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 25 AND s.size_number = '" + SizeTwo + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesDOWN.text = "Ваш размер '" + SizeTwo + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesDOWN.text = "Вашего размера '" + SizeTwo + "' нет в наличии";
                }
            }
            ClothesDOWNImage.sprite = Resources.Load<Sprite>("Textures/images_down-v2_3");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 25", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesDOWN.text = rdr[0].ToString();
            }
        }
        if (selectCategoryDOWN == 2 && selectDOWN == 4)
        {
            modelDOWNver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v2_4");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 26", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIDOWN.text = rdr[0].ToString();
            }
            selectTextureDOWN.text = "(" + selectDOWN + "/" + amountClothesCatDOWN2 + ")";

            CategoryClothesDOWN.text = textUIcatDOWN.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 26", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesDOWN.text = rdr[1].ToString();
            }
            PriceClothesDOWN.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 26 AND s.size_number = '" + SizeTwo + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesDOWN.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesDOWN.text = "Для выбранной одежды размер '" + SizeTwo + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 26 AND s.size_number = '" + SizeTwo + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesDOWN.text = "Ваш размер '" + SizeTwo + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesDOWN.text = "Вашего размера '" + SizeTwo + "' нет в наличии";
                }
            }
            ClothesDOWNImage.sprite = Resources.Load<Sprite>("Textures/images_down-v2_4");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 26", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesDOWN.text = rdr[0].ToString();
            }
        }
        if (selectCategoryDOWN == 2 && selectDOWN == 5)
        {
            modelDOWNver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v2_5");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 27", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIDOWN.text = rdr[0].ToString();
            }
            selectTextureDOWN.text = "(" + selectDOWN + "/" + amountClothesCatDOWN2 + ")";

            CategoryClothesDOWN.text = textUIcatDOWN.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 27", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesDOWN.text = rdr[1].ToString();
            }
            PriceClothesDOWN.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 27 AND s.size_number = '" + SizeTwo + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesDOWN.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesDOWN.text = "Для выбранной одежды размер '" + SizeTwo + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 27 AND s.size_number = '" + SizeTwo + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesDOWN.text = "Ваш размер '" + SizeTwo + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesDOWN.text = "Вашего размера '" + SizeTwo + "' нет в наличии";
                }
            }
            ClothesDOWNImage.sprite = Resources.Load<Sprite>("Textures/images_down-v2_5");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 27", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesDOWN.text = rdr[0].ToString();
            }
        }
        if (selectCategoryDOWN == 2 && selectDOWN == 6)
        {
            modelDOWNver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v2_6");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 28", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIDOWN.text = rdr[0].ToString();
            }
            selectTextureDOWN.text = "(" + selectDOWN + "/" + amountClothesCatDOWN2 + ")";

            CategoryClothesDOWN.text = textUIcatDOWN.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 28", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesDOWN.text = rdr[1].ToString();
            }
            PriceClothesDOWN.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 28 AND s.size_number = '" + SizeTwo + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesDOWN.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesDOWN.text = "Для выбранной одежды размер '" + SizeTwo + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 28 AND s.size_number = '" + SizeTwo + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesDOWN.text = "Ваш размер '" + SizeTwo + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesDOWN.text = "Вашего размера '" + SizeTwo + "' нет в наличии";
                }
            }
            ClothesDOWNImage.sprite = Resources.Load<Sprite>("Textures/images_down-v2_6");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 28", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesDOWN.text = rdr[0].ToString();
            }
        }
        if (selectCategoryDOWN == 2 && selectDOWN == 7)
        {
            modelDOWNver2.GetComponent<Renderer>().material.mainTexture = (Texture)Resources.Load("Textures/humanLow_down-v2_7");
            cmd_db = new SqliteCommand("SELECT b.name || ' ' || c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 29", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                textUIDOWN.text = rdr[0].ToString();
            }
            selectTextureDOWN.text = "(" + selectDOWN + "/" + amountClothesCatDOWN2 + ")";

            CategoryClothesDOWN.text = textUIcatDOWN.text;
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 29", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                NameClothesDOWN.text = rdr[1].ToString();
            }
            PriceClothesDOWN.text = "";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 29 AND s.size_number = '" + SizeTwo + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if ("" != rdr[0].ToString())
                {
                    PriceClothesDOWN.text = rdr[0].ToString() + " грн.";
                }
            }
            SizeAvailabilityClothesDOWN.text = "Для выбранной одежды размер '" + SizeTwo + "' не предусмотрен";
            cmd_db = new SqliteCommand("SELECT cs.price, cs.availability FROM clothes_size cs INNER JOIN size s ON cs.id_size = s.id_size WHERE cs.id_clothes = 29 AND s.size_number = '" + SizeTwo + "'", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr[1].ToString() == "1")
                {
                    SizeAvailabilityClothesDOWN.text = "Ваш размер '" + SizeTwo + "' есть в наличии";
                }
                else if ("0" == rdr[1].ToString())
                {
                    SizeAvailabilityClothesDOWN.text = "Вашего размера '" + SizeTwo + "' нет в наличии";
                }
            }
            ClothesDOWNImage.sprite = Resources.Load<Sprite>("Textures/images_down-v2_7");
            cmd_db = new SqliteCommand("SELECT b.name, c.name FROM clothes c INNER JOIN brand b ON c.fk_brand = b.id_brand WHERE id_clothes = 29", con_db);
            rdr = cmd_db.ExecuteReader();
            while (rdr.Read())
            {
                BrandClothesDOWN.text = rdr[0].ToString();
            }
        }
    }

    public void GetSize() // получаем размеры с текстовых полей (чтобы не передавать их из другого скрипта)
    {
        SizeOne = Convert.ToString(GameObject.Find("SizeOne").GetComponent<Text>().text);
        SizeTwo = Convert.ToString(GameObject.Find("SizeTwo").GetComponent<Text>().text);
        SizeThree = Convert.ToString(GameObject.Find("SizeThree").GetComponent<Text>().text);
    }
}
