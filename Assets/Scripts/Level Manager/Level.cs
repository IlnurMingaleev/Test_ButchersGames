using System.Collections.Generic;
using PathCreation;
using PathCreation.Examples;
using UnityEditor;
using UnityEngine;

namespace ButchersGames
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] private List<Transform> _wayPoints;
        

        public Transform PlayerSpawnPoint => playerSpawnPoint;
        public PathCreator PathCreator => _pathCreator; 
        public List<Transform> WayPoints => _wayPoints;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (playerSpawnPoint != null)
        {
            Gizmos.color = Color.magenta;
            var m = Gizmos.matrix;
            Gizmos.matrix = playerSpawnPoint.localToWorldMatrix;
            Gizmos.DrawSphere(Vector3.up * 0.5f + Vector3.forward, 0.5f);
            Gizmos.DrawCube(Vector3.up * 0.5f, Vector3.one);
            Gizmos.matrix = m;
        }
    }
#endif
    }
}