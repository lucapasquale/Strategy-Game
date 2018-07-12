﻿using System.Collections.Generic;
using UnityEngine;

public class ActorStats : MonoBehaviour
{
    #region Fields / Properties

    [SerializeField]
    private readonly float[] _data = new float[(int)StatTypes.Count];

    public float this[StatTypes s] {
        get { return _data[(int)s]; }
        set { SetValue(s, value, true); }
    }

    #endregion Fields / Properties

    #region Notifications

    private static Dictionary<StatTypes, string> _didChangeNotifications = new Dictionary<StatTypes, string>();

    private static Dictionary<StatTypes, string> _willChangeNotifications = new Dictionary<StatTypes, string>();

    public static string DidChangeNotification(StatTypes type) {
        if (!_didChangeNotifications.ContainsKey(type))
            _didChangeNotifications.Add(type, string.Format("Stats.{0}DidChange", type.ToString()));
        return _didChangeNotifications[type];
    }

    public static string WillChangeNotification(StatTypes type) {
        if (!_willChangeNotifications.ContainsKey(type))
            _willChangeNotifications.Add(type, string.Format("Stats.{0}WillChange", type.ToString()));
        return _willChangeNotifications[type];
    }

    #endregion Notifications

    #region Public

    public void SetValue(StatTypes type, float value, bool allowExceptions) {
        float oldValue = this[type];

        if (oldValue == value)
            return;

        //if (allowExceptions) {
        //    // Allow exceptions to the rule here
        //    ValueChangeException exc = new ValueChangeException(oldValue, value);

        //    // The notification is unique per stat type
        //    this.PostNotification(WillChangeNotification(type), exc);

        //    // Did anything modify the value?
        //    value = exc.GetModifiedValue();

        //    // Did something nullify the change?
        //    if (exc.toggle == false || value == oldValue)
        //        return;
        //}

        _data[(int)type] = value;
        this.PostNotification(DidChangeNotification(type), oldValue);
    }

    #endregion Public
}