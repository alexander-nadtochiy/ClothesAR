using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BodyParameters : MonoBehaviour {

    // Use this for initialization
    GameObject DirectionalLight;
    GameObject LampOne;
    GameObject LampTwo;
    GameObject LampThree;
    GameObject ToggleLighting;
    GameObject TogglePanelTransparency;
    GameObject PanelBackground;
    GameObject TextSize;
    GameObject PanelMenu;

    Text SizeOne;
    Text SizeTwo;
    Text SizeThree;

    //Text InputFieldOne;
    //Text InputFieldTwo;
    //GameObject InputFieldThree;
    InputField InputFieldOne;
    InputField InputFieldTwo;
    InputField InputFieldThree;

    float height = 172;
    float chestGirth = 89; // грудь
    float waistGirth = 76; // талия
    byte numberSizeChestGirth = 0;
    byte numberSizeWaistGirthUP = 0;
    byte chooseSizeUP;

    GameObject humanModelsLow;
    GameObject modelHuman;
    GameObject modelUPver1;
    GameObject modelUPver2;
    GameObject modelUPver3;
    GameObject modelDOWNver1;
    GameObject modelDOWNver2;

    void Start () {
        DirectionalLight = GameObject.Find("Directional Light");
        LampOne = GameObject.Find("LampOne");
        LampTwo = GameObject.Find("LampTwo");
        LampThree = GameObject.Find("LampThree");
        ToggleLighting = GameObject.Find("ToggleLighting");
        TogglePanelTransparency = GameObject.Find("TogglePanelTransparency");
        PanelBackground = GameObject.Find("PanelBackground");
        //InputFieldOne = GameObject.Find("InputFieldOne").GetComponent<Text>();
        //InputFieldTwo = GameObject.Find("InputFieldTwo").GetComponent<Text>();
        //InputFieldThree = GameObject.Find("InputFieldThree");
        InputFieldOne = GameObject.Find("InputFieldOne").GetComponent<InputField>();
        InputFieldTwo = GameObject.Find("InputFieldTwo").GetComponent<InputField>();
        InputFieldThree = GameObject.Find("InputFieldThree").GetComponent<InputField>();
        humanModelsLow = GameObject.Find("humanModelsLow");
        modelHuman = GameObject.Find("Human");
        modelUPver1 = GameObject.Find("UPver1");
        modelUPver2 = GameObject.Find("UPver2");
        modelUPver3 = GameObject.Find("UPver3");
        modelDOWNver1 = GameObject.Find("DOWNver1");
        modelDOWNver2 = GameObject.Find("DOWNver2");
        TextSize = GameObject.Find("TextChooseSize");
        SizeOne = GameObject.Find("SizeOne").GetComponent<Text>();
        SizeTwo = GameObject.Find("SizeTwo").GetComponent<Text>();
        SizeThree = GameObject.Find("SizeThree").GetComponent<Text>();
        PanelMenu = GameObject.Find("PanelMenu");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HideMenu()
    {
        //height = Convert.ToInt32(InputFieldOne.text);
        //chestGirth = Convert.ToInt32(InputFieldTwo.GetComponent<Text>().text);
        //waistGirth = Convert.ToInt32(InputFieldThree.GetComponent<Text>().text);
        height = Convert.ToInt32(InputFieldOne.text);
        chestGirth = Convert.ToInt32(InputFieldTwo.text);
        waistGirth = Convert.ToInt32(InputFieldThree.text);

        DeterminationClothingSize();

        ChangeHumanParameters();

        PanelMenu.transform.position = new Vector2(PanelMenu.transform.position.x, 1019);
        GetComponent<ChangeClothes>().AfterFirstHideMenu();
        ChangeLighting();
        ChangePanelTransparency();
    }

    public void DeterminationClothingSize()
    {
        /* Для верхней одежды */
        /* Опредение международного размера по обхвату груди */
        if (chestGirth <= 89 && chestGirth >= 86)
        {
            numberSizeChestGirth = 1;
            // XS
        }
        else if (chestGirth <= 93 && chestGirth >= 90)
        {
            numberSizeChestGirth = 2;
            // S
        }
        else if (chestGirth <= 97 && chestGirth >= 94)
        {
            numberSizeChestGirth = 3;
            // M
        }
        else if (chestGirth <= 101 && chestGirth >= 98)
        {
            numberSizeChestGirth = 4;
            // L
        }
        else if (chestGirth <= 105 && chestGirth >= 102)
        {
            numberSizeChestGirth = 5;
            // XL
        }
        else if (chestGirth <= 109 && chestGirth >= 106)
        {
            numberSizeChestGirth = 6;
            // XXL
        }
        else if (chestGirth <= 113 && chestGirth >= 110)
        {
            numberSizeChestGirth = 7;
            // 3XL
        }
        /* В случае, если нет подходящих размеров - берём по ближайшим */
        else if (chestGirth < 86)
        {
            numberSizeChestGirth = 1;
            chestGirth = 85; // установка значения из диапазона (разница в единицу - чтобы было ясно, что пределы были нарушены)
            InputFieldTwo.GetComponent<InputField>().text = Convert.ToString(chestGirth); // отображение этого значения в поле
            // XS
        }
        else if (chestGirth > 113)
        {
            numberSizeChestGirth = 7;
            chestGirth = 114;
            InputFieldTwo.GetComponent<InputField>().text = Convert.ToString(chestGirth);
            // 3XL
        }

        /* Определение международного размера по обхвату талии */
        if (waistGirth <= 77 && waistGirth >= 74)
        {
            numberSizeWaistGirthUP = 1;
            // XS
        }
        else if (waistGirth <= 81 && waistGirth >= 78)
        {
            numberSizeWaistGirthUP = 2;
            // S
        }
        else if (waistGirth <= 85 && waistGirth >= 82)
        {
            numberSizeWaistGirthUP = 3;
            // M
        }
        else if (waistGirth <= 89 && waistGirth >= 86)
        {
            numberSizeWaistGirthUP = 4;
            // L
        }
        else if (waistGirth <= 93 && waistGirth >= 92)
        {
            numberSizeWaistGirthUP = 5;
            // XL
        }
        else if (waistGirth <= 99 && waistGirth >= 96)
        {
            numberSizeWaistGirthUP = 6;
            // XXL
        }
        else if (waistGirth <= 109 && waistGirth >= 106)
        {
            numberSizeWaistGirthUP = 7;
            // 3XL
        }
        /* В случае, если нет подходящих размеров - берём по ближайшим */
        else if (waistGirth < 74)
        {
            numberSizeWaistGirthUP = 1;
            waistGirth = 73;
            InputFieldThree.GetComponent<InputField>().text = Convert.ToString(waistGirth);
            // XS
        }
        else if (waistGirth > 109)
        {
            numberSizeWaistGirthUP = 7;
            waistGirth = 110;
            InputFieldThree.GetComponent<InputField>().text = Convert.ToString(waistGirth);
            // 3XL
        }

        if ((numberSizeWaistGirthUP != numberSizeChestGirth) && (numberSizeChestGirth - numberSizeWaistGirthUP == 1 || numberSizeWaistGirthUP - numberSizeChestGirth == 1))
        {
            chooseSizeUP = (numberSizeChestGirth > numberSizeWaistGirthUP) ? numberSizeChestGirth : numberSizeWaistGirthUP;
            Debug.Log(0);
        }
        else if (numberSizeChestGirth - numberSizeWaistGirthUP == 2 || numberSizeWaistGirthUP - numberSizeChestGirth == 2)
        {
            if (numberSizeChestGirth - numberSizeWaistGirthUP == 2)
            {
                chooseSizeUP = --numberSizeChestGirth;
                Debug.Log(1);
            }
            else
            {
                chooseSizeUP = --numberSizeWaistGirthUP;
                Debug.Log(2);
            }
        }
        else
        {
            chooseSizeUP = numberSizeChestGirth; // если размеры совпали / при большой разнице - обхват груди в приоритете
            Debug.Log(3);
        }

        /* Итоговый выбор размера для верхней одежды */
        if (chooseSizeUP == 1)
        {
            SizeOne.text = "XS";
            // XS
        }
        else if (chooseSizeUP == 2)
        {
            SizeOne.text = "S";
            // S
        }
        else if (chooseSizeUP == 3)
        {
            SizeOne.text = "M";
            // M
        }
        else if (chooseSizeUP == 4)
        {
            SizeOne.text = "L";
            // L
        }
        else if (chooseSizeUP == 5)
        {
            SizeOne.text = "XL";
            // XL
        }
        else if (chooseSizeUP == 6)
        {
            SizeOne.text = "XXL";
            // XXL
        }
        else if (chooseSizeUP == 7)
        {
            SizeOne.text = "3XL";
            // 3XL
        }

        /* Для нижней одежды */
        /* Опредение размера в дюймах по обхвату талии */
        /* Для шорт достаточно одного размера */
        if (waistGirth <= 74 && waistGirth >= 72)
        {
            SizeTwo.text = "28";
            // 28
        }
        else if (waistGirth <= 77 && waistGirth >= 75)
        {
            SizeTwo.text = "29";
            // 29
        }
        else if (waistGirth <= 79 && waistGirth >= 78)
        {
            SizeTwo.text = "30";
            // 30
        }
        else if (waistGirth <= 82 && waistGirth >= 80)
        {
            SizeTwo.text = "31";
            // 31
        }
        else if (waistGirth <= 84 && waistGirth >= 83)
        {
            SizeTwo.text = "32";
            // 32
        }
        else if (waistGirth <= 87 && waistGirth >= 85)
        {
            SizeTwo.text = "33";
            // 33
        }
        else if (waistGirth <= 92 && waistGirth >= 88)
        {
            SizeTwo.text = "34";
            // 34
        }
        else if (waistGirth <= 97 && waistGirth >= 93)
        {
            SizeTwo.text = "36";
            // 36
        }
        else if (waistGirth <= 102 && waistGirth >= 98)
        {
            SizeTwo.text = "38";
            // 38
        }
        else if (waistGirth <= 109 && waistGirth >= 103)
        {
            SizeTwo.text = "40";
            // 40
        }
        /* В случае, если нет подходящих размеров - берём по ближайшим */
        else if (waistGirth < 74)
        {
            SizeTwo.text = "28";
            // 28
        }
        else if (waistGirth > 109)
        {
            SizeTwo.text = "40";
            // 40
        }

        /* Опредение длины штанины (для джинсов) в дюймах по росту */
        if (height <= 177 && height >= 165)
        {
            SizeThree.text = SizeTwo.text + "-32";
            // 32
        }
        else if (height <= 185 && height >= 178)
        {
            SizeThree.text = SizeTwo.text + "-34";
            // 34
        }
        else if (height <= 190 && height >= 186)
        {
            SizeThree.text = SizeTwo.text + "-36";
            // 36
        }
        /* В случае, если нет подходящих размеров - берём по ближайшим */
        else if (height < 165)
        {
            SizeThree.text = SizeTwo.text + "-32";
            height = 164;
            InputFieldOne.GetComponent<InputField>().text = Convert.ToString(height);
            // 32
        }
        else if (height > 190)
        {
            SizeThree.text = SizeTwo.text + "-36";
            height = 191;
            InputFieldOne.GetComponent<InputField>().text = Convert.ToString(height);
            // 36
        }
    }

    public void ChangeHumanParameters()
    {
        height = height - 82; // 172 => 90 scale Z
        chestGirth = chestGirth + 1; // 89 => 90 scale X
        //humanModelsLow.transform.localScale = new Vector3(humanModelsLow.transform.position.x, humanModelsLow.transform.position.y, humanModelsLow.transform.position.z);
        modelHuman.transform.localScale = new Vector3(chestGirth, chestGirth, height);
        modelUPver1.transform.localScale = new Vector3(chestGirth, chestGirth, height);
        modelUPver2.transform.localScale = new Vector3(chestGirth, chestGirth, height);
        modelUPver3.transform.localScale = new Vector3(chestGirth, chestGirth, height);
        modelDOWNver1.transform.localScale = new Vector3(chestGirth, chestGirth, height);
        modelDOWNver2.transform.localScale = new Vector3(chestGirth, chestGirth, height);
        humanModelsLow.transform.localPosition = new Vector3(humanModelsLow.transform.localPosition.x, height / 115 + 0.42f, humanModelsLow.transform.localPosition.z); // 90 local scale Z - 1.202 local position Y
    }

    public void ShowMenu()
    {
        transform.position = new Vector2(transform.position.x, 360);
        TextSize.transform.localPosition = new Vector2(343, 81);
        ShadowsMenu();
    }

    public void ChangeLighting()
    {
        if (ToggleLighting.GetComponent<Toggle>().isOn == true)
        {
            ShadowsOn();
        }
        else
        {
            ShadowsOff();
        }
    }

    public void ShadowsOn()
    {
        DirectionalLight.GetComponent<Light>().type = LightType.Spot;
        DirectionalLight.GetComponent<Light>().shadows = LightShadows.Soft;
        DirectionalLight.transform.position = new Vector3(0, 8, -7);
        LampOne.transform.position = new Vector3(-4, 7, -5);
        LampTwo.transform.position = new Vector3(4, 7, -5);
        LampThree.transform.position = new Vector3(400, 400, 400); // () = 0, 0, 0
    }
    public void ShadowsOff()
    {
        DirectionalLight.GetComponent<Light>().type = LightType.Directional;
        DirectionalLight.GetComponent<Light>().shadows = LightShadows.None;
        //DirectionalLight.transform.position = new Vector3(0, 11, -10);
        LampOne.transform.position = new Vector3(-4, 7, -400);
        LampTwo.transform.position = new Vector3(4, 7, -400);
        LampThree.transform.position = new Vector3(400, 400, 400);
    }
    public void ShadowsMenu()
    {
        LampThree.transform.position = new Vector3(-5, 2, 6);
    }
    public void ChangePanelTransparency()
    {
        if (TogglePanelTransparency.GetComponent<Toggle>().isOn == true)
        {
            PanelBackground.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }
        else
        {
            PanelBackground.GetComponent<Image>().color = new Color32(255, 255, 255, 65);
        }
    }
}
