using UnityEngine;
using TMPro;

public class CoinView : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinText;

    private void Start()
    {
        Coin.Instance.ChangeCurrency += UpdateText;
    }

    private void UpdateText(float currentCoinCount)
    {
        var count = Mathf.RoundToInt(currentCoinCount);
        _coinText.text = count.ToString();
    }

    private void OnDestroy()
    {
        Coin.Instance.ChangeCurrency -= UpdateText;
    }
}
