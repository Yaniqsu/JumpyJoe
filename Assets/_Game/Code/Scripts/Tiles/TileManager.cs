using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace YNQ.JumpyJoe
{
    public class TileManager : MonoBehaviour
    {
        private const int START_SIZE = 14;
        [SerializeField] private TileSpawnData _tileSpawnData;

        public Tile LastTile => _tiles.Count > 0 ? _tiles[^1] : null;
        private List<Tile> _tiles = new List<Tile>();

        private TileSpawner _tileSpawner;

        public Vector3 CurrentPos
        {
            get 
            {
                return _tiles[2].Pos;
            }
        }
        public Vector3 NextPos => _tiles[3].Pos;

        private void Awake()
        {
            _tileSpawner = new TileSpawner(this, _tileSpawnData);
        }

        public void GenerateStartTiles()
        {
            for(var i = 0; i < START_SIZE; i++)
            {
                var tile = _tileSpawner.SpawnTile(i > 3);
                _tiles.Add(tile);
            }
        }

        public void AddTile()
        {
            var tile = _tileSpawner.SpawnTile();
            _tiles.Add(tile);

            //if(_tiles.Count > START_SIZE + 2)
            {
                Destroy(_tiles[0].gameObject);
                _tiles.RemoveAt(0);
            }
        }
    }
}
