using Cube.CubeObject;
using TMPro;

public class Points
{
    private int _points;

    private readonly TMP_Text _pointsText;

    private const int StartPoints = 0;

    public Points(TMP_Text pointsText)
    {
        _pointsText = pointsText;
    }

    public void Init()
    {
        ChangePointsText(StartPoints);
    }

    public void ChangePoints(CubeControl cubeControl)
    {
        _points += cubeControl.CubeLevel.Number;
        ChangePointsText(_points);
    }

    private void ChangePointsText(int pointNumber)
    {
        _pointsText.text = pointNumber.ToString();
    }
}