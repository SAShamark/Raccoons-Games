using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Cube.CubeObject
{
    public class CubeLevel: ILevelUp
    {
        public int Number { get; private set; }
        public int ColorIndex { get; private set; }

        private readonly List<Color> _colors;
        private readonly List<TMP_Text> _numberTexts;
        private readonly Renderer _renderer;
        private readonly ParticleSystem _levelUpParticle;
        private readonly float _durationToChangeCube;

        private const int NextNumber = 2;
        private const int FirstColorIndex = 0;
        private const int SecondColorIndex = 1;

        public CubeLevel(List<Color> colors, List<TMP_Text> numberTexts, Renderer renderer, float durationToChangeCube,
            ParticleSystem levelUpParticle)
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
                ChangeColorIndex(SecondColorIndex);
            }
            else
            {
                ChangeColorIndex(FirstColorIndex);
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
            int lastColorIndex = _colors.Count - 1;
            
            SetColor(_colors.Count > index ? _colors[index] : _colors[lastColorIndex]);
        }

        private void SetColor(Color color)
        {
            _renderer.material.DOColor(color, _durationToChangeCube);
        }
    }
}