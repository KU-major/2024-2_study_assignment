using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private GameManager gameManager;
    private Piece selectedPiece = null;
    private Vector3 dragOffset;
    private Vector3 originalPosition;
    private bool isDragging = false;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    // 마우스의 위치를 (int, int) 좌표로 보정해주는 함수
    private (int, int) GetBoardPosition(Vector3 worldPosition)
    {
        float x = worldPosition.x + (Utils.TileSize * Utils.FieldWidth) / 2f;
        float y = worldPosition.y + (Utils.TileSize * Utils.FieldHeight) / 2f;
        
        int boardX = Mathf.FloorToInt(x / Utils.TileSize);
        int boardY = Mathf.FloorToInt(y / Utils.TileSize);
        
        return (boardX, boardY);
    }

    void HandleMouseDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var boardPos = GetBoardPosition(mousePosition);

        if (!Utils.IsInBoard(boardPos)) return;
        // 클릭된 piece을 검증하고, 가능한 이동 경로를 표시
        // --- TODO ---
        var piece = gameManager.Pieces[boardPos.Item1, boardPos.Item2];
        if (piece != null && piece.PlayerDirection == gameManager.CurrentTurn)
        {
            selectedPiece = piece;
            originalPosition = selectedPiece.transform.position;
            dragOffset = originalPosition - mousePosition;
            isDragging = true;

            // 가능한 이동 경로를 표시
            gameManager.ShowPossibleMoves(selectedPiece);
        }
        // ------
        Piece clickedPiece = gameManager.Pieces[boardPos.Item1, boardPos.Item2];
        if (clickedPiece != null && clickedPiece.PlayerDirection == gameManager.CurrentTurn)
        {
            selectedPiece = clickedPiece;
            isDragging = true;
            dragOffset = selectedPiece.transform.position - mousePosition;
            dragOffset.z = 0;
            originalPosition = selectedPiece.transform.position;
            
            gameManager.ShowPossibleMoves(selectedPiece);
        }
    }

    void HandleDrag()
    {
        if (selectedPiece != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            selectedPiece.transform.position = mousePosition + dragOffset;
        }
    }

    void HandleMouseUp()
    {
        if (selectedPiece != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var boardPos = GetBoardPosition(mousePosition);

            // piece의 이동을 검증하고, 이동시킴
            // effect를 초기화
            // --- TODO ---
            if (Utils.IsInBoard(boardPos) && gameManager.IsValidMove(selectedPiece, boardPos))
            {
                gameManager.Move(selectedPiece, boardPos); // Move the piece
            }
            else
            {
                // Return the piece to its original position if the move is invalid
                selectedPiece.transform.position = originalPosition;
            }

            // 효과 제거
            gameManager.ClearEffects();
            selectedPiece = null;
            isDragging = false;
            // ------
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseDown();
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            HandleDrag();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            HandleMouseUp();
        }
    }
}