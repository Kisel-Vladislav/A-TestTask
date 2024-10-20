using System.Collections.Generic;
using UnityEngine;
namespace CodeBase.GrabLogic
{
    public class ItemContainer : MonoBehaviour
    {
        [SerializeField] private List<Transform> _itemPlacements;
        [SerializeField] private List<GrabItem> _items;

        public bool IsFull => _items.Count >= _itemPlacements.Count;
        public bool IsEmpty => _items[0] == null;

        public bool TryAdd(GrabItem item)
        {
            if (IsFull)
                return false;

            _items.Add(item);
            Placement(item);

            return true;
        }

        private void Placement(GrabItem item)
        {
            item.transform.SetParent(_itemPlacements[_items.Count - 1]);
            item.gameObject.transform.localPosition = Vector3.zero;
        }

        public GrabItem Get()
        {
            var item = _items[_items.Count - 1]; 
            _items.RemoveAt(_items.Count - 1); 
            return item; 
        }
    }
}