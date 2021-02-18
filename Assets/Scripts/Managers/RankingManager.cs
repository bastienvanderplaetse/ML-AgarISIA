using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RankingManager : MonoBehaviour
{
    public Text[] texts;
    public PlayerMisc[] players;

    private void Update()
    {
        RankPlayers();
        UpdateDisplay();
    }

    private void RankPlayers()
    {
        players = players.OrderBy(p => -p.Score()).ToArray();
    }

    private void UpdateDisplay()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = (i + 1) + " - " + players[i].name + " - " + players[i].Score();
            texts[i].color = players[i].PlayerColor;
        }
    }
}
