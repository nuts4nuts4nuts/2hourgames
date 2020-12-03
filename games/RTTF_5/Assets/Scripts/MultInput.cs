using System;
using UnityEngine;

public enum InputKey
{
    Right,
    Left,
    Jump,
    DoThing,
    FastFall,
    Dash
}

public class MultInput : ScriptableObject
{
    [Serializable]
    private struct TwoKeys
    {
        public KeyCode First;
        public KeyCode Second;

        public bool GetKeyDown() => Input.GetKeyDown(First) || Input.GetKeyDown(Second);
        public bool GetKey() => Input.GetKey(First) || Input.GetKey(Second);
        public bool GetKeyUp() => Input.GetKeyUp(First) || Input.GetKeyUp(Second);
    }

    private static MultInput _one;
    private static MultInput _two;

    public static MultInput One
    {
        get
        {
            if (_one == null)
            {
                _one = Resources.Load<MultInput>("Input/One");
            }

            return _one;
        }
    }

    public static MultInput Two
    {
        get
        {
            if (_two == null)
            {
                _two = Resources.Load<MultInput>("Input/Two");
            }

            return _two;
        }
    }

    //If you wanna add more keys, just type one here and make sure MultInput.GetTwoKeys has the mapping
    [SerializeField] private TwoKeys _right;
    [SerializeField] private TwoKeys _left;
    [SerializeField] private TwoKeys _jump;
    [SerializeField] private TwoKeys _doThing;
    [SerializeField] private TwoKeys _fastFall;
    [SerializeField] private TwoKeys _dash;

    private TwoKeys GetTwoKeys(InputKey key)
    {
        switch (key)
        {
            case InputKey.Right:
                return _right;
            case InputKey.Left:
                return _left;
            case InputKey.Jump:
                return _jump;
            case InputKey.DoThing:
                return _doThing;
            case InputKey.FastFall:
                return _fastFall;
            case InputKey.Dash:
                return _dash;
            default:
                throw new ArgumentOutOfRangeException(nameof(key), key, null);
        }
    }

    public bool GetKey(InputKey key) => GetTwoKeys(key).GetKey();

    public bool GetKeyDown(InputKey key) => GetTwoKeys(key).GetKeyDown();

    public bool GetKeyUp(InputKey key) => GetTwoKeys(key).GetKeyUp();
}
