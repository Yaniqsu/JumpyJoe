using UnityEngine;

namespace YNQ.JumpyJoe
{
    [CreateAssetMenu(fileName = "Tile Spawn Data", menuName = "Scriptable Objects/Tile Spawn Data", order = 0)]
    public class TileSpawnData : ScriptableObject
    {
        [SerializeField] private Tile _tilePrefab;
        [SerializeField] private GameObject[] _propsPrefabs;
        [SerializeField] private GameObject[] _obstaclesPrefabs;
        [SerializeField, Range(0, 6)] private int _minProps;
        [SerializeField, Range(0, 20)] private int _maxProps;
        [SerializeField, Range(0, 1)] private float _randomObjectShowRate;
        [SerializeField, Range(0, 1)] private float _obstacleShowRate;

        public Tile TilePrefab => _tilePrefab;
        public GameObject[] PropsPrefabs => _propsPrefabs;
        public GameObject[] ObstaclesPrefabs => _obstaclesPrefabs;
        public GameObject RandomProp => _propsPrefabs[Random.Range(0, _propsPrefabs.Length)];
        public GameObject RandomObstacle => _obstaclesPrefabs[Random.Range(0, _obstaclesPrefabs.Length)];
        public int MinProps => _minProps;
        public int MaxProps => _maxProps;
        public float RandomObjectShowRate => _randomObjectShowRate;
        public float ObstacleShowRate => _obstacleShowRate;
    }
}
