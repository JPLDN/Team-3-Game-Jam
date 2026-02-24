using System.Collections;
using UnityEngine;

public class Card : MonoBehaviour
{
    private SpriteRenderer sr;
    public int id;

    private static Card firstCard;
    private static Card secondCard;
    private static bool canClick = true;

    private GameController gameController;



    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gameController = FindAnyObjectByType<GameController>();
        HideFace();
    }

    private void OnMouseUpAsButton()
    {
        if (!canClick || sr.enabled == false)
            return;

        ShowFace();

        if (firstCard == null)
        {
            firstCard = this;
        }
        else
        {
            secondCard = this;
            canClick = false;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.8f);

        if (firstCard.id == secondCard.id)
        {
            // match found
            gameController.PairMatched();
        }
        else
        {
            // not found
            firstCard.HideFace();
            secondCard.HideFace();
        }

        firstCard = null;
        secondCard = null;
        canClick = true;
    }

    public void HideFace()
    {
        sr.enabled = true;
    }

    public void ShowFace()
    {
        sr.enabled = false;
    }
}
