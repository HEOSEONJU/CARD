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




    [Header("�Է��ʵ�")]
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

    [Header("������Ʈ")]
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
                    //POPUP_1.View_Text("�̹� �ִ� ���̵��Դϴ�");
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
                        POPUP_1.View_Text("�̹� �ִ� ���̵��Դϴ�");
                        return;
                    }
                }
                
                
            }
            else
            {
                //POPUP_1.View_Text("�̹� �ִ� ���̵��Դϴ�");
            }
        }
        else
        {
            POPUP_1.View_Text("���̵� Ȥ�� ��й�ȣ�� �����Դϴ�.");
            return;
        }

        

            PlayerData temp = new PlayerData();
            temp.Init_Account(INPUT_ID, INPUT_PASSWORD, INPUT_GAME_NAME);
            Player_Data.Player.Add(temp);
            Create_ID_POPUP.SetActive(false);
            POPUP_1.View_Text("���̵� ������ �����Ͽ����ϴ�.");

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
                    POPUP_1.View_Text("�α��ο� ���� �Ͽ����ϴ�.");
                    return;
                }
            }
            POPUP_1.View_Text("���̵� Ȥ�� ��й�ȣ�� Ʋ�ǽ��ϴ�.");
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
        Debug.Log("�ε�����");
        TextMeshProUGUI LP=Gage_Value.transform.GetChild(2).GetComponent<TextMeshProUGUI>();

        LP.text = "Loading... 0%";

        // ���� Ÿ�̸ӷ� ������ ���� ����
        float dummyTime = Random.Range(0.8f, 1.5f);

        // �������� ǥ���Ǵ� loading �� ������
        float loadingTime = 0.0f;   // �ð� ��� ��
        float progress = 0.0f;      // ������ ��


        // Ÿ�̸� ������ ó�� 
        while (loadingTime <= dummyTime)
        {
            InfiniteLoopDetector.Run();
            //Debug.Log(loadingTime);
            // ������ �� �ð��� ���� 
            loadingTime += Time.deltaTime;

            // AsyncOperation �� ���� �߰� �ε� ó���� ���� 0.9 �� �� �����ȭ 
            // ���� opertaion.progress �� 0.9 ��ġ ó�� �ǰ� �Ϸ� �ȴ�.
            progress = Mathf.Clamp01(loadingTime / (0.9f + dummyTime));

            // �����̴����� �� ���� ó��
            Gage_Value.value = progress;
            LP.text = "Loading... " + Gage_Value.value * 100 + "%";
            yield return null;
        }
        yield return null;
        // "AsyncOperation"��� "�񵿱����� ������ ���� �ڷ�ƾ�� ����"
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainScene");
        operation.allowSceneActivation = true;


        // �ε� �� ��ŸƮ ��ư ó���� ���� ����
        // �� �׸��� ������ �ٷ� �ε� �� Scene �̵��� ó�� ��


        // �ε��� ����Ǳ� �������� �ε�â ������ ó��
        while (!operation.isDone)
        {
            LP.text = "Loading... " + Gage_Value.value * 100 + "%";
            // �񵿱� �ε� ���࿡ ���� ������ ó��
            progress = Mathf.Clamp01((operation.progress + loadingTime) / (0.9f + dummyTime));
            // �����̴� ���� ó��
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
