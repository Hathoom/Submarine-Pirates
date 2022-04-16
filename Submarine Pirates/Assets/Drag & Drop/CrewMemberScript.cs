using UnityEngine;

public class CrewMemberScript : MonoBehaviour, IDrag
{

    public void onStartDrag()
    {
        Debug.Log("Start Drag");
    }

    public void onEndDrag()
    {
        Debug.Log("End Drag");
        //throw new System.NotImplementedException();
    }

}
