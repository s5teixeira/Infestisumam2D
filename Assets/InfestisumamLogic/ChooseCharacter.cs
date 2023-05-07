using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;

    private int selectedCharacter = 0;

    void Start()
    {
        SpawnCharacter();
    }

    public void SelectCharacter(int index)
    {
        selectedCharacter = index;
        DestroyCharacters();
        SpawnCharacter();
    }


}