using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Key : Item
{
    private Player player;
    private void Start()
    {
        player = FindAnyObjectByType<Player>();
    }
    protected override void CollectItem()
    {
        player.keyCount++;
    }
}
