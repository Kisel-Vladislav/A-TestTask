﻿using UnityEngine;
namespace CodeBase.Service
{
    public class StandaloneInputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public Vector2 Axis => UnityAxis();

        public bool IsGrabButtonUp => Input.GetKeyUp(KeyCode.F);
        public bool IsDropButtonUp => Input.GetKeyUp(KeyCode.G);

        private static Vector2 UnityAxis() =>
            new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));

    }
}
