using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rook.cs
public class Rook : Piece
{
    public override MoveInfo[] GetMoves()
    {
        return new MoveInfo[]
      {
            new MoveInfo(1, 0, Utils.FieldWidth),  // 오른쪽
            new MoveInfo(-1, 0, Utils.FieldWidth), // 왼쪽
            new MoveInfo(0, 1, Utils.FieldHeight), // 위
            new MoveInfo(0, -1, Utils.FieldHeight) // 아래
      };
    }
}
