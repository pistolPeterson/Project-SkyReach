using System.Collections.Generic;
using UnityEngine;

namespace SkyReach.Platforms
{
    public class PlatformGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject platformPrefab;
        [SerializeField] private Transform basePlatform;
        [SerializeField] private Rect spawnArea;
        [SerializeField] private List<GameObject> availablePlatforms;
        public int spawnCount;

        // internal variables
        private List<GameObject> spawnedPlatforms;

        void Start()
        {
            Generate(spawnCount);
        }

        void Generate(int count)
        {
            for (int i = 0; i < count; i++)
            {
                // get random platform
                GameObject platform = availablePlatforms[Random.Range(0, availablePlatforms.Count)];

                // get random position
                Vector2 position = new Vector2(Random.Range(spawnArea.xMin, spawnArea.xMax), Random.Range(spawnArea.yMin, spawnArea.yMax)) + (Vector2)spawnedPlatforms[^0]?.transform.position;

                // instantiate platform
                GameObject newPlatform = Instantiate(platform, position, Quaternion.identity, transform); // parents to the generator object

                // add to list
                spawnedPlatforms.Add(newPlatform);
            }
        }
    }
}
