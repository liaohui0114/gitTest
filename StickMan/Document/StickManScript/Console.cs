using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public class Console : MonoBehaviour
{
    [Space(15f), SerializeField]
    private KeyCode _activate = KeyCode.Menu;
    [SerializeField]
    private KeyCode _clear = KeyCode.Escape;
    private Dictionary<LogType, int> _counts = new Dictionary<LogType, int>();
    [SerializeField, Range(1f, 5f)]
    private int _darkness = 4;
    [SerializeField]
    private bool _dontDestroyOnLoad = true;
    [SerializeField]
    private int _fontHeight = 12;
    private readonly List<LogItem> _logs = new List<LogItem>();
    [Space(15f), SerializeField]
    private bool _multiThreaded;
    [SerializeField]
    private bool _pixelPerfect;
    private Vector2 _scrollPosition;
    [SerializeField]
    private bool _showAsserts = true;
    [SerializeField]
    private bool _showErrors = true;
    [SerializeField]
    private bool _showLogs = true;
    private bool _shown;
    [SerializeField]
    private bool _showStackTrace = true;
    [SerializeField]
    private bool _showTime;
    [SerializeField]
    private bool _showWarnings = true;

    private void Awake()
    {
        if (UnityEngine.Object.FindObjectsOfType<Console>().Length > 1)
        {
            UnityEngine.Object.Destroy(base.gameObject);
        }
        else
        {
            if (this._dontDestroyOnLoad)
            {
                UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
            }
            LogType[] values = (LogType[]) Enum.GetValues(typeof(LogType));
            foreach (LogType type in values)
            {
                this._counts.Add(type, 0);
            }
        }
    }

    private void HandleLog(string message, string stackTrace, LogType type)
    {
        List<LogItem> list = this._logs;
        lock (list)
        {
            Dictionary<LogType, int> dictionary;
            LogType type2;
            this._logs.Insert(0, new LogItem(type, message, stackTrace));
            int num = dictionary[type2];
            (dictionary = this._counts)[type2 = type] = num + 1;
        }
    }

    private void OnDisable()
    {
        if (this._multiThreaded)
        {
            Application.RegisterLogCallbackThreaded(null);
        }
        else
        {
            Application.RegisterLogCallback(null);
        }
    }

    private void OnEnable()
    {
        if (this._multiThreaded)
        {
            Application.RegisterLogCallbackThreaded(new Application.LogCallback(this.HandleLog));
        }
        else
        {
            Application.RegisterLogCallback(new Application.LogCallback(this.HandleLog));
        }
    }

    private void OnGUI()
    {
        int num = this._fontHeight;
        if (!this._pixelPerfect)
        {
            num = Mathf.RoundToInt(this._fontHeight * (((float) Screen.height) / 720f));
        }
        if (!this._shown)
        {
            string str3;
            string str = string.Empty;
            if (this._showErrors)
            {
                str = "<color=red>E:" + (this._counts[LogType.Error] + this._counts[LogType.Exception]) + "</color>";
            }
            if (this._showAsserts)
            {
                str3 = str;
                object[] objArray1 = new object[] { str3, " <color=magenta>A:", this._counts[LogType.Assert], "</color>" };
                str = string.Concat(objArray1);
            }
            if (this._showWarnings)
            {
                str3 = str;
                object[] objArray2 = new object[] { str3, " <color=yellow>W:", this._counts[LogType.Warning], "</color>" };
                str = string.Concat(objArray2);
            }
            if (this._showLogs)
            {
                str3 = str;
                object[] objArray3 = new object[] { str3, " <color=white>L:", this._counts[LogType.Log], "</color>" };
                str = string.Concat(objArray3);
            }
            object[] objArray4 = new object[] { "<size=", num, ">", str, "</size>" };
            GUILayoutOption[] options = new GUILayoutOption[] { GUILayout.Height(num * 1.5f) };
            if (GUILayout.Button(string.Concat(objArray4), options))
            {
                this._shown = true;
            }
        }
        else
        {
            for (int i = 0; i < this._darkness; i++)
            {
                GUI.Box(new Rect(-10f, -10f, (float) (Screen.width + 20), (float) (Screen.height + 20)), string.Empty);
            }
            GUILayout.BeginHorizontal(new GUILayoutOption[0]);
            GUILayoutOption[] optionArray2 = new GUILayoutOption[] { GUILayout.Height(num * 1.75f) };
            if (GUILayout.Button("<size=" + (num * 1.25f) + ">Hide</size>", optionArray2))
            {
                this._shown = false;
            }
            else
            {
                GUI.color = Color.red;
                GUILayoutOption[] optionArray3 = new GUILayoutOption[] { GUILayout.Height(num * 1.75f) };
                if (GUILayout.Button("<size=" + (num * 1.25f) + ">Clear</size>", optionArray3))
                {
                    this._logs.Clear();
                    LogType[] values = (LogType[]) Enum.GetValues(typeof(LogType));
                    foreach (LogType type in values)
                    {
                        this._counts[type] = 0;
                    }
                }
                GUI.color = Color.white;
                GUILayout.EndHorizontal();
                GUILayoutOption[] optionArray4 = new GUILayoutOption[] { GUILayout.Width((float) Screen.width) };
                this._scrollPosition = GUILayout.BeginScrollView(this._scrollPosition, optionArray4);
                List<LogItem> list = this._logs;
                lock (list)
                {
                    for (int j = 0; j < this._logs.Count; j++)
                    {
                        LogItem item2;
                        LogItem item = this._logs[j];
                        switch (item.Type)
                        {
                            case LogType.Error:
                            case LogType.Exception:
                            {
                                if (this._showErrors)
                                {
                                    break;
                                }
                                continue;
                            }
                            case LogType.Assert:
                            {
                                if (this._showAsserts)
                                {
                                    goto Label_0371;
                                }
                                continue;
                            }
                            case LogType.Warning:
                            {
                                if (this._showWarnings)
                                {
                                    goto Label_0390;
                                }
                                continue;
                            }
                            case LogType.Log:
                            {
                                if (this._showLogs)
                                {
                                    goto Label_03AF;
                                }
                                continue;
                            }
                            default:
                                goto Label_03BE;
                        }
                        GUI.color = Color.red;
                        goto Label_03BE;
                    Label_0371:
                        GUI.color = Color.magenta;
                        goto Label_03BE;
                    Label_0390:
                        GUI.color = Color.yellow;
                        goto Label_03BE;
                    Label_03AF:
                        GUI.color = Color.white;
                    Label_03BE:
                        item2 = this._logs[j];
                        string message = item2.Message;
                        if (this._showTime)
                        {
                            LogItem item3 = this._logs[j];
                            message = "<b>" + item3.Time + " </b>" + message;
                        }
                        if (this._showStackTrace)
                        {
                            object[] objArray5 = new object[6];
                            objArray5[0] = message;
                            objArray5[1] = "\n<size=";
                            objArray5[2] = num * 0.9f;
                            objArray5[3] = ">";
                            LogItem item4 = this._logs[j];
                            objArray5[4] = item4.StackTrace;
                            objArray5[5] = "</size>";
                            message = string.Concat(objArray5);
                        }
                        GUILayout.Label(string.Concat(new object[] { "<size=", num, ">", message, "</size>" }), new GUILayoutOption[0]);
                    }
                }
                GUILayout.EndScrollView();
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(this._activate))
        {
            this._shown = !this._shown;
        }
        if (this._shown && Input.GetKeyDown(this._clear))
        {
            this._logs.Clear();
            LogType[] values = (LogType[]) Enum.GetValues(typeof(LogType));
            foreach (LogType type in values)
            {
                this._counts[type] = 0;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential, Size=1)]
    private struct LogItem
    {
        public LogItem(LogType type, string message, string stack)
        {
            this.Type = type;
            this.Message = message;
            char[] trimChars = new char[] { '\n' };
            this.StackTrace = stack.TrimEnd(trimChars);
            this.Time = DateTime.Now.ToString("HH:mm:ss ");
        }

        public LogType Type { get; private set; }
        public string Time { get; private set; }
        public string Message { get; private set; }
        public string StackTrace { get; private set; }
    }
}

