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
            new MoveInfo(1, 1, 1),  // �밢��
            new MoveInfo(1, -1, 1),
            new MoveInfo(-1, 1, 1),
            new MoveInfo(-1, -1, 1),
            new MoveInfo(1, 0, 1),  // �����¿�
            new MoveInfo(-1, 0, 1),
            new MoveInfo(0, 1, 1),
            new MoveInfo(0, -1, 1)
        };
        // ------
    }
}