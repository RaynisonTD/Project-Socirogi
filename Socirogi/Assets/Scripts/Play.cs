using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
public class Play : MonoBehaviour
{
   public Button yourButton;
   
   	void Start () {
   		Button btn = yourButton.GetComponent<Button>();
   		btn.onClick.AddListener(TaskOnClick);
   	}
   
   	void TaskOnClick(){
	    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   	}
}
