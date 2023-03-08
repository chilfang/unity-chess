using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class CameraController : MonoBehaviour {
    private GameObject board; //board square size 3.12 (half size: 1.56)
    private GameController gameController;
    
    public float speed;

    private bool PanLeftCheck = false;
    private bool PanRightCheck = false;

    private bool firstClick = true;

    void Start() {
        if (board == null) {
            board = GameObject.Find("Board");
            gameController = board.GetComponent<GameController>();
        }
    }

    void Update() {
        if (PanLeftCheck) {transform.Rotate(0, speed * Time.deltaTime, 0);}
        if (PanRightCheck) {transform.Rotate(0, speed * Time.deltaTime * -1, 0);}
    }
    
    
    public void PanLeft(InputAction.CallbackContext context) {
        PanLeftCheck = context.performed;
    }
    public void PanRight(InputAction.CallbackContext context) {
        PanRightCheck = context.performed;
    }

    public void Sprint(InputAction.CallbackContext context) {
        if (context.performed) {
            speed = 80;
        } else {
            speed = 20;
        }
    }

    public void ClickDetection(InputAction.CallbackContext context) {
        if (context.canceled) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit) && hit.collider != null) {
                if (firstClick) { gameController.initialize(); firstClick = false; }

                var hitObject = hit.collider.gameObject;
                if (hitObject == board && gameController.selectedPiece != null) { //board hit while a piece is selected
                    var cordinates = hit.point;

                    //adjust for board poisiton
                    cordinates[0] -= board.transform.position.x;
                    cordinates[2] -= board.transform.position.z;

                    //adjust for board origin offset
                    cordinates[0] += (3.12f * 5);
                    cordinates[2] += (3.12f * 5);

                    //translate into board square units
                    cordinates[0] /= 3.12f;
                    cordinates[2] /= 3.12f;

                    //round to integers
                    cordinates[0] = (float) Math.Round(cordinates[0]);
                    cordinates[2] = (float) Math.Round(cordinates[2]);

                    //fix x by flipping 
                    cordinates[0] -= 9;
                    cordinates[0] *= -1;

                    
                    //make sure cordinates are within limits
                    if (cordinates[0] > 8) { cordinates[0] = 8; }
                    if (cordinates[2] > 8) { cordinates[2] = 8; }
                    if (cordinates[0] < 1) { cordinates[0] = 1; }
                    if (cordinates[2] < 1) { cordinates[2] = 1; }



                    var pieceController = gameController.selectedPiece.GetComponent<PieceController>();
                    //Do checks and record move
                    if (gameController.recordPieceMove(gameController.selectedPiece, pieceController.column, pieceController.row, (int) cordinates[0], (int) cordinates[2])) {
                        //run piece move function
                        pieceController.moveToPosition();

                        //switch to other player
                        gameController.swapControl();

                        /* too much work
                        //check if king in check
                        KingController kingPiece = (gameController.playerInControl ? gameController.kingWhite : gameController.kingBlack).GetComponent<KingController>();
                        if (kingPiece.inCheck()) {

                            //check if king in checkmate
                            for (int x = 0; x < 3; x++) {
                                for (int y = 0; y < 3; y++) {
                                    if (gameController.findPiece(kingPiece.column - 1 + x, kingPiece.row - 1 + y) != null) { continue; }
                                    if (!kingPiece.inCheck(kingPiece.column - 1 + x, kingPiece.row - 1 + y)) {
                                        kingPiece.RedHighlightToggle();
                                        return;
                                    }
                                }
                            }
                        }
                        */
                    }
                    


                } else if (hitObject.GetComponent<Rigidbody>() != null) { //piece hit
                    //hitObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 1, 0) * hitObject.GetComponent<Rigidbody>().mass * 8, ForceMode.Impulse);

                    if ((hitObject.GetComponent<PieceController>().pieceColor == 'W') == gameController.playerInControl) {
                        if (gameController.selectedPiece != null) { gameController.selectedPiece.GetComponent<PieceController>().HighlightToggle(); }
                        hitObject.GetComponent<PieceController>().HighlightToggle();

                        gameController.selectedPiece = hitObject;
                    }
                }
            }

        }
    }
}
