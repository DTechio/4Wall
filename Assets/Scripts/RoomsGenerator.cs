using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomsGenerator : MonoBehaviour
{
    public GameObject verWallPrefab;
    public GameObject horWallPrefab;
    public GameObject doorPrefab;
    public GameObject roomPrefab;
    public Vector2 playerStartPos; // Position to place the player in the new room

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Determine the direction of the door
            Vector2 direction = other.transform.position - transform.position;

            // Create a new room in that direction
            CreateRoom(direction);

            // Move the player to the new room
            other.transform.position = playerStartPos;
        }
    }

    private void CreateRoom(Vector2 direction)
    {
        // Instantiate room, walls and doors based on the direction
        GameObject newRoom = Instantiate(roomPrefab, new Vector2(0, 0), Quaternion.identity);

        GameObject wallNorth = Instantiate(horWallPrefab, new Vector2(0, 4.3f), Quaternion.identity);
        GameObject wallSouth = Instantiate(horWallPrefab, new Vector2(0, -5), Quaternion.identity);
        GameObject wallEast = Instantiate(verWallPrefab, new Vector2(7.7f, 0), Quaternion.identity);
        GameObject wallWest = Instantiate(verWallPrefab, new Vector2(-7.7f, 0), Quaternion.identity);

        GameObject doorNorth = Instantiate(doorPrefab, new Vector2(0, 4), Quaternion.identity);
        GameObject doorSouth = Instantiate(doorPrefab, new Vector2(0, -4.5f), Quaternion.identity);
        GameObject doorEast = Instantiate(doorPrefab, new Vector2(7, -0.3f), Quaternion.identity);
        GameObject doorWest = Instantiate(doorPrefab, new Vector2(-7, -0.3f), Quaternion.identity);

        // Disable the walls and doors not needed based on the direction
        if (direction == Vector2.up)
        {
            wallSouth.SetActive(false);
            doorSouth.SetActive(false);
            Debug.Log("Up");
        }
        else if (direction == Vector2.down)
        {
            wallNorth.SetActive(false);
            doorNorth.SetActive(false);
        }
        else if (direction == Vector2.left)
        {
            wallEast.SetActive(false);
            doorEast.SetActive(false);
        }
        else if (direction == Vector2.right)
        {
            wallWest.SetActive(false);
            doorWest.SetActive(false);
        }
    }
}



//public Vector2 destination = new Vector2(2, 2); // Posição inicial na próxima sala

//private void OnTriggerEnter2D(Collider2D other)
//{
//    if (other.CompareTag("Player"))
//    {
//        other.transform.position = destination;
//    }
//}

