using PathCreation;
using PathCreation.Examples;
using UnityEditor;
using UnityEngine;

namespace ButchersGames
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private GameObject _roadMesh;
        [SerializeField] private RoadMeshCreator _pathCreator;

        public void UpdateRoad()
        {
            //_roadMesh.gameObject.SetActive(false);
            //_roadMesh.gameObject.SetActive(true);
            //Selection.SetActiveObjectWithContext(_roadMesh, t);
        }

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