using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUI : MonoBehaviour
{
    public static bool cheat = false;
    void OnGUI()
    {
        int posX = 10;
        int posY = 10;
        int width = 100;
        int height = 30;
        int buffer = 10;

        List<string> itemList = Managers.Inventory.GetItemList();
        if(itemList.Count == 0)
        {
            GUI.Box(new Rect(posX, posY, width, height), "No items(");
        }

        foreach (string item in itemList)
        {
            int count = Managers.Inventory.GetItemCount(item);
            Texture2D image = Resources.Load<Texture2D>("Icons/" + item);
            GUI.Box(new Rect(posX, posY, width, height), new GUIContent("(" + count + ")",image));
            posX += buffer + width;
        }

            int money = Managers.Player.money;
            Texture2D img = Resources.Load<Texture2D>("Icons/money");
            GUI.Box(new Rect(10, Screen.height - 80, 100, 65), new GUIContent("(" + money + ")", img));

        string equipped = Managers.Inventory.equippedItem;
        if(equipped != null)
        {
            posX = Screen.width - (buffer + width);
            Texture2D image = Resources.Load("Icons/" + equipped) as Texture2D;
            GUI.Box(new Rect(posX, posY, width, height), new GUIContent("Equipped", image));
        }

        posX = 10;
        posY += buffer + height;

        foreach (string item in itemList)
        {
           if(GUI.Button(new Rect(posX, posY, width, height), new GUIContent("Equip ", item)))
            {
                Managers.Inventory.EquipItem(item);
            }

            if (item == "health")
            {
                if(GUI.Button(new Rect(posX, posY + height+buffer, width, height), new GUIContent("Use Health", item)))
                {
                    Managers.Inventory.ConsumeItem("health");
                    Managers.Player.ChangeHealth(25);
                }
            }
            posX += buffer + width;
        }

        if(cheat)
        {
            int posX_1 = Screen.width / 2 - 100;
            int posY_1 = Screen.height / 2 - 50;
            int width_1 = 200;
            int height_1 = 100;

            Texture2D warning_img = Resources.Load<Texture2D>("Icons/warning");
            GUI.Box(new Rect(posX_1, posY_1, width_1, height_1), new GUIContent("CHEATED!!!", warning_img));
        }
       

    }
}
