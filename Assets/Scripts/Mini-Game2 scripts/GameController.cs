using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject card;
    public float spacing;
    public Sprite[] faces;
    private int facesIndex = 0;
    public int totalPairs = 8;   // 16 cards = 8 pairs
    private int matchedPairs = 0;
    public string SceneName;  // Scene should probably load when game is complete
    private int[] cardNumbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7,};

    private void Awake()
    {
        Shuffle();
        Layout();
    }

    void Shuffle()
    {
        for (int i = 0; i < 100; i++)
        {
            int n1 = Random.Range(0, cardNumbers.Length);
            int n2 = Random.Range(0, cardNumbers.Length);
            int temp = cardNumbers[n1];
            cardNumbers[n1] = cardNumbers[n2];
            cardNumbers[n2] = temp;
        }
    }


    public void PairMatched()
    {
        matchedPairs++;

        if (matchedPairs >= totalPairs)
        {
            SceneManager.LoadScene(SceneName);
        }
    }

    void Layout()
    {
        int index = 0;
        int cID = faces.Length;
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
            {
                float xpos = x * spacing + transform.position.x;
                float ypos = y * -spacing + transform.position.y;

                GameObject aCard = Instantiate(card, new Vector3(xpos, ypos, 0), Quaternion.identity);

                cID -= 1;

                facesIndex = cardNumbers[index];

                // Randomises, but has duplicates

                aCard.transform.GetChild(0)
                    .GetComponent<SpriteRenderer>().sprite = faces[cardNumbers[index]];

                aCard.GetComponent<Card>().id = cardNumbers[index];
                index += 1;
            }
        }


    }
}