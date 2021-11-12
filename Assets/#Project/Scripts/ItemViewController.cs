using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemViewController : MonoBehaviour
{
    public Inventory inventoryHolder; //references a Inventory qui contient ItemData
    public Transform inventoryViewParent; //references à Transform pour créer des objets ItemView en tant qu'enfant de Inventory view dans la scène
    public GameObject itemViewPrefab; // L'ItemView sauvegardé dans Prefab
                                      //Item item;

    //private GameObject infoView;

    private void Start()
    {
        UpdateView();
    }
    public void UpdateView()
    {

        //créer une instance de ItemView et on appelle la méthode InitItem pour l'initialiser avec ItemData pour chaque item dans l'inventaire
        foreach (var item in inventoryHolder.inventory)
        {
            var itemGO = GameObject.Instantiate(itemViewPrefab, inventoryViewParent);
            itemGO.GetComponent<ItemView>().InitItem(item);
        }
    }
    void Update()
    {
        //if(item.ItemIsdestroyed == true){
        //   Debug.Log("Item is in inventory !");
        //}
    }
}
