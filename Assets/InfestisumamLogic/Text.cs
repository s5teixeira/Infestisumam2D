using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public Text message;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            message.gameObject.SetActive(true);
        }
    }

}