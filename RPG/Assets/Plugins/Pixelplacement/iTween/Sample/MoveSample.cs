using UnityEngine;
using System.Collections;

public class MoveSample : MonoBehaviour
{	
	public Transform[] paths;
	void Start(){
		Hashtable args = new Hashtable();
		args.Add("path",paths);
		args.Add("easeType","Linear");
		args.Add("speed",3);
		args.Add("movetopath",true);
		args.Add("orienttopath",true);
		args.Add("loopType","pingPong");
		args.Add("delay",.1f);
		iTween.MoveTo(gameObject,args);

	}
	 void OnDrawGizmos()
	{
		iTween.DrawLine(paths,Color.yellow);
	}
}

