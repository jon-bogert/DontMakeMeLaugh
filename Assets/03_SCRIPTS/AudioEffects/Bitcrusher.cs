using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Bitcrusher : MonoBehaviour
{
    public int resolution = 16;
    public int sampleRate = 8000;

    private int internalSampleRate;

    private void Start()
    {
        internalSampleRate = AudioSettings.outputSampleRate;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        float[] currentSample = new float[channels];
        if (sampleRate > internalSampleRate)
            sampleRate = internalSampleRate;

        if (resolution <= 0)
            resolution = 1;

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
