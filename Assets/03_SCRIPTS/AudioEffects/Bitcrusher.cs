using UnityEngine;

public class Bitcrusher : MonoBehaviour
{
    public int resolution = 256;
    public int sampleRate = 8000;

    private int internalSampleRate;
    string n;

    private void Start()
    {
        internalSampleRate = AudioSettings.outputSampleRate;
        n = name;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        float[] currentSample = new float[channels];
        if (sampleRate > internalSampleRate)
            sampleRate = internalSampleRate;

        if (sampleRate < 0)
            sampleRate = 1;

        if (resolution <= 0)
            resolution = 1;

        if (sampleRate == 0)
            Debug.Log(n + " is the issue");
        int sampleWidth = internalSampleRate / sampleRate;

        for (int i = 0; i < data.Length; i += channels)
        {
            if ((int)(i * 0.5f) % sampleWidth == 0)
            {
                for (int c = 0; c < channels; c++)
                {
                    currentSample[c] = data[i + c];
                    currentSample[c] = ((int)(data[i + c] * 0.5f * resolution) / (float)resolution);
                }
            }
            for (int c = 0; c < channels; c++)
            {
                data[i + c] = currentSample[c];
            }

        }
    }
}
