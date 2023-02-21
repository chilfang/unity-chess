using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : PieceController {
    public override bool isValidMove(int column, int row) {
        throw new System.NotImplementedException();
    }

    public override void markDangerSpots() {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
    }
}

