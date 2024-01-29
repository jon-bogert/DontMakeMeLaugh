public class AppSettings
{
    public static float mouseSensitivity { get { return Get()._mouseSensitivity; } set { Get()._mouseSensitivity = value; } }
    public static float gamepadSensitivity { get { return Get()._gamepadSensitivity; } set { Get()._gamepadSensitivity = value; } }

    static AppSettings inst;

    static AppSettings Get()
    {
        if (inst == null)
            inst = new AppSettings();
        return inst;
    }

    float _mouseSensitivity = 0.67f;
    float _gamepadSensitivity = 1.0f;
}
