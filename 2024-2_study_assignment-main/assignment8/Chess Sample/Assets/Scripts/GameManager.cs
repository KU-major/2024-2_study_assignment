using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject TilePrefab;
    public GameObject[] PiecePrefabs;
    public GameObject EffectPrefab;

    private Transform TileParent;
    private Transform PieceParent;
    private Transform EffectParent;
    private MovementManager movementManager;
    private UIManager uiManager;

    public int CurrentTurn = 1;
    public Tile[,] Tiles = new Tile[Utils.FieldWidth, Utils.FieldHeight];
    public Piece[,] Pieces = new Piece[Utils.FieldWidth, Utils.FieldHeight];

    void Awake()
    {
        TileParent = GameObject.Find("TileParent").transform;
        PieceParent = GameObject.Find("PieceParent").transform;
        EffectParent = GameObject.Find("EffectParent").transform;

        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        movementManager = gameObject.AddComponent<MovementManager>();
        movementManager.Initialize(this, EffectPrefab, EffectParent);

        InitializeBoard();
    }

    void InitializeBoard()
    {
        // 8x8로 타일들을 배치
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            for (int y = 0; y < Utils.FieldHeight; y++)
            {
                var tile = Instantiate(TilePrefab, TileParent);
                tile.transform.position = new Vector3(x, y, 0);
                var tileScript = tile.GetComponent<Tile>();
                tileScript.Set((x, y));
                Tiles[x, y] = tileScript;
            }
        }

        // 말 배치
        PlacePieces(1);  // White (위쪽 플레이어)
        PlacePieces(-1); // Black (아래쪽 플레이어)
    }

    void PlacePieces(int direction)
    {
        int backRow = direction == 1 ? 0 : 7;
        int pawnRow = direction == 1 ? 1 : 6;

        // 폰 배치
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            PlacePiece(5, (x, pawnRow), direction); // 0: Pawn
        }

        // Back row 배치
        PlacePiece(4, (0, backRow), direction); // 4: Rook (좌측 구석)
        PlacePiece(4, (7, backRow), direction); // 4: Rook (우측 구석)

        PlacePiece(2, (1, backRow), direction); // 2: Knight
        PlacePiece(2, (6, backRow), direction); // 2: Knight

        PlacePiece(3, (2, backRow), direction); // 3: Bishop
        PlacePiece(3, (5, backRow), direction); // 3: Bishop

        PlacePiece(1, (3, backRow), direction); // 4: Queen
        PlacePiece(0, (4, backRow), direction); // 5: King
    }

    Piece PlacePiece(int pieceType, (int, int) pos, int direction)
    {
        var pieceObj = Instantiate(PiecePrefabs[pieceType], PieceParent);
        pieceObj.transform.position = new Vector3(pos.Item1, pos.Item2, 0);

        var pieceScript = pieceObj.GetComponent<Piece>();
        pieceScript.initialize(pos, direction);
        Pieces[pos.Item1, pos.Item2] = pieceScript;

        return pieceScript;
    }
    public bool IsValidMove(Piece piece, (int, int) targetPos)
    {
        return movementManager.IsValidMove(piece, targetPos);
    }

    public void ShowPossibleMoves(Piece piece)
    {
        movementManager.ShowPossibleMoves(piece);
    }

    public void ClearEffects()
    {
        movementManager.ClearEffects();
    }

    public void Move(Piece piece, (int, int) targetPos)
    {
        if (!IsValidMove(piece, targetPos)) return;

        Piece targetPiece = Pieces[targetPos.Item1, targetPos.Item2];
        if (targetPiece != null)
        {
            Destroy(targetPiece.gameObject); // 상대 말을 제거
        }

        Pieces[piece.MyPos.Item1, piece.MyPos.Item2] = null;
        piece.MoveTo(targetPos);
        Pieces[targetPos.Item1, targetPos.Item2] = piece;

        ChangeTurn();
    }

    void ChangeTurn()
    {
        CurrentTurn *= -1; // 턴 변경
        uiManager.UpdateTurn(CurrentTurn); // UI 갱신
    }
}

