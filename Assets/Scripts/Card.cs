using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Sprite frontSprite; // The sprite for the front of the card
    public Sprite backSprite;  // The sprite for the back of the card
    private Image cardImage;
    private Button button;

    private void Awake()
    {
        cardImage = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnCardClicked);
    }

    public void SetCard(Sprite front)
    {
        frontSprite = front;
        cardImage.sprite = backSprite; // Start hidden
    }

    public void FlipCard(bool showFront)
    {
        cardImage.sprite = showFront ? frontSprite : backSprite;
    }

    private void OnCardClicked()
    {
        FindObjectOfType<GameManager>().CardClicked(this);
    }
}
