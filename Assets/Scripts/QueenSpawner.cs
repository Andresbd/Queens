using UnityEngine;

public class QueenSpawner : MonoBehaviour
{

    /*
    Armando Hernández Vargas A01334836
    Andrés Bustamante Díaz A01172912
    Elizabeth Rodríguez Fallas A01334353
         */

    public Queen q;
    public Queen[] queenList = new Queen[8];
    public int[] board = new int[64];
    private int min, max;

    // Start is called before the first frame update
    void Start()
    {
        min = 0;
        max = 7;

        spawnQueens();

        moveQueens();
    }

    private void spawnQueens() {

        for (int x = 0; x < 8; x++)
        {
            Queen newQueen = Instantiate(q, new Vector3(-1, -1, -1), Quaternion.identity);
            newQueen.index = x;
            newQueen.minPosition = min;
            newQueen.maxPosition = max;
            queenList[x] = newQueen;

            min = min + 8;
            max = max + 8;
            
        }

    }

    public void moveQueens() {


        for (int i = 0; i < queenList.Length; i++)
        {
            int firstMove = queenList[i].newPos();
            queenList[i].QueenMovement(firstMove);
        }
    }

}
