using UnityEngine;
using Player.PlayerController; 

public class TriggerDetector : MonoBehaviour
{
    private PlayerController playerController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerController = other.GetComponent<PlayerController>();
        if ( playerController != null)
        {
        
            playerController.SetCanExchange(true); 
            //Debug.Log("Player is near an NPC exchange stand.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerController = other.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.SetCanExchange(false); 
            playerController.SetExchangeToggle(false);
            //Debug.Log("Player is no longer near an NPC exchange stand.");
        }
    
    }
}
