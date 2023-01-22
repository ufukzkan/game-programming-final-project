using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinAmount = 100;

    private string CoinAmounID = "CoinAmount";

    Animator animator;
    Animator otherUIAnimator;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        otherUIAnimator = gameObject.transform.parent.Find("BackgroundImage").GetComponent<Animator>();
        coinAmount = PlayerPrefs.GetInt(CoinAmounID, 0);
        this.GetComponent<TextMeshProUGUI>().text = coinAmount.ToString();
    }

    public void AddValue(int value)
    {
        coinAmount += value;
        this.GetComponent<TextMeshProUGUI>().text = coinAmount.ToString();
        PlayerPrefs.SetInt(CoinAmounID, coinAmount);
        animator.SetTrigger("Pop");
        otherUIAnimator.SetTrigger("Pop");
    }
}
