using UnityEngine;

namespace CodeBase.GrabLogic
{
    public class ItemPlace : MonoBehaviour
    {
        public GrabItem GrabItem;

        public void Placement(GrabItem grabItem)
        {
            grabItem.transform.SetParent(transform);
            grabItem.gameObject.transform.localPosition = Vector3.zero;
            GrabItem = grabItem;
        }
        public void Release()
        {
            GrabItem.transform.SetParent(null);
            GrabItem = null;
        }
    }
}