using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject effectPrefab;
    private Transform effectParent;
    private List<GameObject> currentEffects = new List<GameObject>();
    
    public void Initialize(GameManager gameManager, GameObject effectPrefab, Transform effectParent)
    {
        this.gameManager = gameManager;
        this.effectPrefab = effectPrefab;
        this.effectParent = effectParent;
    }

    private bool TryMove(Piece piece, (int, int) targetPos, MoveInfo moveInfo)
    {
        // moveInfo의 distance만큼 direction을 이동시키며 이동이 가능한지를 체크
        // 보드에 있는지, 다른 piece에 의해 막히는지 등을 체크
        // 폰에 대한 예외 처리를 적용
        // --- TODO ---
        (int dirX, int dirY) = (moveInfo.dirX, moveInfo.dirY);
        int distance = moveInfo.distance;

        for (int step = 1; step <= distance; step++)
        {
            (int newX, int newY) = (piece.MyPos.Item1 + step * dirX, piece.MyPos.Item2 + step * dirY);

            if (!Utils.IsInBoard((newX, newY)))
                return false; // 보드 밖으로 나가면 이동 불가

            var targetPiece = gameManager.Pieces[newX, newY];
            if (targetPiece != null)
            {
                if (step == distance && targetPiece.PlayerDirection != piece.PlayerDirection)
                    return true; // 마지막 위치에 적이 있으면 이동 가능
                else
                    return false; // 중간에 다른 말이 있으면 이동 불가
            }
        }

        return true; // 이동 가능  
        // ------
    }
    private bool IsValidMoveWithoutCheck(Piece piece, (int, int) targetPos)
    {
        if (!Utils.IsInBoard(targetPos) || targetPos == piece.MyPos) return false;

        foreach (var moveInfo in piece.GetMoves())
        {
            if (TryMove(piece, targetPos, moveInfo))
                return true;
        }
        
        return false;
    }

    public bool IsValidMove(Piece piece, (int, int) targetPos)
    {
        if (!IsValidMoveWithoutCheck(piece, targetPos)) return false;

        // 체크 상태 검증을 위한 임시 이동
        var originalPiece = gameManager.Pieces[targetPos.Item1, targetPos.Item2];
        var originalPos = piece.MyPos;

        gameManager.Pieces[targetPos.Item1, targetPos.Item2] = piece;
        gameManager.Pieces[originalPos.Item1, originalPos.Item2] = null;
        piece.MyPos = targetPos;

        bool isValid = !IsInCheck(piece.PlayerDirection);

        // 원상 복구
        gameManager.Pieces[originalPos.Item1, originalPos.Item2] = piece;
        gameManager.Pieces[targetPos.Item1, targetPos.Item2] = originalPiece;
        piece.MyPos = originalPos;

        return isValid;
    }
    private bool IsInCheck(int playerDirection)
    {
        (int, int) kingPos = (-1, -1); // 왕의 위치
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            for (int y = 0; y < Utils.FieldHeight; y++)
            {
                var piece = gameManager.Pieces[x, y];
                if (piece is King && piece.PlayerDirection == playerDirection)
                {
                    kingPos = (x, y);
                    break;
                }
            }
            if (kingPos.Item1 != -1 && kingPos.Item2 != -1) break;
        }

        // --- TODO ---
        // 상대방 말들의 움직임 확인
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            for (int y = 0; y < Utils.FieldHeight; y++)
            {
                var piece = gameManager.Pieces[x, y];
                if (piece != null && piece.PlayerDirection != playerDirection)
                {
                    MoveInfo[] moves = piece.GetMoves(); // GetMoves() 호출 결과를 명시적으로 배열에 저장
                    for (int i = 0; i < moves.Length; i++)
                    {
                        var moveInfo = moves[i];
                        for (int step = 1; step <= moveInfo.distance; step++)
                        {
                            (int targetX, int targetY) = (x + step * moveInfo.dirX, y + step * moveInfo.dirY);

                            if (!Utils.IsInBoard((targetX, targetY)))
                                break;

                            if (gameManager.Pieces[targetX, targetY] is King &&
                                gameManager.Pieces[targetX, targetY].PlayerDirection == playerDirection)
                            {
                                return true;
                            }

                            if (gameManager.Pieces[targetX, targetY] != null)
                                break; 
                        }
                    }
                }
            }
        }

        return false;
    }

    public void ShowPossibleMoves(Piece piece)
    {
        ClearEffects();

        // 가능한 움직임을 표시
        // --- TODO ---
        foreach (var moveInfo in piece.GetMoves())
        {
            for (int step = 1; step <= moveInfo.distance; step++)
            {
                (int targetX, int targetY) = (piece.MyPos.Item1 + step * moveInfo.dirX,
                                              piece.MyPos.Item2 + step * moveInfo.dirY);

                if (!Utils.IsInBoard((targetX, targetY)))
                    break; // 보드 밖으로 나가면 중지

                var targetPiece = gameManager.Pieces[targetX, targetY];
                if (targetPiece != null && targetPiece.PlayerDirection == piece.PlayerDirection)
                    break; // 아군 말이 있으면 중지

                var effect = Instantiate(effectPrefab, effectParent);
                effect.transform.position = new Vector3(targetX, targetY, 0);
                currentEffects.Add(effect);

                if (targetPiece != null && targetPiece.PlayerDirection != piece.PlayerDirection)
                    break; // 적이 있으면 중지
            }
        }
        // ------
    }

    public void ClearEffects()
    {
        foreach (var effect in currentEffects)
        {
            if (effect != null) Destroy(effect);
        }
        currentEffects.Clear();
    }
}