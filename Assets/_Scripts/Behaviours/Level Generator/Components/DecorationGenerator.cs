using JetMonkey.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace JetMonkey.Generators
{
    public class DecorationGenerator : MonoBehaviour, IGenerator
    {
        //[Header("Generator Settings")]
        //[SerializeField] private int numberOfDecorations = 200;

        [Header("Prefabs Settings")]
        [SerializeField] private float decorationYOffset = 4f;
        [SerializeField] private Vector2 decorationDistance = new Vector2(4f, 8f);

        [Space]

        [Header("Prefabs")]
        [SerializeField] private GameObject[] decorationPrefabs;
        [SerializeField] private Transform decorationParent;


        private List<GameObject> spawnedDecorations;
        private float currentXPosition;
        private void Awake()
        {
            spawnedDecorations = new List<GameObject>();
            decorationParent = transform;
        }

        public void Generate(float levelWidth)
        {

            float decorationXOffset = Random.Range(decorationDistance.x, decorationDistance.y);


            int numberOfDecorations = Mathf.CeilToInt(levelWidth / decorationXOffset);

            currentXPosition = transform.position.x;

            for (int i = 0; i < numberOfDecorations; i++)
            {
                GameObject newDecoration = GetRandomDecoration();


                newDecoration.transform.position = new Vector2(currentXPosition, decorationYOffset);
                spawnedDecorations.Add(newDecoration);

                currentXPosition += decorationXOffset;
            }
        }

        private GameObject GetRandomDecoration()
        {
            int randomIndex = Random.Range(0, decorationPrefabs.Length);
            GameObject newDecoration = Instantiate(decorationPrefabs[randomIndex], decorationParent);
            return newDecoration;
        }
    }
}
