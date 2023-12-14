using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Database", menuName = "Gambling Game/Globa/Database")]
public class Database : ScriptableObject
{
    public List<UserData> userDataList;

    public int UserLogin(string userName)
    {
        UserData ud = new UserData(userName);
        
        if (userDataList == null)
        {
            return UserRegister(ud);
        }

        for(int i = 0; i < userDataList.Count; i++)
        {
            if(userDataList[i].Name == ud.Name)
            {
                return i;
            }
        }

        return UserRegister(ud);
    }

    public int UserRegister(UserData ud)
    {
        if(userDataList == null)
            userDataList = new List<UserData>();

        userDataList.Add(ud);

        return userDataList.Count - 1;
    }
}

public class UserData
{
    private string _uid;
    private string _name;
    private int money;

    public string Name 
    { 
        get => _name;
        set {
            if(_name == null)
            {
                _name = value;
            }
        } }

    public int Money 
    { 
        get => money; 
        set {
            if (value < 0)
            {
                money = 0;
            }
            else
            {
                money = value;
            }
        } }

    public string Uid
    {
        get => _uid; 
        set
        {
            if (_uid == null)
            {
                _uid = value;
            }
        }
    }

    public UserData(string userName)
    {
        Name = userName;
        Money = 0;
        RegisterUserId();
    }

    
    public void RegisterUserId()
    {
        Uid = System.Guid.NewGuid().ToString();
    }
    

}
