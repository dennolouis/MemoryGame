using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject card;

    [SerializeField] Material back;

    [SerializeField] List<Material> materials;

    [SerializeField] float startX = -1.5f;
    [SerializeField] float startY = 0f;
    [SerializeField] float offset = 1f;

    [SerializeField] float delayTime = 0.5f;

    Card firstCard;
    Card secondCard;



    enum GameState
    {
        FlipFirstCard,
        FlipSecondCard,
        CheckMatch,
        FlipBack
    }

    [SerializeField] GameState state;

    // Start is called before the first frame update
    void Start()
    {
        state = GameState.FlipFirstCard;

        SpawnCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCards()
    {
        List<int> mats = GetRandomMaterialIndexes();

        for (var j = 0; j < 6; j++)
        {
            GameObject obj = Instantiate(card, new Vector3(j * offset + startX, startY, 0), Quaternion.identity);
            Card c = obj.GetComponent<Card>();
            c.SetBackMaterial(back);
            c.SetFrontMaterial(materials[mats[j]]);
        }
    }

    List<int> GetRandomMaterialIndexes()
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
    }

    IEnumerator HandleMatch()
    {
        yield return new WaitForSeconds(delayTime);
        if (firstCard.GetFront() == secondCard.GetFront())
        {
            print("match");
            Destroy(firstCard.gameObject);
            Destroy(secondCard.gameObject);
        }
        else
        {
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
        yield return new WaitForSeconds(delayTime);
        state = GameState.FlipFirstCard;
    }

    public bool FlipCardsback()
    {
        return state == GameState.FlipBack;
    }

    public bool CanFlip()
    {
        return state != GameState.CheckMatch;
    }

}