using System;
using Cube;
using Cube.CubeObject;
using Scriptable;
using TMPro;
using UnityEngine;

public class ApplicationStart : MonoBehaviour
{
    [Header("CubeInfo")] [SerializeField] private CubeControl _cubePrefab;
    [SerializeField] private Transform _cubeTransform;
    [SerializeField] private Transform _startCubePosition;
    [SerializeField] private CubeDates _cubeDates;

    [Header("UI")] [SerializeField] private TMP_Text _pointsText;

    private CubeFactory _cubeFactory;
    private InputCubeAction _inputCubeAction;
    private CubeCombiner _cubeCombiner;
    private Points _points;

    private void Awake()
    {
        _cubeFactory = new CubeFactory(_cubeDates, _cubePrefab, _cubeTransform, _startCubePosition);
        _inputCubeAction = new InputCubeAction();
        _cubeCombiner = new CubeCombiner();
        _points = new Points(_pointsText);
    }

    private void Start()
    {
        _inputCubeAction.Start();
        _points.Init();

        _inputCubeAction.Detached += CubeDetach;
        _inputCubeAction.Detached += _points.ChangePoints;
        _cubeCombiner.OnCombined += CubeCombined;

        GetNewCube();
    }

    private void Update()
    {
        _inputCubeAction?.Update();
    }
    
    private void OnDestroy()
    {
        _inputCubeAction.Detached -= CubeDetach;
        _inputCubeAction.Detached -= _points.ChangePoints;
        _cubeCombiner.OnCombined -= CubeCombined;
    }

    private void CubeDetach(CubeControl cube)
    {
        cube.OnCollide += CubeCollide;
        GetNewCube();
    }

    private void GetNewCube()
    {
        var cube = _cubeFactory.GetCube();
        _inputCubeAction.Attach(cube);
    }

    private void CubeCollide(CubeControl firstCube, CubeControl secondCube)
    {
        _cubeCombiner.Combine(firstCube, secondCube);

        firstCube.OnCollide -= CubeCollide;
        secondCube.OnCollide -= CubeCollide;
    }

    private void CubeCombined(CubeControl cube)
    {
        cube.OnCollide += CubeCollide;
    }
}