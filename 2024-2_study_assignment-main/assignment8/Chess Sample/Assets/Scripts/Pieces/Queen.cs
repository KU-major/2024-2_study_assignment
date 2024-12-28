using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override MoveInfo[] GetMoves()
    {
        // --- TODO ---
        return new MoveInfo[]
        {
            new MoveInfo(1, 1, 1),  // 대각선
            new MoveInfo(1, -1, 1),
            new MoveInfo(-1, 1, 1),
            new MoveInfo(-1, -1, 1),
            new MoveInfo(1, 0, 1),  // 상하좌우
            new MoveInfo(-1, 0, 1),
            new MoveInfo(0, 1, 1),
            new MoveInfo(0, -1, 1)
        };
        // ------
    }
}