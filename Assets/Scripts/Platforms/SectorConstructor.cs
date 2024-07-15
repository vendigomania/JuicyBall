using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorConstructor : MonoBehaviour
{
    [SerializeField] private EmptyPlatform empty;
    [SerializeField] private BombPlatform bomb;
    [SerializeField] private GhostPlatform ghost;
    [SerializeField] private MovingPlatform moving;
    [SerializeField] private FinishPlatform finish;
    [SerializeField] private TramplinePLatform trampline;

    public void SetSector()
    {
        if(JuicyBouncingBallGame.LevelCellsCount == JuicyBouncingBallGame.LevelMaxCellsCount - 6)
        {
            SwitchSector(5);
            finish.LoadCell();
        }
        else if (JuicyBouncingBallGame.LevelCellsCount < JuicyBouncingBallGame.LevelMaxCellsCount - 6)
        {
            int random = Random.Range(0, 100);

            if(random < 40)
            {
                SwitchSector(0);
            }
            else if(random < 60)
            {
                SwitchSector(1);
                bomb.LoadCell();
            }
            else if(random < 80)
            {
                SwitchSector(2);
                ghost.LoadCell();
            }
            else if(random < 90)
            {
                SwitchSector(3);
            }
            else
            {
                SwitchSector(4);
                trampline.LoadCell();
            }
        }
        else
        {
            SwitchSector(-1);
        }
    }

    private void SwitchSector(int number)
    {
        empty.gameObject.SetActive(number == 0);
        bomb.gameObject.SetActive(number == 1);
        ghost.gameObject.SetActive(number == 2);
        moving.gameObject.SetActive(number == 3);
        trampline.gameObject.SetActive(number == 4);
        finish.gameObject.SetActive(number == 5);
    }
}
