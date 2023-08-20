using UnityEngine;

public class Reposition : MonoBehaviour
{
    private Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) {
            return;
        }

        if (GameManager.Instance == null) return;
        if (GameManager.Instance.player == null) return;
        Vector3 playerPosition = GameManager.Instance.player.transform.position;
        Vector3 mapPosition = transform.position;
        float diffX = Mathf.Abs(playerPosition.x - mapPosition.x);
        float diffY = Mathf.Abs(playerPosition.y - mapPosition.y);

        Vector3 playerDir = GameManager.Instance.player.InputVector;
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if(diffX > diffY) {
                    transform.Translate(Vector3.right * dirX * 80f);
                }
                else if(diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY * 80f);
                }
                break;
            case "Enemy":
                if (coll.enabled) {
                    transform.Translate(playerDir * 40 + new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f));
                }
                break;
        }
    }
}
