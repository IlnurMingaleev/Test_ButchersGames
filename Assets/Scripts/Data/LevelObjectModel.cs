using System;
using PickUps;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public struct LevelObjectModel
    {
        public LevelObjectType Type;
        public LevelObjectTag LevelObjectTag;
    }
}