using System.Collections;
using UnityEngine;

public class Coin : Item
{
    [SerializeField] private int _coins = 1;

    public override void InteractWithPlayer(PlayerController player)
    {
        base.InteractWithPlayer(player);

        player.AddCoins(_coins);
        Die();
    }

}
