using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace HangmanFun.Sound;
public class AudioPlayer
{
    IWavePlayer _output;
    PdProvider _pdProvider;
    bool _playing;

    public AudioPlayer(string pdPath)
    {
        _output = new WasapiOut(AudioClientShareMode.Shared, 10);
        _pdProvider = new PdProvider(pdPath);
        SetVolume(0);
        _output.Init(_pdProvider);
        Play();
    }
    public void Play()
    {

        _output.Play();
        _playing = true;

    }

    public void Pause()
    {

        _output.Pause();
        _playing = false;
    }

    public void TogglePlaying()
    {
        if (_playing)
        {
            Pause();
        }
        else
        {
            Play();
        }
    }

    public void SetVolume(int volume)
    {
        volume = int.Clamp(volume, 0, 100);
        float fVolume = 0.2f / 100 * volume;
        _pdProvider.SetVolume(fVolume);
    }

    public void StopPd()
    {
        _pdProvider.Stop();
    }
    public void StartPd()
    {
        _pdProvider.Start();
    }

}
