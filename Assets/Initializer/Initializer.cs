using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Initializer
{

    private static string CARDS_RESOURCE_PATH = @"Cards/Cards";
    private static string CARD_DECKS_RESOURCE_PATH = @"Cards/Decks";
    private static string ENCOUNTERS_RESOURCE_PATH = @"Enemies/Encounters";
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void OnBeforeSceneLoad()
    {

        Debug.Log("== Running Initializer!");

        LoadCards();

        LoadCardDecks();

        LoadEncounters();

        Debug.Log("== Finished initializing game data!");

    }

    static void LoadCards()
    {
        CardDatabase cardDatabase = new CardDatabase();
        foreach (var cardGeneric in Resources.LoadAll(CARDS_RESOURCE_PATH, typeof(BaseCard)))
        {
            BaseCard card = cardGeneric as BaseCard;
            card.Initialize();
            cardDatabase.AddCard(card);
        }

        DI.Set<CardDatabase>(cardDatabase);
        Debug.Log("=== Finished LoadCards");
    }

    static void LoadCardDecks()
    {
        foreach (var deckGeneric in Resources.LoadAll(CARD_DECKS_RESOURCE_PATH, typeof(CardDeck)))
        {
            CardDeck deck = deckGeneric as CardDeck;
            deck.Prepare();
        }
        Debug.Log("=== Finished LoadCardDecks");
    }

    static void LoadEncounters()
    {
        EncounterManager encounterManager = new EncounterManager();
        foreach (var encounterGeneric in Resources.LoadAll(ENCOUNTERS_RESOURCE_PATH, typeof(Encounter)))
        {
            Encounter encounter = encounterGeneric as Encounter;
            encounterManager.AddEncounter(encounter);
        }
        DI.Set<EncounterManager>(encounterManager);
        Debug.Log("=== Finished LoadCardDecks");
    }

}
