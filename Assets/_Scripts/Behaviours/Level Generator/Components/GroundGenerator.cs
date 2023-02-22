using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetMonkey.Interfaces;

namespace JetMonkey.Generators
{

    public class GroundGenerator : MonoBehaviour, IGenerator
    {
        //[Header("Generator Settings")]
        //[SerializeField] private int numberOfPlatforms = 200;

        [Header("Prefabs Settings")]
        [SerializeField] private float YPos = .2f;
        [SerializeField] private float platformXOffset = 0.75f;

        [Space]

        [Header("Prefabs")]
        [SerializeField] private GameObject[] platformPrefabs;
        [SerializeField]    private Transform platformParent;



        private List<GameObject> spawnedPlatforms;
        private float currentXPosition;

        private void Awake()
        {
            spawnedPlatforms = new List<GameObject>();
            platformParent = transform;
        }

        public void Generate(float levelWidth)
        {
            currentXPosition = transform.position.x;
            int numberOfPlatforms = Mathf.CeilToInt(levelWidth / platformXOffset);

            for (int i = 0; i < numberOfPlatforms; i++)
            {
                GameObject newPlatform = GetRandomPlatform();
               

                newPlatform.transform.position = new Vector2(currentXPosition, YPos);
                spawnedPlatforms.Add(newPlatform);

                currentXPosition += platformXOffset;
            }
        }

        private GameObject GetRandomPlatform()
        {
            int randomIndex = Random.Range(0, platformPrefabs.Length);
            GameObject newPlatform = Instantiate(platformPrefabs[randomIndex], platformParent);
            return newPlatform;
        }

    }
}
