using System.Collections.Generic;
using JetMonkey.Interfaces;
using UnityEngine;

namespace JetMonkey.Generators
{
    public class LevelSection : MonoBehaviour
    {
        [Header("Level Parameters")]
        [SerializeField] private float sectionWidth = 100f;

        [Header("Sections")]
        [SerializeField] private List<SectionData> sections;

        private void Awake()
        {
            sections = new List<SectionData>();
            foreach (Transform child in transform)
            {
                SectionData section = new SectionData(sectionWidth);
                foreach (IGenerator generator in child.GetComponents<IGenerator>())
                {
                    section.AddGenerator(generator);
                }
                sections.Add(section);
            }
        }

        private void Start()
        {
            foreach (SectionData section in sections)
            {
                section.Generate();
            }
        }

        public float GetSectionWidth() => sectionWidth;

    }
}
