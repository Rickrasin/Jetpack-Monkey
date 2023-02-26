using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JetMonkey.Generators
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("Sections")]
        public List<GameObject> sectionPrefabs;

        [Space]

        [Header("Transition Sections")]
        public List<GameObject> transitionPrefabs;

        [Header("Settings")]
        public int numInitialSections = 3;
        public int numExtraSections = 2;

        private float levelLength;
        private float sectionWidth = 0f;
        private float transitionWidth = 0f;

        private List<GameObject> activeSections = new List<GameObject>();
        private Transform playerTransform;

        private void Start()
        {
            // Calcula o comprimento total do nível
            sectionWidth = sectionPrefabs[0].GetComponent<LevelSection>().GetSectionWidth();
            transitionWidth = transitionPrefabs[0].GetComponent<LevelSection>().GetSectionWidth();
            levelLength = numInitialSections * sectionWidth;

            for (int i = 0; i < numInitialSections; i++) //TODO: Aqui que irá entrar o Início do game
            {
                // Cria uma nova seção a partir de um prefab aleatório
                GameObject section = Instantiate(sectionPrefabs[Random.Range(0, sectionPrefabs.Count)], transform);

                // Define a posição da seção
                section.transform.position = new Vector3(i * sectionWidth, 0f, 0f);

                // Adiciona a seção à lista de seções ativas
                activeSections.Add(section);
            }

            // Obtém a referência para o transform do jogador
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            // Verifica se é necessário adicionar novas seções
            float playerX = playerTransform.position.x;
            float lastSectionEndX = activeSections[activeSections.Count - 1].transform.position.x;
            if (playerX > lastSectionEndX)
            {
                // Remove as seções que foram ultrapassadas pelo jogador
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
                    // Adiciona novas seções na frente do jogador
                    GenerateSection();
                }
            }
        }

        private void GenerateSection()
        {
            // Cria uma nova seção a partir de um prefab aleatório
            GameObject section = Instantiate(sectionPrefabs[Random.Range(0, sectionPrefabs.Count)], transform);
            GameObject transition = Instantiate(transitionPrefabs[Random.Range(0, transitionPrefabs.Count)], transform);

            // Define a posição da seção
            float sectionStart = activeSections[activeSections.Count - 1].transform.position.x + transitionWidth;
            section.transform.position = new Vector3(sectionStart, 0f, 0f);
            Debug.Log(sectionStart);


            //Define a posição da Transição
            float transitionStart = section.transform.position.x + sectionWidth;
            transition.transform.position = new Vector2(transitionStart, 0f);
            Debug.Log(transitionStart);


            // Adiciona a seção à lista de seções ativas
            activeSections.Add(section);
            activeSections.Add(transition);

            sectionWidth = sectionPrefabs[0].GetComponent<LevelSection>().GetSectionWidth();
            transitionWidth = transitionPrefabs[0].GetComponent<LevelSection>().GetSectionWidth();
        }
    }
}