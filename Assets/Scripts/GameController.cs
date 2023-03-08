using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public bool playerInControl = true; //true for white | false for black
    public GameObject selectedPiece;

    public GameObject kingWhite;
    public GameObject kingBlack;

    public GameObject[,] boardLayout = new GameObject[8, 8];
    public int[,] dangerLayoutWhite = new int[8, 8];
    public int[,] dangerLayoutBlack = new int[8, 8];

    public void initialize() {
        
    }

    public void reCalculateDangers() {
        foreach (GameObject piece in boardLayout) {
            if (piece != null) {
                piece.GetComponent<PieceController>().unmarkDangerSpots();
                piece.GetComponent<PieceController>().markDangerSpots();
            }
        }

        Debug.Log("----------------");
        Debug.Log("WHITE");
        for (int x = 7; x >= 0; x--) {
            string temp = "";
            for (int y = 7; y >= 0; y--) {
                temp += dangerLayoutWhite[y, x] + " ";
            }
            Debug.Log(temp);
        }

        Debug.Log("BLACK");
        for (int x = 7; x >= 0; x--) {
            string temp = "";
            for (int y = 7; y >= 0; y--) {
                temp += dangerLayoutBlack[y, x] + " ";
            }
            Debug.Log(temp);
        }
    }

    public void swapControl() {
        playerInControl = !playerInControl;
        Debug.Log(playerInControl ? "White's Turn Starts" : "Black's Turn Starts");

        if (selectedPiece != null) { selectedPiece.GetComponent<PieceController>().HighlightToggle(); }



        selectedPiece = null;
    }

    public GameObject findPiece(int column, int row) {
        try {
            return boardLayout[column - 1, row - 1];
        } catch {
            return null;
        }
    }

    public bool isDangerSpot(int column, int row, char pieceColor) {
        return (pieceColor == 'W' ? dangerLayoutBlack[column - 1, row - 1] : dangerLayoutWhite[column - 1, row - 1]) > 0;
    }

    public void raiseSpotDanger(int column, int row, char pieceColor) {
        try {
            if (pieceColor == 'W') {
                dangerLayoutWhite[column - 1, row - 1]++;
            } else {
                dangerLayoutBlack[column - 1, row - 1]++;
            }
        }
        catch { return; }
    }

    public void lowerSpotDanger(int column, int row, char pieceColor) {
        try {
            if (pieceColor == 'W') {
                dangerLayoutWhite[column - 1, row - 1]--;
            } else {
                dangerLayoutBlack[column - 1, row - 1]--;
            }
        } catch { return; }
    }

    public bool recordPieceMove(GameObject piece, int oldColumn, int oldRow, int newColumn, int newRow) {
        boardLayout[oldColumn - 1, oldRow - 1] = null;

        reCalculateDangers();

        if (!piece.GetComponent<PieceController>().isValidMove(newColumn, newRow)) {
            boardLayout[oldColumn - 1, oldRow - 1] = piece;
            return false;
        }

        if (findPiece(newColumn, newRow) != null) { Destroy(findPiece(newColumn, newRow)); }
            

        /*
        var pieceHolder = gameController.boardLayout[(int) cordinates[0], (int) cordinates[2]];
        gameController.boardLayout[(int) cordinates[0], (int) cordinates[2]] = null;
        gameController.reCalculateDangers();
        */

        piece.GetComponent<PieceController>().column = newColumn;
        piece.GetComponent<PieceController>().row = newRow;

        boardLayout[newColumn - 1, newRow - 1] = piece;

        return true;
    }
}
