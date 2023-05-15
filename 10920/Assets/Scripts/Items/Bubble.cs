using UnityEngine;
using System.Collections;

public class Bubble : Item
{
    [SerializeField] private float _oxygen = 1;

    public override void InteractWithPlayer(PlayerController player)
    {
        base.InteractWithPlayer(player);

        player.AddOxygen(_oxygen);
        Die();
    }
}
