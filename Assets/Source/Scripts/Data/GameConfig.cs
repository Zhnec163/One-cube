using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Data
{
    [CreateAssetMenu(menuName = "Create GameConfig", fileName = "GameConfig", order = 51)]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public List<Pattern> Patterns { get; private set; }
    }
}