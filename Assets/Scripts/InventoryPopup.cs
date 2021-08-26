using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryPopup : MonoBehaviour
{
    [SerializeField] private Text[] itemLabels;
    [SerializeField] private Image[] itemImages;

    [SerializeField] private Text CurItemLabel;
    [SerializeField] private Button equipButton;
    [SerializeField] private Button useButton;

    private string _curItem;
   
    public void Refresh()
    {
        List<string> itemList = Managers.Inventory.GetItemList();

        int len = itemImages.Length;
        for (int i = 0; i < len; i++)
        {
            if (i < itemList.Count)
            {
                itemImages[i].gameObject.SetActive(true);
                itemLabels[i].gameObject.SetActive(true);

                string item = itemList[i];

                Sprite sprite = Resources.Load<Sprite>("Icons/" + item);
                itemImages[i].sprite = sprite;
                itemImages[i].SetNativeSize();

                int count = Managers.Inventory.GetItemCount(item);
                string message = "x" + count;
                if (item == Managers.Inventory.equippedItem)
                {
                    message = "Equiped\n" + message;
                }
                itemLabels[i].text = message;

                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;
                entry.callback.AddListener((BaseEventData data) =>
                {
                    OnItem(item);
                });
                EventTrigger trigger = itemImages[i].GetComponent<EventTrigger>();
                trigger.triggers.Clear();
                trigger.triggers.Add(entry);
            }
            else
            {
                itemImages[i].gameObject.SetActive(false);
                itemLabels[i].gameObject.SetActive(false);
            }
        }

        if(!itemList.Contains(_curItem))
        {
            _curItem = null;
        }

        if(_curItem == null)
        {
            CurItemLabel.gameObject.SetActive(false);
            equipButton.gameObject.SetActive(false);
            useButton.gameObject.SetActive(false);
        }
        else
        {
            CurItemLabel.gameObject.SetActive(true);
            equipButton.gameObject.SetActive(true);
            if(_curItem=="health")
            {
                useButton.gameObject.SetActive(true);
            }
            else useButton.gameObject.SetActive(false);

            CurItemLabel.text = _curItem + ":";
        }
    }

    public void OnItem(string item)
    {
        _curItem = item;
        Refresh();
    }

    public void OnEquip()
    {
        Managers.Inventory.EquipItem(_curItem);
        Refresh();
    }

    public void OnUse()
    {
        Managers.Inventory.ConsumeItem(_curItem);
        if(_curItem == "health")
        {
            Managers.Player.ChangeHealth(25);
        }
        Refresh();
    }
}
