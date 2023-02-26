using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetMonkey.Generators
{
    public class LevelGenerator : MonoBehaviour
    {
        public List<GameObject> sectionPrefabs;
        public int numInitialSections = 3;
        public int numExtraSections = 2;

        private float levelLength;
        private float sectionWidth;
        private List<GameObject> activeSections = new List<GameObject>();
        private Transform playerTransform;

        private void Start()
        {
            // Calcula o comprimento total do n�vel
            sectionWidth = sectionPrefabs[0].GetComponent<LevelSection>().GetSectionWidth();
            levelLength = numInitialSections * sectionWidth;
            Debug.Log(levelLength);

            for (int i = 0; i < numInitialSections; i++)
            {
                // Cria uma nova se��o a partir de um prefab aleat�rio
                GameObject section = Instantiate(sectionPrefabs[Random.Range(0, sectionPrefabs.Count)], transform);

                // Define a posi��o da se��o
                section.transform.position = new Vector3(i * sectionWidth, 0f, 0f);

                // Adiciona a se��o � lista de se��es ativas
                activeSections.Add(section);
            }

            // Obt�m a refer�ncia para o transform do jogador
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            // Verifica se � necess�rio adicionar novas se��es
            float playerX = playerTransform.position.x;
            float lastSectionEndX = activeSections[activeSections.Count - 1].transform.position.x;
            if (playerX > lastSectionEndX)
            {
                // Remove as se��es que foram ultrapassadas pelo jogador
                for (int i = 0; i < activeSections.Count; i++)
                {
                    if (activeSections[i].transform.position.x + sectionWidth < playerX - levelLength / 2f)
                    {
                        Destroy(activeSections[i]);
                        activeSections.RemoveAt(i);
                        i--;
                    }
                }

                for (int i = 0; i < numExtraSections; i++)
                {
                    // Adiciona novas se��es na frente do jogador
                    GenerateSection();
                }
            }
        }

        private void GenerateSection()
        {
            // Cria uma nova se��o a partir de um prefab aleat�rio
            GameObject section = Instantiate(sectionPrefabs[Random.Range(0, sectionPrefabs.Count)], transform);

            // Define a posi��o da se��o
            float sectionStart = activeSections[activeSections.Count - 1].transform.position.x + sectionWidth;
            section.transform.position = new Vector3(sectionStart, 0f, 0f);

            // Adiciona a se��o � lista de se��es ativas
            activeSections.Add(section);
        }
    }
}