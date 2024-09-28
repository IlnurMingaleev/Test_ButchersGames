using UnityEngine;

namespace PickUps
{
    public class LevelObjectTag:MonoBehaviour
    {
        [SerializeField] private LevelObjectType type;
        public LevelObjectType Type => type;
    }
}