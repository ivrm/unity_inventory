using System;
using Game.Scripts.Props;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Scripts.Inventory
{
    [Serializable]
    public class InventorySlot : MonoBehaviour
    {
        public Collectible Item => item;
        public int Position => position;
        
        [SerializeField] private int amount;
        [SerializeField] private Sprite placeholderIcon;
        [SerializeField] private int size = 1;
        [SerializeField] private Image icon;
        [SerializeField] private int position;
        
        private Collectible item;

        public bool TryAddItem(Collectible itm)
        {
            if (itm == null) return false;
            
            if (item != null && item.Id == itm.Id)
            { 
                if (amount < size)
                {
                    IncrementAmount();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                AddItem(itm);
            }

            return true;
        }

        public bool RemoveItem()
        {
            if (amount <= 0)
            {
                return false;
            }
            
            --amount;
            
            if (amount == 0)
            {
                Clear();
            }

            return true;
        }

        private void Start()
        {
            icon = GetComponent<Image>();
            icon.sprite = placeholderIcon;
        }

        private void Clear()
        {
            item = null;
            icon.sprite = placeholderIcon;
        }

        private void IncrementAmount()
        {
            ++amount;
        }

        private void AddItem(Collectible itm)
        {
            ++amount;
            icon.sprite = itm.Icon;
            item = itm;
        }
    }
}
