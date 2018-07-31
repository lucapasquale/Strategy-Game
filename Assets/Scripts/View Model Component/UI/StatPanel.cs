using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatPanel : MonoBehaviour
{
    public Panel panel;
    public Sprite allyBackground;
    public Sprite enemyBackground;
    public Image background;
    public Text hpLabel;
    public Text atkLabel;
    public Text defLabel;

    public void Display(GameObject obj) {
        Unit unit = obj.GetComponent<Unit>();
        background.sprite = unit.alliance == Alliances.Enemy ? enemyBackground : allyBackground;

        Stats stats = obj.GetComponent<Stats>();
        if (stats) {
            hpLabel.text = string.Format("HP {0} / {1}", stats[StatTypes.HP], stats[StatTypes.MHP]);
            atkLabel.text = string.Format("ATK. {0}", stats[StatTypes.ATK]);
            defLabel.text = string.Format("DEF. {0}", stats[StatTypes.DEF]);
        }
    }
}