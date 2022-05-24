using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    int order = 10;
    Stack<UI_Popup> popupStack = new Stack<UI_Popup>();
    UI_Scene sceneUI = null;

    //루트 프로퍼티
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null) //오브젝트가 없다면?
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    ///팝업이 생길때
    ///우선순위 설정하는 함수
    public void SetCanvas(GameObject _go, bool _sort = true)
    {
        //캔버스 추출
        Canvas canvas = Util.GetOrAddComponent<Canvas>(_go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        ///캔버스가 중첩해서 존재할 때
        ///무조건 sortingOrder 강제
        canvas.overrideSorting = true;

        if (_sort) //popup ui
        {
            canvas.sortingOrder = order;
            order++;
        }
        ///정렬 요청 안했을 때,
        ///즉 일반 UI일 때
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    //씬 유아이 생성 함수
    public T ShowSceneUI<T>(string _name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(_name)) //이름 안받았으면
            _name = typeof(T).Name; //T 이름을 사용

        //프리팹 생성
        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{_name}");

        T sceneUI = Util.GetOrAddComponent<T>(go);
        sceneUI = sceneUI; //SceneUI 저장

        //게임오브젝트를 루트 산하에 위치시킴
        go.transform.SetParent(Root.transform);

        return sceneUI; //팝업 반환
    }

    /// 팝업 여는 함수
    /// where T : UI_Popup : ui_popup 상속 받는 녀석만 받겠다
    /// 앞의 T 는 컴포넌트(스크립트), 뒤의 스트링은 프리팹의 이름을 받음
    public T ShowPopupUI<T>(string _name = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(_name)) //이름 안받았으면
            _name = typeof(T).Name; //T 이름을 사용하겠다

        //프리팹 생성
        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{_name}");

        T popup = Util.GetOrAddComponent<T>(go);
        popupStack.Push(popup); //스택에 추가

        go.transform.SetParent(Root.transform);

        return popup; //팝업 반환
    }

    //삭제할 대상이 맞는지 확인후 삭제
    public void ClosePopupUI(UI_Popup _popup)
    {
        if (popupStack.Count == 0) return;

        //최상단 조회
        if(popupStack.Peek() != _popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }
        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        //없으면 리턴
        if (popupStack.Count == 0) return;

        //팝업 추출
        UI_Popup popup = popupStack.Pop();

        //팝업 삭제
        //해당 스크립트를 가지고 있는 게임 오브젝트 삭제
        Managers.Resource.Destroy(popup.gameObject);

        popup = null; //삭제 후 접근 방지
        order--;
    }

    public void CloseAllPopupUI()
    {
        //스택 빌때까지 삭제
        while (popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        sceneUI = null;
    }
}

