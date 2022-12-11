using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Text textbullet;
    private int bulletcnt = 7;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DispBullet(int cnt)
    {
        bulletcnt -= cnt;
        textbullet.text = "7 / " + bulletcnt.ToString();
    }
}
