using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override MoveInfo[] GetMoves()
    {
        // --- TODO ---
        return new MoveInfo[]
      {
            new MoveInfo(1, 1, Utils.FieldWidth),  // 오른쪽 위
            new MoveInfo(1, -1, Utils.FieldWidth), // 오른쪽 아래
            new MoveInfo(-1, 1, Utils.FieldWidth), // 왼쪽 위
            new MoveInfo(-1, -1, Utils.FieldWidth) // 왼쪽 아래
      };

    }
}