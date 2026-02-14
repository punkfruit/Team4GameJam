using UnityEngine;
using TMPro;

public class PointsTriggerParent : MonoBehaviour
{
    public int points = 0;
    public TMP_Text pointsText;

    public void IncrementPoints()
    {
        points += 10;
        UpdatePointsText();
    }

    void UpdatePointsText()
    {
        pointsText.text = points.ToString();
    }
}
