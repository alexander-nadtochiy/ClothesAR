using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomClothes : MonoBehaviour {

    /*
    public GameObject UPver1 = GameObject.Find("UPver1");
    public GameObject UPver2 = GameObject.Find("UPver2");
    public GameObject UPver3 = GameObject.Find("UPver3");
    public GameObject DOWNver1 = GameObject.Find("DOWNver1");
    public GameObject DOWNver2 = GameObject.Find("DOWNver2");	
    */
    /*GameObject modelUPver1;
    GameObject modelUPver2;
    GameObject modelUPver3;
    GameObject modelDOWNver1;
    GameObject modelDOWNver2;
    Text textUIcatUP;
    Text textUIcatDOWN;
    Text textUIUP;
    Text textUIDOWN;*/

    // Use this for initialization
    void Start () {
        /*modelUPver1 = GameObject.Find("UPver1");
        modelUPver2 = GameObject.Find("UPver2");
        modelUPver3 = GameObject.Find("UPver3");
        modelDOWNver1 = GameObject.Find("DOWNver1");
        modelDOWNver2 = GameObject.Find("DOWNver2");
        textUIcatUP = GameObject.Find("Text CategoriesUP").GetComponent<Text>();
        textUIUP = GameObject.Find("Text DescriptionUP").GetComponent<Text>();
        textUIcatDOWN = GameObject.Find("Text CategoriesDOWN").GetComponent<Text>();
        textUIDOWN = GameObject.Find("Text DescriptionDOWN").GetComponent<Text>();*/
        
        /*
        GameObject UPver1 = GameObject.Find("UPver1");
        if (UPver1)
        {
            Debug.Log(UPver1.name);
        }
        else
        {
            Debug.Log("No game object called wibble found");
        }
        */

        //StartRandom(); // random одежда при старте игры
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void StartRandom() // random одежды при нажатии на кнопку
    {
        /*
        byte change = 0;
        change = Convert.ToByte(UnityEngine.Random.Range(0, 2)); // рандом - 1 или 2
        if (change == 1) // если = 1 - удаляем объект со сцены - DOWNver1 был отвязан от кнопки Random - метод StartRandom
        {
            // Destroy(gameObject, 1); // время до удаления объекта
            transform.position = new Vector3(transform.position.x, -40, transform.position.z);
        }
        */
        /********************************/
        /*sbyte change = 1;
        change = Convert.ToSByte(UnityEngine.Random.Range(1, 4)); // Верхняя одежда - 3 типа (от 0/1 до 4, т.е. значения 0/1, 2 или 3)
        GetComponent<ChangeClothes>().RandomParamCategoryUP(change);*/ // передача в скрипт ChangeClothes параметра о выбранной рандомно одежде (в данном случае передача типа верхней одежды)
        //GetComponent<ChangeClothes>().selectCategoryUP = change;
        /*if (change == 1)
        {*/
            /*modelUPver1.transform.position = new Vector3(transform.position.x, 2.62f, transform.position.z);
            modelUPver2.transform.position = new Vector3(transform.position.x, -40, transform.position.z);
            modelUPver3.transform.position = new Vector3(transform.position.x, -40, transform.position.z);
            textUIcatUP.text = "up1";*/
            /*change = Convert.ToSByte(UnityEngine.Random.Range(1, 6));*/ // 1 тип - 5 видов
            /*if (change == 1)
            {

                textUIUP.text = "1up_cat1";
            }
            else if (change == 2)
            {

                textUIUP.text = "2up_cat1";
            }
            else if (change == 3)
            {

                textUIUP.text = "3up_cat1";
            }
            else if (change == 4)
            {

                textUIUP.text = "4up_cat1";
            }
            else if (change == 5)
            {

                textUIUP.text = "5up_cat1";
            }*/
            /*GetComponent<ChangeClothes>().RandomParamTextureUP(change);*/
            //GetComponent<ChangeClothes>().selectUP = change;
        /*}
        else if (change == 2)
        {*/
            /*modelUPver1.transform.position = new Vector3(transform.position.x, -40, transform.position.z);
            modelUPver2.transform.position = new Vector3(transform.position.x, 2.62f, transform.position.z);
            modelUPver3.transform.position = new Vector3(transform.position.x, -40, transform.position.z);
            textUIcatUP.text = "up2";*/
            /*change = Convert.ToSByte(UnityEngine.Random.Range(1, 5));*/ // 2 тип - 4 вида
            /*if (change == 1)
            {

                textUIUP.text = "1up_cat2";
            }
            else if (change == 2)
            {

                textUIUP.text = "2up_cat2";
            }
            else if (change == 3)
            {

                textUIUP.text = "3up_cat2";
            }
            else if (change == 4)
            {

                textUIUP.text = "4up_cat2";
            }*/
            /*GetComponent<ChangeClothes>().RandomParamTextureUP(change);*/
            //GetComponent<ChangeClothes>().selectUP = change;
        /*}
        else if (change == 3)
        {*/
            /*modelUPver1.transform.position = new Vector3(transform.position.x, -40, transform.position.z);
            modelUPver2.transform.position = new Vector3(transform.position.x, -40, transform.position.z);
            modelUPver3.transform.position = new Vector3(transform.position.x, 2.62f, transform.position.z);
            textUIcatUP.text = "up3";*/
            /*change = Convert.ToSByte(UnityEngine.Random.Range(1, 7));*/ // 3 тип - 6 видов
            /*if (change == 1)
            {

                textUIUP.text = "1up_cat3";
            }
            else if (change == 2)
            {

                textUIUP.text = "2up_cat3";
            }
            else if (change == 3)
            {

                textUIUP.text = "3up_cat3";
            }
            else if (change == 4)
            {

                textUIUP.text = "4up_cat3";
            }
            else if (change == 5)
            {

                textUIUP.text = "5up_cat3";
            }
            else if (change == 6)
            {

                textUIUP.text = "6up_cat3";
            }*/
            /*GetComponent<ChangeClothes>().RandomParamTextureUP(change);*/
            //GetComponent<ChangeClothes>().selectUP = change;
        /*}*/
        /*else if (change == 4)
        {
            Debug.Log("Ошибка в рандоме");
        }*/
        /*Debug.Log("Random - верхняя часть тела сгенерирована");

        change = Convert.ToSByte(UnityEngine.Random.Range(1, 3));*/ // Нижняя часть - 2 типа
        /*GetComponent<ChangeClothes>().RandomParamCategoryDOWN(change);*/
        //GetComponent<ChangeClothes>().selectCategoryDOWN = change;
        /*if (change == 1)
        {*/
            /*modelDOWNver1.transform.position = new Vector3(transform.position.x, 2.62f, transform.position.z);
            modelDOWNver2.transform.position = new Vector3(transform.position.x, -40, transform.position.z);
            textUIcatDOWN.text = "down1";*/
            /*change = Convert.ToSByte(UnityEngine.Random.Range(1, 5));*/ // 1 тип - 4 вида
            /*if (change == 1)
            {

                textUIDOWN.text = "1down_cat1";
            }
            else if (change == 2)
            {

                textUIDOWN.text = "2down_cat1";
            }
            else if (change == 3)
            {

                textUIDOWN.text = "3down_cat1";
            }
            else if (change == 4)
            {

                textUIDOWN.text = "4down_cat1";
            }*/
            /*GetComponent<ChangeClothes>().RandomParamTextureDOWN(change);*/
            //GetComponent<ChangeClothes>().selectDOWN = change;
        /*}
        else if (change == 2)
        {*/
            /*modelDOWNver1.transform.position = new Vector3(transform.position.x, -40, transform.position.z);
            modelDOWNver2.transform.position = new Vector3(transform.position.x, 2.62f, transform.position.z);
            textUIcatDOWN.text = "down2";*/
            /*change = Convert.ToSByte(UnityEngine.Random.Range(1, 8));*/ // 2 тип - 7 видов
            /*if (change == 1)
            {

                textUIDOWN.text = "1down_cat2";
            }
            else if (change == 2)
            {

                textUIDOWN.text = "2down_cat2";
            }
            else if (change == 3)
            {

                textUIDOWN.text = "3down_cat2";
            }
            else if (change == 4)
            {

                textUIDOWN.text = "4down_cat2";
            }
            else if (change == 5)
            {

                textUIDOWN.text = "5down_cat2";
            }
            else if (change == 6)
            {

                textUIDOWN.text = "6down_cat2";
            }
            else if (change == 7)
            {

                textUIDOWN.text = "7down_cat2";
            }*/
            /*GetComponent<ChangeClothes>().RandomParamTextureDOWN(change);*/
            //GetComponent<ChangeClothes>().selectDOWN = change;
        /*}*/
        /*Debug.Log("Random - нижняя часть тела сгенерирована");*/
        /*
        if (transform.name == "DOWNver1" || gameObject.name == "DOWNver2")
        {

        }
        */
    }
}
