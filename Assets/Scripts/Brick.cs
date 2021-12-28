using UnityEngine;

public class Brick : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Ball"))
        {
            Dispatcher.Send(Event.ON_SCORE_ADD);
            Dispatcher.Send(Event.ON_BRICK_DESTROY);
            Destroy(gameObject);
        }
    }
}
