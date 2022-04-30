using System.Collections.Generic;
using UnityEngine;

namespace Maps
{
    public class MapsManager : MonoBehaviour
    {
        [SerializeField] private List<GameMap> maps;

        public GameMap GetRandomMap(int playersCount)
        {
            var tmp = maps.FindAll(x => x.spawnPoints.Count == playersCount);
            return tmp[Random.Range(0, tmp.Count)];
        }
    }
}
