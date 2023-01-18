using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Cube.CubeObject
{
    public class CubeLevel
    {
        public int Number { get; private set; }
        public int ColorIndex { get; private set; }

        private readonly List<Color> _colors;
        private readonly List<TMP_Text> _numberTexts;
        private readonly Renderer _renderer;
        private readonly float _durationToChangeCube;
        private readonly ParticleSystem _levelUpParticle;

        private const int NextNumber = 2;

        public CubeLevel(List<Color> colors, List<TMP_Text> numberTexts, Renderer renderer, float durationToChangeCube, ParticleSystem levelUpParticle)
        {
            _colors = colors;
            _numberTexts = numberTexts;
            _renderer = renderer;
            _durationToChangeCube = durationToChangeCube;
            _levelUpParticle = levelUpParticle;
        }

        public void IsSecondLevel(bool isSecondLevel, int baseNumber)
        {
            if (isSecondLevel)
            {
                ChangeNumber(baseNumber * NextNumber);
                ChangeColorIndex(1);
            }
            else
            {
                ChangeColorIndex(0);
                ChangeNumber(baseNumber);
            }
        }

        public void NextLevel(int nextCubeNumber, int nextColorIndex)
        {
            _levelUpParticle.Play();
            ChangeNumber(nextCubeNumber);
            ChangeColorIndex(nextColorIndex);
        }

        private void ChangeNumber(int number)
        {
            Number = number;
            SetNumberText(number);
        }

        private void SetNumberText(int number)
        {
            foreach (var text in _numberTexts)
            {
                text.text = number.ToString();
            }
        }

        private void ChangeColorIndex(int index)
        {
            ColorIndex = index;
            SetColor(_colors.Count > index ? _colors[index] : _colors[_colors.Count - 1]);
        }

        private void SetColor(Color color)
        {
            _renderer.material.DOColor(color, _durationToChangeCube);
        }
    }
}