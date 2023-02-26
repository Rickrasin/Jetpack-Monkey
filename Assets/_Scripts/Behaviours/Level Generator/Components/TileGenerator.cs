using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetMonkey.Interfaces;

namespace JetMonkey.Generators
{
    /// <summary>
    /// Esta classe é responsável por gerar e posicionar tiles em um nível.
    /// </summary>
    public class TileGenerator : MonoBehaviour, IGenerator
    {
       

        [Header("Spawn Settings")]
        [SerializeField] private float tilesYOffset = 4f;
        [SerializeField] private Vector2 tilesDistance = new Vector2(4f, 8f);
        [Space(5)]
        [SerializeField] private bool addColliders = false;
        [Space(5)]
        [SerializeField] private LayerMask layer;

        private Transform tilesParent;

        [Header("Sprites")]
        [SerializeField] private int priorityLayer;
        [Space(5)]
        [SerializeField] private Sprite[] tilesSprites;


        private List<GameObject> spawnedTiles;
        private float currentXPosition;

        private void Awake()
        {
            spawnedTiles = new List<GameObject>();
            tilesParent = transform;
        }

        public void Generate(float levelWidth)
        {

            float tileXOffset = Random.Range(tilesDistance.x, tilesDistance.y);


            int numberOfTiles = Mathf.CeilToInt(levelWidth / tileXOffset);

            currentXPosition = transform.position.x + tileXOffset;

            for (int i = 0; i < numberOfTiles; i++)
            {

                tileXOffset = Random.Range(tilesDistance.x, tilesDistance.y);

                GameObject newTile = new GameObject();
                newTile.transform.parent = tilesParent;
                newTile.layer = layer.value;

                SpriteRenderer spriteRenderer = newTile.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = GetRandomSprite();
                spriteRenderer.sortingOrder = priorityLayer;

                if (addColliders)
                {
                    BoxCollider2D collider = newTile.AddComponent<BoxCollider2D>();
                    collider.size = spriteRenderer.size;
                }

                newTile.transform.position = new Vector2(currentXPosition, tilesYOffset);
                spawnedTiles.Add(newTile);


                currentXPosition += tileXOffset;

            }
        }
        private Sprite GetRandomSprite()
        {
            int randomIndex = Random.Range(0, tilesSprites.Length);
            return tilesSprites[randomIndex];
        }

    }
}
