using UnityEngine;
using unityutilities;

public class EnableDisableWithButton : MonoBehaviour
{

    public Side side;
    public GameObject obj;
    
    // Start is called before the first frame update

    // Update is called once per frame
    private void Update()
    {
        if (InputMan.Button1Down(side))
        {
            obj.SetActive(!obj.activeSelf);
        }
    }
}
