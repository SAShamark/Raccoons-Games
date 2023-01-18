using Cube;
using Scriptable;
using TMPro;
using UnityEngine;

public class ApplicationStart : MonoBehaviour
{
    [SerializeField] private CubeControl _cubePrefab;
    [SerializeField] private Transform _cubeTransform;
    [SerializeField] private Transform _startCubePosition;
    [SerializeField] private CubeDates _cubeDates;
    [SerializeField] private TMP_Text _pointsText;

    private CubeFactory _cubeFactory;
    private InputCubeAction _inputCubeAction;
    private CubeCombiner _cubeCombiner;
    private InterfaceController _interfaceController;

    private void Awake()
    {
        _cubeFactory = new CubeFactory(_cubeDates, _cubePrefab, _cubeTransform, _startCubePosition);
        _inputCubeAction = new InputCubeAction();
        _cubeCombiner = new CubeCombiner();
        _interfaceController = new InterfaceController(_pointsText);
    }

    private void Start()
    {
        _inputCubeAction.Detached += CubeDetach;
        _inputCubeAction.Detached += _interfaceController.ChangePoints;
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
        _inputCubeAction.Detached += _interfaceController.ChangePoints;
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

    private void CubeCollide(CubeControl cube1, CubeControl cube2)
    {
        _cubeCombiner.Combine(cube1, cube2);

        cube1.OnCollide -= CubeCollide;
        cube2.OnCollide -= CubeCollide;
    }

    private void CubeCombined(CubeControl cube)
    {
        cube.OnCollide += CubeCollide;
    }
}