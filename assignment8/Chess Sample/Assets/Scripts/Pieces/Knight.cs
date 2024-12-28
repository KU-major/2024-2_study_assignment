using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Knight.cs
public class Knight : Piece
{
    public override MoveInfo[] GetMoves()
    {
        return new MoveInfo[]
      {
            new MoveInfo(2, 1, 1),   // 오른쪽으로 두 칸, 위로 한 칸
            new MoveInfo(2, -1, 1),  // 오른쪽으로 두 칸, 아래로 한 칸
            new MoveInfo(-2, 1, 1),  // 왼쪽으로 두 칸, 위로 한 칸
            new MoveInfo(-2, -1, 1), // 왼쪽으로 두 칸, 아래로 한 칸
            new MoveInfo(1, 2, 1),   // 위로 두 칸, 오른쪽으로 한 칸
            new MoveInfo(1, -2, 1),  // 아래로 두 칸, 오른쪽으로 한 칸
            new MoveInfo(-1, 2, 1),  // 위로 두 칸, 왼쪽으로 한 칸
            new MoveInfo(-1, -2, 1)  // 아래로 두 칸, 왼쪽으로 한 칸
      };
    
    }
}