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

    private void SpawnCharacter()
    {
        Instantiate(characters[selectedCharacter], transform.position, Quaternion.identity);
    }
    private void DestroyCharacters()
    {
        GameObject[] existingCharacters = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject character in existingCharacters)
        {
            Destroy(character);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameCoordinator");
    }

}