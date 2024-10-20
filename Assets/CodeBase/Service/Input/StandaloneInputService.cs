using UnityEngine;
namespace CodeBase.Service
{
    public class StandaloneInputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public Vector2 Axis => UnityAxis();

        private static Vector2 UnityAxis() =>
            new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));

    }

}
