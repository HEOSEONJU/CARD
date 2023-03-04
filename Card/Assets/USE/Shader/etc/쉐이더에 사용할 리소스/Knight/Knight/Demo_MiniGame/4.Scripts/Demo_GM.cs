using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Demo_GM : MonoBehaviour {



    public static Demo_GM Gm;

    public Image[] UIImage;
    public Sprite[] IdleUIImage;
    public Sprite[] PressedUIImage;

    public GameObject[] KnightPrefab;
    public int CharacterID = 0;

  //  public GameObject PlayerObj;
    public List<GameObject> MonsterList = new List<GameObject>();
    public GameObject[] respawnPos;
    public GameObject MonsterPrefab;
    public int respawnID = 0;
    // Use this for initialization
    void Awake () {
        Screen.fullScreen = false;

        Gm = this;
    }
	
	// Update is called once per frame
	void Update () {

        KeyUPDownchange();
        if (MonsterList.Count < 2)
        {
            Instantiate(MonsterPrefab, respawnPos[respawnID % 2].transform.position, Quaternion.identity);
            respawnID++;

        }

    }
    public void AllDestroyMonster()
    {
        for (int i = 0; i < MonsterList.Count; i++)
        {
            Destroy(MonsterList[i]);
        }
        MonsterList.Clear();
    }

    void InitColor()
    {

        for (int i = 0; i < UIImage.Length; i++)
        {
            UIImage[i].sprite = IdleUIImage[i];


        }

    }

    public void KeyUPDownchange()
    {
        // wsad
        if (Input.GetKeyUp(KeyCode.A))
        {
          

            Demo_GM.Gm.UIImage[2].sprite = IdleUIImage[2];
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Demo_GM.Gm.UIImage[3].sprite = IdleUIImage[3];
        }
            if (Input.GetKeyUp(KeyCode.W))
        {
                Demo_GM.Gm.UIImage[0].sprite = IdleUIImage[0];
            }
        if (Input.GetKeyUp(KeyCode.S))
        {
                Demo_GM.Gm.UIImage[1].sprite = IdleUIImage[1];
            }
        if (Input.GetKeyDown(KeyCode.A))
        {
                Demo_GM.Gm.UIImage[2].sprite = PressedUIImage[2];
            }
        if (Input.GetKeyDown(KeyCode.D))
        {
                Demo_GM.Gm.UIImage[3].sprite = PressedUIImage[3];
            }

        if (Input.GetKeyDown(KeyCode.W))
        {
                Demo_GM.Gm.UIImage[0].sprite = PressedUIImage[0];
            }
        if (Input.GetKeyDown(KeyCode.S))
        {
                Demo_GM.Gm.UIImage[1].sprite = PressedUIImage[1];
            }

        ///
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
                Demo_GM.Gm.UIImage[4].sprite = IdleUIImage[4];
            }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
                Demo_GM.Gm.UIImage[5].sprite = IdleUIImage[5];
            }

   


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

                Demo_GM.Gm.UIImage[4].sprite = PressedUIImage[4];

            }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {


                Demo_GM.Gm.UIImage[5].sprite = PressedUIImage[5];

            }

    

     

        if (Input.GetKeyDown(KeyCode.Space))
        {

                Demo_GM.Gm.UIImage[6].sprite = PressedUIImage[6];
            }



        if (Input.GetKeyUp(KeyCode.Space))
        {

                Demo_GM.Gm.UIImage[6].sprite = IdleUIImage[6];
            }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
                Demo_GM.Gm.UIImage[7].sprite = PressedUIImage[7];
        }
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
                Demo_GM.Gm.UIImage[7].sprite = IdleUIImage[7];
         }



        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Demo_GM.Gm.UIImage[8].sprite = PressedUIImage[8];
        }
        if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            Demo_GM.Gm.UIImage[8].sprite = IdleUIImage[8];
        }



        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Demo_GM.Gm.UIImage[9].sprite = PressedUIImage[9];
        }
        if (Input.GetKeyUp(KeyCode.Alpha3))
        {
            Demo_GM.Gm.UIImage[9].sprite = IdleUIImage[9];
        }



        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Demo_GM.Gm.UIImage[10].sprite = PressedUIImage[10];
        }
        if (Input.GetKeyUp(KeyCode.Alpha4))
        {
            Demo_GM.Gm.UIImage[10].sprite = IdleUIImage[10];
        }




    }

    public GameObject CurrentPlayerObj;

    public void Btn_Lv1()
    {
        if (CurrentPlayerObj == null)
            return;


        Vector2 tmpPos = CurrentPlayerObj.transform.position;

        GameObject tmpObj = CurrentPlayerObj;
        CurrentPlayerObj = Instantiate(KnightPrefab[0], tmpPos, Quaternion.identity);
       CameraController.Instance.Target = CurrentPlayerObj;
        CharacterID = 0;
        Destroy(tmpObj);
        AllDestroyMonster();
    }
    public void Btn_Lv2()
    {
        if (CurrentPlayerObj == null)
            return;


        Vector2 tmpPos = CurrentPlayerObj.transform.position;

        GameObject tmpObj = CurrentPlayerObj;
        CurrentPlayerObj = Instantiate(KnightPrefab[1], tmpPos, Quaternion.identity);
       CameraController.Instance.Target = CurrentPlayerObj;
        CharacterID = 1;
        Destroy(tmpObj);
        AllDestroyMonster();
    }
    public void Btn_Lv3()
    {
        if (CurrentPlayerObj == null)
            return;

        Vector2 tmpPos = CurrentPlayerObj.transform.position;

        GameObject tmpObj = CurrentPlayerObj;
        CurrentPlayerObj = Instantiate(KnightPrefab[2], tmpPos, Quaternion.identity);
        CameraController.Instance.Target = CurrentPlayerObj;
        CharacterID = 2;
        Destroy(tmpObj);
        AllDestroyMonster();
    }


}
