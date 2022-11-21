using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject card;

    [SerializeField] Material back;

    [SerializeField] List<Material> materials;

    [SerializeField] Vector3 fourCardScale;
    [SerializeField] float fourOffset;
    [SerializeField] float startX = -1.5f;
    [SerializeField] float startY = 0f;
    [SerializeField] float offset = 1f;

    [SerializeField] float delayTime = 0.5f;

    Card firstCard;
    Card secondCard;

    [SerializeField] int numberOfCards = 0;
    
    enum GameState
    {
        FlipFirstCard,
        FlipSecondCard,
        CheckMatch,
        FlipBack
    }

    enum GameMode
    {
        FourCards,
        SixCards,
        Staggered
    }

    [SerializeField] GameMode mode;

    [SerializeField] GameState state;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        state = GameState.FlipFirstCard;

        SpawnCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCards()
    {
        switch(mode)
        {
            case GameMode.FourCards:
                Spawn4();
                break;
            case GameMode.SixCards:
                Spawn6();
                break;
            case GameMode.Staggered:
                SpawnStaggered();
                break;
        }
    }

    List<int> GetSixMaterialIndexes()
    {
        List<int> mats = new List<int>();

        int notUsing = Random.Range(0, materials.Count);

        for(int i = 0; i < materials.Count * 2; i++)
        {
            if(i % materials.Count != notUsing)
                mats.Add(i % materials.Count);
        }

        int j = 0;
        while(j < 3)
        {
            int rand1 =  Random.Range(0, mats.Count);
            int rand2 =  Random.Range(0, mats.Count);

            int temp = mats[rand1];

            mats[rand1] = mats[rand2];

            mats[rand2] = temp;

            j++;
        }


        return mats;
    }

    List<int> GetFourMaterialIndexes()
    {
        List<int> mats = new List<int>();

        int notUsing = Random.Range(0, materials.Count);
        int notUsing2 = Random.Range(0, materials.Count);

        while(notUsing == notUsing2)
        {
            notUsing2 = Random.Range(0, materials.Count);
        }

        for(int i = 0; i < materials.Count * 2; i++)
        {
            if(i % materials.Count != notUsing && i % materials.Count != notUsing2)
                mats.Add(i % materials.Count);
        }

        int j = 0;
        while(j < 3)
        {
            int rand1 =  Random.Range(0, mats.Count);
            int rand2 =  Random.Range(0, mats.Count);

            int temp = mats[rand1];

            mats[rand1] = mats[rand2];

            mats[rand2] = temp;

            j++;
        }


        return mats;
    }
    
    public void SetCard(Card card)
    {
        switch(state)
        {
            case GameState.FlipFirstCard:
                firstCard = card;
                state = GameState.FlipSecondCard;
                break;
            case GameState.FlipSecondCard:
                if(card == firstCard) return;
                secondCard = card;
                state = GameState.CheckMatch;
                StartCoroutine(HandleMatch());
                break;
        }

        //card.HighLight(true);
    }

    IEnumerator HandleMatch()
    {
        if (firstCard.GetFront() == secondCard.GetFront())
        {
            yield return new WaitForSeconds(delayTime);
            Destroy(firstCard.gameObject);
            Destroy(secondCard.gameObject);
            numberOfCards -= 2;
            player.AddScore(2);
            if(numberOfCards <= 0) SpawnCards();
        }
        else
        {
            yield return new WaitForSeconds(delayTime/3);
            SpawnCards();
        }
        FlibBack();
    }
    void FlibBack()
    {
        state = GameState.FlipBack;

        StartCoroutine(ResetState());
    }

    IEnumerator ResetState()
    {
        yield return new WaitForSeconds(delayTime/3);
        state = GameState.FlipFirstCard;
    }

    public bool FlipCardsback()
    {
        return state == GameState.FlipBack;
    }

    public bool CanFlip()
    {
        return (state != GameState.CheckMatch) && (state != GameState.FlipBack);
    }

    public void UnselectCard()
    {
        if(state == GameState.FlipSecondCard) state = GameState.FlipFirstCard;
    }

    void Spawn4()
    {
        List<int> mats = GetFourMaterialIndexes();

        numberOfCards += 4;

        for(var j = 0; j < 4; j++)
        {
            GameObject obj = Instantiate(card, new Vector3(j * offset * fourOffset + startX, startY, 0), Quaternion.identity);
            obj.transform.localScale = fourCardScale;
            
            Card c = obj.GetComponent<Card>();
            c.SetShiftValue(1.2f);
            c.SetBackMaterial(back);
            c.SetFrontMaterial(materials[mats[j]]);

        
        }
    }

    void Spawn6()
    {
        List<int> mats = GetSixMaterialIndexes();

        numberOfCards += 6;

        for (var j = 0; j < 6; j++)
        {
            GameObject obj = Instantiate(card, new Vector3(j * offset + startX, startY, 0), Quaternion.identity);
            Card c = obj.GetComponent<Card>();
            c.SetBackMaterial(back);
            c.SetFrontMaterial(materials[mats[j]]);
        }
    }

    void SpawnStaggered()
    {
        List<int> mats = GetFourMaterialIndexes();

        numberOfCards += 4;

        int notUsing = Random.Range(0, 6);
        int notUsing2 = Random.Range(0, 6);

        while(notUsing == notUsing2)
        {
            notUsing2 = Random.Range(0, 6);
        }

        for(int j = 0, i = 0; j < 6; j++)
        {
            if(j == notUsing || j == notUsing2) continue;

            GameObject obj = Instantiate(card, new Vector3(j * offset + startX, startY, 0), Quaternion.identity);
            Card c = obj.GetComponent<Card>();
            c.SetBackMaterial(back);
            c.SetFrontMaterial(materials[mats[i++]]);
        }
    }

}