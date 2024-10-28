using System;
using UnityEngine;

namespace Scripts.Data
{
    [Serializable]
    public class CubeData
    {
        public CubeData(Vector3 position, Vector3 rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public Vector3 Rotation { get; private set; }
    }
}