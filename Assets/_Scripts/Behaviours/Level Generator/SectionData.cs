using System.Collections.Generic;
using UnityEngine;
using JetMonkey.Interfaces;

namespace JetMonkey.Generators
{
    public class SectionData
    {
        public float Width { get; private set; }
        private List<IGenerator> generators;

        public SectionData(float width)
        {
            Width = width;
            generators = new List<IGenerator>();
        }

        public void AddGenerator(IGenerator generator)
        {
            generators.Add(generator);
        }

        public void Generate()
        {
            foreach (IGenerator generator in generators)
            {
                generator.Generate(Width);
            }
        }
    }
}
