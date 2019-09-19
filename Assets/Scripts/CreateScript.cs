using UnityEngine;

public class CreateScript : MonoBehaviour
{

    /*
    Armando Hernández Vargas A01334836
    Andrés Bustamante Díaz A01172912
    Elizabeth Rodríguez Fallas A01334353
         */
    public GameObject white, black;

    // Start is called before the first frame update
    void Start()
    {
        int cont = 0;

        for (int x = 0; x < 8; x++) {
            for (int y = 0; y < 8; y++)
            {
                if (cont % 2 == 0)
                {
                    black = Instantiate(black, new Vector3(x, 0, y), Quaternion.identity);
                    black.transform.parent = transform;
                    cont++;
                }
                else {
                    white = Instantiate(white, new Vector3(x, 0, y), Quaternion.identity);
                    white.transform.parent = transform;
                    cont++;
                }
                
            }
            cont++;
        }
    }
}
