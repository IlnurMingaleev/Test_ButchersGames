using UnityEngine;

namespace Runner
{
    public class LevelObjectTag : MonoBehaviour
    {
        [SerializeField] private LevelObjectType type;
        public LevelObjectType Type => type;
    }
}