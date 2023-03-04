using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField]
    PlayerInfoData Player_Data;
    // Start is called before the first frame update




    [Header("입력필드")]
    [SerializeField]
    TMP_InputField _INPUT_ID;
    [SerializeField]
    TMP_InputField _INPUT_PASSWORD;
    [SerializeField]
    TMP_InputField _INPUT_GAME_NAME;
    [SerializeField]
    TMP_InputField _INPUT_LOGINID;
    [SerializeField]
    TMP_InputField _INPUT_LOGINPASSWORD;

    string INPUT_ID;
    string INPUT_PASSWORD;
    string INPUT_GAME_NAME;


    string LogIn_ID;
    string LogIn_Password;


    string Sucess_ID;

    [Header("오브젝트")]
    [SerializeField]
    GameObject Compelete;
    [SerializeField]
    GameObject LogIn_POPUP;
    [SerializeField]
    GameObject Create_ID_POPUP;
    [SerializeField]
    GameObject Loading;
    [SerializeField]
    Slider Gage_Value;

    FailWinodw POPUP_1;
    [SerializeField]
    GameObject POPUP;

    private void OnEnable()
    {
        POPUP.SetActive(true);
        Compelete.SetActive(false); 
        LogIn_POPUP.SetActive(false);
        Create_ID_POPUP.SetActive(false);
        Loading.SetActive(false);
        POPUP_1=GetComponent<FailWinodw>();
    }


    private void Update()
    {
        INPUT_ID = _INPUT_ID.text;
        INPUT_PASSWORD = _INPUT_PASSWORD.text;
        INPUT_GAME_NAME = _INPUT_GAME_NAME.text;
        LogIn_ID = _INPUT_LOGINID.text;
        LogIn_Password = _INPUT_LOGINPASSWORD.text;


        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Init_Super_Account();
        }
    }

    public void Init_Super_Account()
    {
        if (Player_Data.Player.Count >= 1)
        {
            for (int i = 0; i < Player_Data.Player.Count; i++)
            {
                if (Player_Data.Player[i].ID == "super")
                {
                    //POPUP_1.View_Text("이미 있는 아이디입니다");
                    return;
                }
            }


        }
        PlayerData temp = new PlayerData();
        temp.Init_SuperAccount();
        Player_Data.Player.Add(temp);
    }



    public void Init_Account()
    {

        if (INPUT_ID != "" & INPUT_PASSWORD != "")
        {
            if (Player_Data.Player.Count >= 1)
            {
                for (int i = 0; i < Player_Data.Player.Count; i++)
                {
                    if (Player_Data.Player[i].ID == INPUT_ID)
                    {
                        POPUP_1.View_Text("이미 있는 아이디입니다");
                        return;
                    }
                }
                
                
            }
            else
            {
                //POPUP_1.View_Text("이미 있는 아이디입니다");
            }
        }
        else
        {
            POPUP_1.View_Text("아이디 혹은 비밀번호가 공백입니다.");
            return;
        }

        

            PlayerData temp = new PlayerData();
            temp.Init_Account(INPUT_ID, INPUT_PASSWORD, INPUT_GAME_NAME);
            Player_Data.Player.Add(temp);
            Create_ID_POPUP.SetActive(false);
            POPUP_1.View_Text("아이디 생성에 성공하였습니다.");

    }







    public void Try_LogiN()
    {
        if(Player_Data.Player.Count>=1)
        {
            for(int i=0;i<Player_Data.Player.Count;i++)
            {
                if(Player_Data.Player[i].Try_Log(LogIn_ID, LogIn_Password))
                {
                    Sucess_ID = LogIn_ID;
                    LogIn_POPUP.SetActive(false);
                    Compelete.SetActive(true);
                    POPUP_1.View_Text("로그인에 성공 하였습니다.");
                    return;
                }
            }
            POPUP_1.View_Text("아이디 혹은 비밀번호가 틀렷습니다.");
            return;

        }


    }


    public void OpenCreate()
    {
        Create_ID_POPUP.SetActive (true);
    }
    public void CloseCreate()
    {
        _INPUT_GAME_NAME.text = "";
        _INPUT_ID.text = "";
        _INPUT_PASSWORD.text = "";
        Create_ID_POPUP.SetActive(false);;
    }

    public void OpenLOGIN()
    {
        LogIn_POPUP.SetActive(true);
    }
    public void CloseLOGIN()
    {
        _INPUT_LOGINID.text = "";
        _INPUT_LOGINPASSWORD.text = "";

        LogIn_POPUP.SetActive(false); ;
    }

    public void CloseCreate_OpneLOGIN()
    {
        CloseCreate();
        OpenLOGIN();

    }
    public void QuitGame()
    {
        Application.Quit();
    }


    public void MoveGame()
    {
        POPUP.SetActive(false);
        Loading.SetActive(true);
        PlayerPrefs.SetString("Sucess_ID", Sucess_ID);
        StartCoroutine(SceneLoading());
        //SceneManager.LoadScene("MainScene");
    }
    IEnumerator SceneLoading()
    {

        
        Gage_Value.value = 0;
        Debug.Log("로딩시작");
        TextMeshProUGUI LP=Gage_Value.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        LP.text = "Loading... 0%";

        // 더미 타이머로 진행할 값을 설정
        float dummyTime = Random.Range(0.8f, 1.5f);

        // 게이지로 표현되는 loading 값 변수들
        float loadingTime = 0.0f;   // 시간 계산 용
        float progress = 0.0f;      // 게이지 용


        // 타이머 게이지 처리 
        while (loadingTime <= dummyTime)
        {
            InfiniteLoopDetector.Run();
            //Debug.Log(loadingTime);
            // 프레임 당 시간을 증가 
            loadingTime += Time.deltaTime;

            // AsyncOperation 를 통한 추가 로딩 처리를 위해 0.9 값 을 백분율화 
            // 이후 opertaion.progress 는 0.9 까치 처리 되고 완료 된다.
            progress = Mathf.Clamp01(loadingTime / (0.9f + dummyTime));

            // 슬라이더바의 값 증가 처리
            Gage_Value.value = progress;
            LP.text = "Loading... " + Gage_Value.value * 100 + "%";
            yield return null;
        }
        yield return null;
        // "AsyncOperation"라는 "비동기적인 연산을 위한 코루틴을 제공"
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainScene");
        operation.allowSceneActivation = true;


        // 로딩 후 스타트 버튼 처리를 위한 선언
        // 이 항목이 없으면 바로 로딩 후 Scene 이동이 처리 됨


        // 로딩이 종료되기 전까지의 로딩창 게이지 처리
        while (!operation.isDone)
        {
            LP.text = "Loading... " + Gage_Value.value * 100 + "%";
            // 비동기 로딩 진행에 따른 게이지 처리
            progress = Mathf.Clamp01((operation.progress + loadingTime) / (0.9f + dummyTime));
            // 슬라이더 증가 처리
            Gage_Value.value = progress;
            yield return null;
        }
        yield break;












        //AsyncOperation Load = SceneManager.LoadSceneAsync("Main Scene");
        //while (!Load.isDone)
        //{
        //    Gage_Value.value = Load.progress;
        //    Debug.Log(Load.progress);
        //    LP.text = "Loading... " + Gage_Value.value * 100 + "%";
        //    yield return null;
        //}
    }


}
