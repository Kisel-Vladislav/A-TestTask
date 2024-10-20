using CodeBase.GrabLogic;
using UnityEngine;
namespace CodeBase.Player
{
    public class PlayerGrab : MonoBehaviour
    {
        [SerializeField] Transform GrabItemPlacement;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float radius;

        private GrabItem _grabItem;

        public bool HasGrabItem => _grabItem != null;

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.F))
                Grab();
            else if (Input.GetKeyUp(KeyCode.G))
                Drop();
        }

        private void Drop()
        {
            if (!HasGrabItem)
                return;

            ReleaseGrabbedItem();
        }

        private void ReleaseGrabbedItem()
        {
            _grabItem.transform.SetParent(null);
            _grabItem = null;
        }

        private void Grab()
        {
            if(HasGrabItem)
                return;

            var colliders = Physics.OverlapSphere(transform.position + offset, radius);
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<GrabItem>(out var grabItem))
                {
                    GrabbedItem(grabItem);
                    break;
                }
            }
        }

        private void GrabbedItem(GrabItem grabItem)
        {
            grabItem.transform.SetParent(GrabItemPlacement);
            grabItem.gameObject.transform.localPosition = Vector3.zero;

            _grabItem = grabItem;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawWireSphere(transform.position + offset, radius);
        }
    }

}
