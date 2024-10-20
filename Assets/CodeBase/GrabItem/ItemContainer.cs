using System.Collections.Generic;
using UnityEngine;
namespace CodeBase.GrabLogic
{
    public class ItemContainer : MonoBehaviour
    {
        [SerializeField] private List<ItemPlace> _itemPlacements; // Список позиций для хранения предметов

        public bool IsFull => _itemPlacements.TrueForAll(place => place.GrabItem != null);
        public bool IsEmpty => _itemPlacements.TrueForAll(place => place.GrabItem == null);

        public bool TryAdd(GrabItem item)
        {
            if (IsFull)
                return false;

            foreach (var itemPlace in _itemPlacements)
            {
                if (itemPlace.GrabItem == null)
                {
                    itemPlace.Placement(item);
                    return true;
                }
            }

            return false;
        }
        public GrabItem Get()
        {
            foreach (var itemPlace in _itemPlacements)
            {
                if (itemPlace.GrabItem != null)
                {
                    var item = itemPlace.GrabItem; 
                    itemPlace.Release(); 
                    return item; 
                }
            }

            return null; 
        }
    }
}