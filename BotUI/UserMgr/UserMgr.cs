using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotUI.UserMgr
{
    internal struct UserInfo
    {
        string m_szKey;
        string m_szPW;

        internal UserInfo(string szKey, string szPw)
        {
            m_szKey = szKey;
            m_szPW = szPw;
        }

        internal string Key
        {
            get
            {
                return m_szKey;
            }
        }

        internal string PassWord
        {
            get
            {
                return m_szPW;
            }
        }
    }

    // Public members
    internal partial class UserMgr
    {
        internal static UserMgr Instance
        {
            get
            {
                if (m_Manager == null) {
                    m_Manager = new UserMgr();
                }
                return m_Manager;
            }
        }

        internal UserInfo GetUserInfo(string szUserName)
        {
            if (m_UserDic.ContainsKey(szUserName) == false) {
                return new UserInfo("", "");
            }

            return m_UserDic[szUserName];
        }
    }

    // Protected members
    internal partial class UserMgr
    {
    }

    // Private members
    internal partial class UserMgr
    {
        static UserMgr m_Manager;
        Dictionary<string, UserInfo> m_UserDic;
        UserMgr()
        {
            m_UserDic = new Dictionary<string, UserInfo>();

            // Add user
            UserInfo CHIENLI_Info = new UserInfo("rWSTf366UrS9q9CBeQOWm6DlDfkAU6wrCRfqqsbGXKJ", "BNsGtg7WCb7bT2uvBoLJVsYiZrR8jZ2fptvJouSA6CX");
            m_UserDic.Add("CHIENLI", CHIENLI_Info);
        }
    }
}
