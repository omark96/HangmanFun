using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace HangmanFun.Sound;
internal class MusicPlayer
{
    IWavePlayer _output;
    PdProvider _pd;

    public MusicPlayer()
    {
        _output = new WasapiOut(AudioClientShareMode.Shared, 10);
        _pd = new PdProvider();

    }
    public void PlayWithWasapi()
    {
        _output.Init(_pd);
        _output.Play();

    }
}
