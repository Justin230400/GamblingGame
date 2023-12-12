using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Database", menuName = "Gambling Game/Globa/Database")]
public class Database : ScriptableObject
{
    public List<UserData> userDataList;

    public void init()
    {
        if(userDataList == null)
        {
            userDataList = new List<UserData>();
        }
    }

    public int UserRegister(string ID)
    {
        return 0;
    }
}

public class UserData
{
    private string _uid;
    private string _name;
    private int money;

    public string UID { get => _uid; }
    public int Money { get => money; set => money = value; }
    public string Name { get => _name; set => _name = value; }

    public void RegisterUserId(string uid)
    {
        if (_uid != null)
            return;

        _uid = uid;
    }
}
