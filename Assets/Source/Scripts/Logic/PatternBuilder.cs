using System.Collections.Generic;
using Scripts.Data;
using UnityEngine;

namespace Scripts.Logic
{
    public class PatternBuilder : MonoBehaviour
    {
        [SerializeField] private Cube _prefab;
        [SerializeField] private GameConfig _gameConfig;

        public List<Cube> Build(int number)
        {
            List<Cube> cubes = new List<Cube>();
            int index = number - 1;

            foreach (CubeData cubeData in _gameConfig.Patterns[index].Cubes)
            {
                Vector3 position = new Vector3(cubeData.Position.x, cubeData.Position.y, cubeData.Position.z);
                Cube cube = Instantiate(_prefab, position, Quaternion.LookRotation(cubeData.Rotation));
                cubes.Add(cube);
            }

            return cubes;
        }
    }
}