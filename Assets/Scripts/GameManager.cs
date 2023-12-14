using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Database dbtemp;

    private static int userIndex = -1;
    private static Database db;
    private static bool loginState = false;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        db = dbtemp;

        if(!loginState)
        {
            Login("ghost");
        }
    }

    public static void Login(string userName)
    {
        userIndex = db.UserLogin(userName);
        Debug.Log("Now login with " + db.userDataList[userIndex].Name);
        if(userName != "ghost")
        {
            loginState = true;
        }
    }

    public static bool GetLoginState()
    {
        return loginState;
    }

    public static void GetMoney(int amount)
    {
        db.userDataList[userIndex].Money += amount;
    }

    public static void WinMoney(int amount)
    {
        db.userDataList[userIndex].Money += amount;
    }

    public static void LostMoney(int amount)
    {
        db.userDataList[userIndex].Money -= amount;
    }

    public static int GetMoney()
    {
        return db.userDataList[userIndex].Money;
    }
}
