using UnityEngine;
public class Baseball_Player : PlayParent
{
    public static void move()//trueはゲームオーバー
    {
        pmove(50, 1.875f);
        bmove();
    }
    public static void bmove()
    {
        float div = 1;
        DifChecker();
        BackGround();
        get();
        if (Main.situation == 6)
        {
            div = 1.2f;
        }
        if (Mathf.Abs(bx - px) > 90 * (Main.powerUps[1] + 1) * (Main.powerUps[2] + 1) / div / (Main.debuf[1] + 1) / (Main.debuf[2] + 1) && by < -70 + Main.powerUps[1] * 70 + Main.powerUps[2] * 70 || Main.over)//ゲームオーバー
        {
            Main.HeartAmount--;
            if (Main.HeartAmount == -1)
            {
                Main.over = true;
                if (Main.memory.GameScores[2] < timeCount)
                {
                    Main.memory.GameScores[2] = (int)timeCount;
                }
                Main.LastScoreTex.text = timeCount.ToString();
                Main.LastCoinTex.text = Main.coinCount.ToString();
                NextCalc();
                Main.BGMstop();
            }
            else
            {
                px = 0;
                Main.Hearts[Main.HeartAmount].SetActive(false);
                Main.brigi.Sleep();
                Main.bTra.transform.localPosition = new Vector2(0, 0);
                Main.pTra.transform.localPosition = new Vector2(0, -80);
            }
        }
        else if (by < -70 + Main.powerUps[1] * 70 + Main.powerUps[2] * 70 )//衝突時キック
        {
            if (once)//慣性リセット
            {
                Coinplus(-20, 180);
                Sound.SEplay(5);
                timeCount += 1 * (Main.powerUps[3] + 1) / (Main.debuf[3] + 1);
                Main.brigi.Sleep();
                Main.brigi.AddForce(new Vector2(0, (0.8f + addY) * Main.phases[0] / Main.phases[1] * Main.phases[6]), ForceMode2D.Impulse);
                once = false;
            }
            hx = (int)Main.pTra.transform.localPosition.x;
            Main.brigi.AddForce(new Vector2((bx - px) * 0.001f * Main.phases[6], 0), ForceMode2D.Impulse);
            Torque();
        }
        else if (Main.situation == 3)
        {
            int bi = 1;
            if (Random.Range(0, 10) > 4)
            {
                bi = -1;
            }
            Main.brigi.AddForce(new Vector2(Main.phases[2] * bi, 0));
        }
        else if (Main.situation == 4 || Main.situation == 5)
        {
            if (by > 180 && on3e)
            {
                Main.brigi.AddForce(new Vector2(Main.phases[2], 0), ForceMode2D.Impulse);
                on3e = false;
            }
            else if(by < 73)
            {
                on3e = true;
            }
            Main.brigi.AddForce(new Vector2(Main.phases[2] * 0.002f, 0), ForceMode2D.Impulse);
        }
        Reonce(-60 + Main.powerUps[1] * 70 + Main.powerUps[2] * 70);
        bWall(hx);
        Main.counter.text = timeCount.ToString();
    }
}
