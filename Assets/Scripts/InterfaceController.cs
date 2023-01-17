using Cube;
using TMPro;

public class InterfaceController
{
    private int _points;
    private readonly TMP_Text _pointsText;

    public InterfaceController(TMP_Text pointsText)
    {
        _pointsText = pointsText;
        ChangePointsText(0);
    }
    public void ChangePoints(CubeControl cubeControl)
    {
        _points += cubeControl.Number;
        ChangePointsText(_points);
    }

    private void ChangePointsText(int pointNumber)
    {
        _pointsText.text = pointNumber.ToString();
    }
}