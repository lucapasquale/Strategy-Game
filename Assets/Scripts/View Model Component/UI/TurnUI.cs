using UnityEngine;
using UnityEngine.UI;

public class TurnUI : MonoBehaviour
{
    public Text nicknameLabel;
    public Text hpLabel;
    public Text atkLabel;
    public Text defLabel;

    private void OnEnable() {
        this.AddObserver(UpdateTurnUI, RoundController.SelectedNotification);
    }

    private void OnDisable() {
        this.RemoveObserver(UpdateTurnUI, RoundController.SelectedNotification);
    }

    private void UpdateTurnUI(object sender, object args) {
        GameObject textsPanel = hpLabel.transform.parent.gameObject;
        Unit unit = args as Unit;

        if (!unit) {
            textsPanel.SetActive(false);
            return;
        }

        textsPanel.SetActive(true);
        Stats stats = unit.GetComponent<Stats>();

        nicknameLabel.text = unit.nickname;
        hpLabel.text = $"HP: {stats[StatTypes.HP]} / {stats[StatTypes.MHP]}";
        atkLabel.text = $"ATK: {stats[StatTypes.ATK]}";
        defLabel.text = $"ATK: {stats[StatTypes.DEF]}";
    }
}