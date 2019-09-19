using UnityEngine;

public class Queen : MonoBehaviour
{

    /*
    Armando Hernández Vargas A01334836
    Andrés Bustamante Díaz A01172912
    Elizabeth Rodríguez Fallas A01334353
         */

    public int index;
    public QueenSpawner queenSpawner;
    public int boardPosition, minPosition, maxPosition, xPos, yPos, contUsedSpaces, newRandPosition;
    public int[] restrictedPos = new int[8];
    public bool isSafe, numberExist, prev, queenMark;

    public void Awake()
    {
        queenSpawner = FindObjectOfType<QueenSpawner>();
    }
    private void Update()
    {
        transform.position = new Vector3(xPos, -1, yPos);
    }

    public void QueenMovement(int newPosition) {

        boardPosition = newPosition;

        //Calculate the position utilizing the 64 array.
        xPos = boardPosition / 8;
        yPos = boardPosition % 8;

        //Check if in the board there is a queen that can attack in that spot.
        isSafe = checkSafe(boardPosition);

        if (isSafe && !queenMark)
        {
            //Mark your place as queen and update the places where you can attack.
            markedArea();
        }
        else {

            //Check the number of different places you have tried.
            if (contUsedSpaces > 7)
            {
                //If there are no new combinations to try, ask for help and then try again.
                resetPosition();
                tryNewPosition();
            }
            else {

                //If the spot is not aviable, check if you have already registered that position.
                numberExist = checkRestriction(boardPosition);

                if (!numberExist)
                {
                    //If you didn't had it registered, add it to the array.
                    restrictedPos[contUsedSpaces] = boardPosition;
                    contUsedSpaces++;
                }

                //Generate a new position and try again.
                int rand = newPos();

                QueenMovement(rand);
            }
            
        }
        
    }

    public int newPos() {

        int r = Random.Range(minPosition, maxPosition+1);

        return r;
    }

    public bool checkRestriction(int currentBoardPosition) {

        //Check your array of known invalid positions and compare it with your current position.
        for (int i = 0; i < restrictedPos.Length; i++) {
            if (currentBoardPosition == restrictedPos[i]) {
                return true;
            }
        }

        return false;
    }

    public void markedArea() {

        //Set that you have stablished your place
        queenMark = true;

        queenSpawner = FindObjectOfType<QueenSpawner>();

        //Mark on the board your current position
        queenSpawner.board[boardPosition] += 1;  //restrict current place 

        //Horizontal restriction
        for (int i = boardPosition + 8; i < 64; i += 8) {
            queenSpawner.board[i] += 1;
        }

        //Diagonal up
        int yDiagonalUp = boardPosition % 8;

        for (int x = boardPosition + 7; x < 64; x += 7) {
            int nextYDiagonal = x % 8;
            
            yDiagonalUp -= 1;

            if (nextYDiagonal == yDiagonalUp) {
                queenSpawner.board[x] += 1;
            }

        }

        //Diagonal down
        int yDiagonalDown = boardPosition % 8;

        for (int x = boardPosition + 9; x < 64; x += 9)
        {
            int nextYDiagonal = x % 8;
            
            yDiagonalDown += 1;

            if (nextYDiagonal == yDiagonalDown)
            {
                queenSpawner.board[x] += 1;
            }

        }
    }

    public bool checkSafe(int currPos) {

        queenSpawner = FindObjectOfType<QueenSpawner>();

        //Check if in your spot on the array there is not a queen that can attack that position.
        if (queenSpawner.board[currPos] > 0)
        {
            return false; // can't locate here 
        }

        return true;
        
    }

    public void clearBoard() {

        //Remove the indicator that your queen have stablished a spot
        queenMark = false;

        queenSpawner = FindObjectOfType<QueenSpawner>();

        queenSpawner.board[boardPosition] -= 1;

        //Horizontal restriction
        for (int i = boardPosition + 8; i < 64; i += 8)
        {
            queenSpawner.board[i] -= 1;
        }

        //Diagonal up

        int yDiagonalUp = boardPosition % 8;

        for (int x = boardPosition + 7; x < 64; x += 7)
        {
            int nextYDiagonal = x % 8;
            yDiagonalUp -= 1;

            if (nextYDiagonal == yDiagonalUp)
            {
                queenSpawner.board[x] -= 1;
                
            }

        }

        //Diagonal down

        int yDiagonalDown = boardPosition % 8;

        for (int x = boardPosition + 9; x < 64; x += 9)
        {
            int nextYDiagonal = x % 8;
            yDiagonalDown += 1;

            if (nextYDiagonal == yDiagonalDown)
            {
                queenSpawner.board[x] -= 1;
                
            }

        }
    }

    public void resetPosition() {  // delete all registers as queen can't locate anywhere and call previous queen 

        for (int j = 0; j < restrictedPos.Length; j++) {
            restrictedPos[j] = 0;
        }

        contUsedSpaces = 0;

        //Ask for the queen with the index before you to try a new combination.
        movePreviousQueen();

    }

    public void tryNewPosition() {

        int newPrevRandom = newPos();
        QueenMovement(newPrevRandom);

    }

    public void movePreviousQueen() {

        //If you are not the first queen and you have more combinations to try ask the previous to move.
        if (index > -1)
        {
            queenSpawner.queenList[index - 1].erasePastRestrictions();
            queenSpawner.queenList[index - 1].tryNewPosition();
        }
        else {
            print("ayuda");
        }
        
    }

    public void erasePastRestrictions() {

        //Clear your mark on the board
        clearBoard();

        //If your haven't tried all combinations, add that spot as unaviable.
        if (contUsedSpaces > 7)
        {
            //If you have no more places to try ask the previous to move.
            resetPosition();
        }
        else
        {
            restrictedPos[contUsedSpaces] = boardPosition;
            contUsedSpaces++;
        }
        
    }

}
