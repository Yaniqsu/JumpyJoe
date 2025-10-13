using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace YNQ.JumpyJoe
{
    /// <summary>
    /// Odpowiada za tworzenie nowych kafelków, generowanie przeszkód i dekoracji na nich.
    /// </summary>
    public class TileSpawner
    {
        private readonly Vector3 TileOffset = new Vector3(0, 1.29400003f, 4.82999992f);

        private TileSpawnData _tileSpawnData;
        private TileManager _tileManager;
        private float _obstacleSpawnRateEasing;

        /// <summary>
        /// Tworzy nowy spawner kafelków z podanymi danymi konfiguracyjnymi i menedżerem kafelków.
        /// </summary>
        /// <param name="tileManager">Menedżer kafelków zarządzający sceną.</param>
        /// <param name="tileSpawnData">Dane dotyczące prefabów i ustawień generowania kafelków.</param>
        public TileSpawner(TileManager tileManager, TileSpawnData tileSpawnData)
        {
            _tileManager = tileManager;
            _tileSpawnData = tileSpawnData;
        }

        /// <summary>
        /// Tworzy nowy kafelek, ustawia jego pozycję i generuje przeszkody oraz dekoracje.
        /// </summary>
        /// <param name="spawnObstacles">Określa, czy należy dodać przeszkody do kafelka.</param>
        /// <returns>Nowo utworzony kafelek.</returns>
        public Tile SpawnTile(bool spawnObstacles = true)
        {
            var tileSpawnPos = _tileManager.LastTile == null ? Vector3.zero : _tileManager.LastTile.transform.position + TileOffset;

            var tile = Object.Instantiate(_tileSpawnData.TilePrefab, tileSpawnPos, Quaternion.identity, _tileManager.transform);

            AddProps(tile);
            DisableRandomObjects(tile);
            if (spawnObstacles)
                SpawnObstacle(tile);

            return tile;
        }

        /// <summary>
        /// Dodaje losowe elementy dekoracyjne (propsy) do kafelka.
        /// </summary>
        /// <param name="tile">Kafelek, do którego mają zostać dodane propsy.</param>
        private void AddProps(Tile tile)
        {
            var maxPropsCount = Mathf.Min(tile.PropsTransforms.Length, _tileSpawnData.MaxProps);
            var propsCount = Random.Range(_tileSpawnData.MinProps, maxPropsCount + 1);

            var availablePoints = new List<Vector3>(tile.PropsTransforms.Select(p => p.transform.position));

            for (var i = 0; i < availablePoints.Count; i++)
            {
                var point = availablePoints[Random.Range(0, availablePoints.Count)];
                var prop = _tileSpawnData.RandomProp;

                Object.Instantiate(prop, point, Quaternion.identity, tile.transform);
                availablePoints.Remove(point);
            }
        }

        /// <summary>
        /// Losowo ukrywa część obiektów przypisanych do kafelka.
        /// </summary>
        /// <param name="tile">Kafelek, którego obiekty mają być przetworzone.</param>
        private void DisableRandomObjects(Tile tile)
        {
            foreach (var obj in tile.RandomObjects)
                if (Random.Range(0f, 1f) > _tileSpawnData.RandomObjectShowRate)
                    obj.gameObject.SetActive(false);
        }

        /// <summary>
        /// Losowo generuje przeszkody na kafelku, uwzględniając zmienny współczynnik pojawiania się.
        /// </summary>
        /// <param name="tile">Kafelek, na którym ma zostać utworzona przeszkoda.</param>
        private void SpawnObstacle(Tile tile)
        {
            var random = Random.Range(0f, 1f) - _obstacleSpawnRateEasing;

            if (random > _tileSpawnData.ObstacleShowRate)
            {
                _obstacleSpawnRateEasing += 0.1f;
                return;
            }

            _obstacleSpawnRateEasing = 0;
            Object.Instantiate(_tileSpawnData.RandomObstacle, tile.ObstaclePos, Quaternion.identity, tile.transform);
        }
    }
}
