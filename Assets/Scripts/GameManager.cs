using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GridLayoutGroup gridLayout;
    public GameObject cardPrefab;
    public List<Sprite> cardImages; 
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI attemptsText;
    public TextMeshProUGUI winText; 
    public Button restartButton;

    private List<Card> cards = new List<Card>();
    private Card firstCard, secondCard;
    private int score = 0;
    private int attempts = 0;
    private bool gameWon = false; 

    public AudioSource audiosource;
    public AudioClip correctclip;
    public AudioClip Wrongclip;

    private void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        restartButton.gameObject.SetActive(false);
        winText.gameObject.SetActive(false); 
        SetupGame();
    }

    private void SetupGame()
    {
        List<Sprite> shuffledSprites = new List<Sprite>(cardImages);
        shuffledSprites.AddRange(cardImages); 
        shuffledSprites = ShuffleList(shuffledSprites);

        for (int i = 0; i < shuffledSprites.Count; i++)
        {
            GameObject newCard = Instantiate(cardPrefab, gridLayout.transform);
            Card cardScript = newCard.GetComponent<Card>();
            cardScript.SetCard(shuffledSprites[i]);
            cards.Add(cardScript);
        }
    }

    public void CardClicked(Card clickedCard)
    {
        if (gameWon) return; 

        if (firstCard == null)
        {
            firstCard = clickedCard;
            firstCard.FlipCard(true);
        }
        else if (secondCard == null && clickedCard != firstCard)
        {
            secondCard = clickedCard;
            secondCard.FlipCard(true);
            attempts++;
            attemptsText.text = "Attempts: " + attempts;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1f);

        if (firstCard.frontSprite == secondCard.frontSprite) 
        {
            score += 10;
            firstCard.gameObject.SetActive(false);
            secondCard.gameObject.SetActive(false);

            audiosource.PlayOneShot(correctclip);

            if (score >= 80)
            {
                gameWon = true;
                WinGame();
            }
        }
        else
        {
            audiosource.PlayOneShot(Wrongclip);
            firstCard.FlipCard(false);
            secondCard.FlipCard(false);
        }

        firstCard = null;
        secondCard = null;
        scoreText.text = "Score: " + score;
    }

    private void WinGame()
    {
        winText.text = "You Win!";
        winText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    private List<Sprite> ShuffleList(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }

    private void RestartGame()
    {
        foreach (Card card in cards)
        {
            Destroy(card.gameObject);
        }
        cards.Clear();
        score = 0;
        attempts = 0;
        gameWon = false;
        scoreText.text = "Score: 0";
        attemptsText.text = "Attempts: 0";
        winText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        SetupGame();
    }
}
