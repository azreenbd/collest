using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public TMP_Text txtLevel, txtXP;

    public UserData userData;

    // Update is called once per frame
    void Update()
    {
        txtLevel.SetText("Level " + xpToLevel(userData.user.xp));
        txtXP.SetText("XP: " + userData.user.xp);
    }

    private int xpToLevel(int xp)
    {
        if (xp < 100)
        {
            return 1;
        }
        else if (xp >= 100 && xp < 200) {
            return 2;
        }
        else if (xp >= 200 && xp < 300)
        {
            return 3;
        }
        else if (xp >= 300 && xp < 400)
        {
            return 4;
        }
        else if (xp >= 400 && xp < 500)
        {
            return 5;
        }
        else if (xp >= 500 && xp < 600)
        {
            return 6;
        }
        else if (xp >= 600 && xp < 700)
        {
            return 7;
        }
        else if (xp >= 700 && xp < 800)
        {
            return 8;
        }
        else if (xp >= 800 && xp < 900)
        {
            return 9;
        }
        else if (xp >= 1000 && xp < 1100)
        {
            return 10;
        }
        else
        {
            return 11;
        }
    }
}
