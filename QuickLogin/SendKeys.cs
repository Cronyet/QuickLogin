using System.Runtime.InteropServices;

#pragma warning disable CS8605 // 取消装箱可能为 null 的值。

namespace QuickLogin
{
    internal static class SendKeys
    {
        public const int KEYEVENTF_KEYUP = 2;
        public const int KEYEVENTF_KEYDOWN = 0;

        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        internal static extern void keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        /// <summary>
        /// 发送密码按键串
        /// </summary>
        /// <param name="pwd">密码</param>
        internal static void SendPwd(string pwd)
        {
            string[] keys = pwd.Split('+');
            bool[] ControlKeyStatus = new bool[3] { false, false, false };
            foreach (string item in keys)
            {
                if(item != null && item != "")
                {
                    switch (item)
                    {
                        case "Ctrl":
                            SetKeyDown(Keys.LControlKey);
                            ControlKeyStatus[0] = true;
                            break;
                        case "Shift":
                            SetKeyDown(Keys.LShiftKey);
                            ControlKeyStatus[1] = true;
                            break;
                        case "Alt":
                            SetKeyDown(Keys.Alt);
                            ControlKeyStatus[2] = true;
                            break;
                        default:
                            Keys key = (Keys)new KeysConverter().ConvertFromString(item);
                            SetKeyDown(key);
                            SetKeyUp(key);
                            break;
                    }
                }
            }
            if(ControlKeyStatus[0]) SetKeyUp(Keys.LControlKey);
            if(ControlKeyStatus[1]) SetKeyDown(Keys.LShiftKey);
            if(ControlKeyStatus[2]) SetKeyDown(Keys.Alt);
        }

        /// <summary>
        /// 使按键变更为按下状态
        /// </summary>
        /// <param name="key">键</param>
        internal static void SetKeyDown(Keys key) => keybd_event(key, 0, KEYEVENTF_KEYDOWN, 0);

        /// <summary>
        /// 使按键变更为抬起状态
        /// </summary>
        /// <param name="key">键</param>
        internal static void SetKeyUp(Keys key) => keybd_event(key, 0, KEYEVENTF_KEYUP, 0);
    }
}

#pragma warning restore CS8605 // 取消装箱可能为 null 的值。
