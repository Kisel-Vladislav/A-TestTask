using CodeBase.GrabLogic;
using CodeBase.Infrastructure;
using CodeBase.Service;
using UnityEngine;
namespace CodeBase.Player
{
    public class PlayerGrab : MonoBehaviour
    {
        [SerializeField] private ItemPlace itemPlace;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float radius;

        private IInputService _inputService;
        public bool HasGrabItem => itemPlace.GrabItem != null;

        private void Awake()
        {
            _inputService = AllServices.Container.Single<IInputService>();
        }
        private void Update()
        {
            if (_inputService.IsGrabButtonUp )
                Grab();
            else if (_inputService.IsDropButtonUp)
                Drop();
        }

        private void Drop()
        {
            if (!HasGrabItem)
                return;

            if (TryPlaceItemInContainer())
                ReleaseGrabbedItem();
        }
        private bool TryPlaceItemInContainer()
        {
            var colliders = GetCollidersInSphere();

            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<ItemContainer>(out var container))
                {
                    if (container.TryAdd(itemPlace.GrabItem))
                        return true;
                }
            }

            return false;
        }

        private void Grab()
        {
            if (HasGrabItem)
                return;

            if (TryGrabItemFromContainer() || TryGrabItemFromWorld())
                return;
        }
        private bool TryGrabItemFromContainer()
        {
            var colliders = GetCollidersInSphere();

            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<ItemContainer>(out var container) && !container.IsEmpty)
                {
                    GrabbedItem(container.Get());
                    return true;
                }
            }

            return false;
        }
        private bool TryGrabItemFromWorld()
        {
            Vector3 spherePosition = GetSpherePosition();
            var colliders = Physics.OverlapSphere(spherePosition, radius);

            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent<GrabItem>(out var grabItem))
                {
                    GrabbedItem(grabItem);
                    return true;
                }
            }

            return false;
        }

        private void GrabbedItem(GrabItem grabItem) => itemPlace.Placement(grabItem);
        private void ReleaseGrabbedItem() => itemPlace.Release();
        private Vector3 GetSpherePosition() => transform.position + transform.forward * radius + offset;
        private Collider[] GetCollidersInSphere()
        {
            Vector3 spherePosition = GetSpherePosition();
            var colliders = Physics.OverlapSphere(spherePosition, radius);
            return colliders;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(GetSpherePosition(), radius);
        }
    }

}
