using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace HangmanFun.Sound;
internal class AudioPlayer
{
    IWavePlayer _output;
    NewApiPdProvider _pdProvider;

    public AudioPlayer(string pdPath)
    {
        _output = new WasapiOut(AudioClientShareMode.Shared, 10);
        _pdProvider = new NewApiPdProvider(pdPath);
        _output.Init(_pdProvider);
    }
    public void Play()
    {
        _output.Play();
    }

    public void Pause()
    {
        _output.Pause();
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
