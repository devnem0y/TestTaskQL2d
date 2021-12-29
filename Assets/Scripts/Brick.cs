using UnityEngine;

public class Brick : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Ball"))
        {
            AudioManager.instance.PlaySound("fx_0");
            
            Dispatcher.Send(Event.ON_SCORE_ADD);
            Dispatcher.Send(Event.ON_BRICK_DESTROY);
            Destroy(gameObject);
        }
    }
}
