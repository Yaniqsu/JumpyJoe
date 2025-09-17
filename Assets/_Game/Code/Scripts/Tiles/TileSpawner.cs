using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YNQ.JumpyJoe
{
    public class TileSpawner
    {
        private readonly Vector3 TileOffset = new Vector3(0, 1.29400003f, 4.82999992f);

        private TileSpawnData _tileSpawnData;
        private TileManager _tileManager;

        public TileSpawner(TileManager tileManager, TileSpawnData tileSpawnData)
        {
            _tileManager = tileManager;
            _tileSpawnData = tileSpawnData;
        }

        public Tile SpawnTile(bool spawnObstacles = true)
        {
            var tileSpawnPos = _tileManager.LastTile == null ? Vector3.zero : _tileManager.LastTile.transform.position + TileOffset;

            var tile = Object.Instantiate(_tileSpawnData.TilePrefab, tileSpawnPos, Quaternion.identity, _tileManager.transform);

            AddProps(tile);
            DisableRandomObjects(tile);
            if(spawnObstacles)
                SpawnObstacle(tile);

            return tile;
        }

        private void AddProps(Tile tile)
        {
            var maxPropsCount = Mathf.Min(tile.PropsTransforms.Length, _tileSpawnData.MaxProps);
            var propsCount = Random.Range(_tileSpawnData.MinProps, maxPropsCount + 1);

            var availablePoints = new List<Vector3>(tile.PropsTransforms.Select(p => p.transform.position));

            for(var i = 0; i < availablePoints.Count; i++)
            {
                var point = availablePoints[Random.Range(0, availablePoints.Count)];
                var prop = _tileSpawnData.RandomProp;

                Object.Instantiate(prop, point, Quaternion.identity, tile.transform);
                availablePoints.Remove(point);
            }
        }

        private void DisableRandomObjects(Tile tile)
        {
            foreach(var obj in tile.RandomObjects)
                if(Random.Range(0f, 1f) > _tileSpawnData.RandomObjectShowRate)
                    obj.gameObject.SetActive(false);
        }

        private void SpawnObstacle(Tile tile)
        {
            var random = Random.Range(0f, 1f);

            if (random > _tileSpawnData.ObstacleShowRate)
                return;

            Object.Instantiate(_tileSpawnData.RandomObstacle, tile.ObstaclePos, Quaternion.identity, tile.transform);
        }
    }
}
