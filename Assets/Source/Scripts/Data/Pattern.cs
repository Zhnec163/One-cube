using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
{
    [Serializable]
    public class Pattern
    {
        public Pattern(List<CubeData> cubes) =>
            Cubes = cubes;

        [field: SerializeField] public List<CubeData> Cubes { get; private set; }
    }
}