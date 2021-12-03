using UnityEngine;

public class Tile : Clickable
{
    public int x { get; protected set; }
    public int y { get; protected set; }
    public Board.Player player { get; set; }
    [SerializeField] private GameObject markX;
    [SerializeField] private GameObject markO;
    public GameObject mark;
    public bool isActive;
    public Board board;

    // variables for floaty movement of the board
    public float timePeriod = 2;
    public float height = 30f;
    private float timeSinceStart;
    private Vector3 pivot;

    private void Start()
    {
        this.player = Board.Player.NONE;
        this.isActive = true;

        pivot = transform.position;
        height /= 2;
        timeSinceStart = (3 * timePeriod) / 4;
    }

    void Update()
    {
        Vector3 nextPos = transform.position;
        nextPos.y = pivot.y + height + height * Mathf.Sin(((Mathf.PI * 2) / timePeriod) * timeSinceStart);
        timeSinceStart += Time.deltaTime;
        transform.position = nextPos;
    }

    public override void onClick()
    {

        Board.Player player = this.board.getCurrentPlayer();
        Debug.Log("player=" + player + ", this.player=" + this.player + ", this.isActive=" + this.isActive);
        if (!this.isActive || this.player != Board.Player.NONE || player == Board.Player.NONE)
        {
            Debug.Log("not executing tile.onClick()");
            return;
        }

        this.player = player;
        GameObject mark = player == Board.Player.X ? this.markX : this.markO;
        this.mark = Instantiate(mark, transform.position + mark.transform.position, mark.transform.rotation, transform);
        this.board.processDecision(this);
    }

    internal void setArrangement(Board board, int x, int y)
    {
        this.board = board;
        this.x = x;
        this.y = y;
    }

    private GameObject getChildByName(Transform parent, string tag)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            GameObject obj = parent.GetChild(i).gameObject;
            if (obj.CompareTag(tag))
            {
                return obj;
            }
        }
        return null;
    }
}
