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
    


    // Start is called before the first frame update
    void Start()
    {
        List<Material> mats = GetRandomMaterials();

        for (var i = 0; i < 6; i++)
        {
            GameObject obj = Instantiate(card, new Vector3(i * offset + startX, startY, 0), Quaternion.identity);
            Card c = obj.GetComponent<Card>();
            c.SetBackMaterial(back);
            c.SetFrontMaterial(mats[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<Material> GetRandomMaterials()
    {
        List<Material> mats = new List<Material>();

        int notUsing = Random.Range(0, materials.Count);

        for(int i = 0; i < materials.Count * 2; i++)
        {
            if(i % materials.Count != notUsing)
                mats.Add(materials[i % materials.Count]);
        }

        // int j = 0;
        // while(j < 3)
        // {
        //     materials temp;


        //     j++;
        // }


        return mats;
    }

}
