using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Analytics;

public class PuzzleScript : MonoBehaviour
{
    [SerializeField] GameObject finishMenu;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] private Transform emptyspace = null;
    [SerializeField] private PieceScript[] tiles;
    [SerializeField] private GameObject endPanel;
    float elapsedTime;
    private Camera cam;
    public string sceneName;

    private int emptySpaceIndex; // Added a variable to keep track of the empty space index

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = -cam.transform.position.z;
            Vector2 worldMousePosition = cam.ScreenToWorldPoint(mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(worldMousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                if (Vector2.Distance(emptyspace.position, hit.transform.position) < 4)
                {
                    Vector2 lastEmptySpacePosition = emptyspace.position;
                    PieceScript thisTile = hit.transform.GetComponent<PieceScript>();
                    emptyspace.position = thisTile.targetPosition;
                    thisTile.targetPosition = lastEmptySpacePosition;

                    int tileIndex = FindIndex(thisTile);
                    tiles[emptySpaceIndex] = thisTile;
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                }
                Debug.Log("Puzzle Move");
            }
           
        }
        int correctTiles = 0;
        foreach (var a in tiles)
        {
            if (a != null)
            {
                if (a.inRightPlace)
                    correctTiles++;
            }
            
        }
        if (correctTiles == tiles.Length - 1)
        {
            Time.timeScale = 0;
            finishMenu.SetActive(true);
            //Debug.Log("Puzzle Solved");
            Debug.Log(message: $"Your Time is {timerText.text}", gameObject);
            Debug.Log(message: $"Level {sceneName} Finished", gameObject);
            //endPanel.SetActive(true);
        }

        elapsedTime += Time.deltaTime;
        timerText.text = elapsedTime.ToString();
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    public void Shuffle()
    {
        //SceneManager.LoadScene(sceneName);
        emptySpaceIndex = tiles.Length - 1; // Initialize the empty space index to the last index
        if (emptySpaceIndex != 8)
        {
            var tileOn8LasPos = tiles[8].targetPosition;
            tiles[8].targetPosition = emptyspace.position;
            emptyspace.position = tileOn8LasPos;
            tiles[emptySpaceIndex] = tiles[8];
            tiles[8] = null;
            emptySpaceIndex = 8;
        }
        int invertion;
        do
        {
            for (int i = 0; i < 8; i++)
            {
                if (tiles[i] != null)
                {
                    var lastPos = tiles[i].targetPosition;
                    int randomIndex = Random.Range(0, 8);
                    tiles[i].targetPosition = tiles[randomIndex].targetPosition;
                    tiles[randomIndex].targetPosition = lastPos;
                    var tile = tiles[i];
                    tiles[i] = tiles[randomIndex];
                    tiles[randomIndex] = tile;

                    if (tiles[i] == null)
                    {
                        emptySpaceIndex = i; // Update the empty space index if the current tile is null
                    }
                }
            }
            invertion = GetInversions();
            Debug.Log("Puzzle Shuffled");
            
        } while (invertion%2 !=0);
    }

    public int FindIndex(PieceScript ts)
    {
        for (int i = 0; i < tiles.Length; i++)
        {
            if (tiles[i] != null && tiles[i] == ts)
            {
                return i;
            }
        }
        return -1;
    }

    int GetInversions()
    {
        int inversionsSum = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            int thisTileInvertion = 0;
            for (int j = i; j < tiles.Length; j++)
            {
                if (tiles[j] != null)
                {
                    if (tiles[i].number > tiles[j].number)
                    {
                        thisTileInvertion++;
                    }
                }
            }
            inversionsSum += thisTileInvertion;
        }
        return inversionsSum;
    }

    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1;
    }

}
