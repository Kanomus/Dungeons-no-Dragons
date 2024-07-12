using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class Clock : MonoBehaviour
{
    public static Clock Instance { get; private set; }
    private static DungeonMaster dungeonMaster;
    private static Image clockVisual;
    public static int time;
    private static float fill = 1f;
    private void Start()
    {
        clockVisual = GetComponent<Image>();
        if(!GameObject.Find("DungeonMaster").TryGetComponent<DungeonMaster>(out dungeonMaster)) 
            Debug.Log("dungeon master not found");
    }
    public static void PassTime(int timeToPass)
    {
        time += timeToPass;
        time %= 100;
        fill = time/100f;
        clockVisual.fillAmount = fill;
        dungeonMaster.EnemyTurn(timeToPass);
    }
}
