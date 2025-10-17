/*
 * For information on usage and redistribution, and for a DISCLAIMER OF ALL
 * WARRANTIES, see the file, "LICENSE.txt," in this distribution.
 * 
 * Copyright(c) 2016 Thomas Mayer<thomas@residuum.org>
 */
using LibPDBinding.Managed;
using LibPDBinding.Managed.Data;
using NAudio.Utils;
using NAudio.Wave;

namespace HangmanFun.Sound
{
    class PdProvider : IWaveProvider
    {
        /// <summary>
        /// number of ticks for libPd to compute in a computation cycle.
        /// 
        /// lower values may lead to distortion because of switching between threads.
        /// </summary>
        static readonly int Ticks = 7;
        static readonly int SampleRate = 44100;
        static readonly int Channels = 2;

        /// <summary>
        /// use a CircularBuffer similar to BufferedWaveProvider.
        /// </summary>
        CircularBuffer _circularBuffer;
        int _minBuffer;
        Pd _pd;
        Patch _patch;
        float[] _pdBuffer;

        public PdProvider(string pdPath)
        {
            SetUpPd(pdPath);
            SetUpBuffer();
            RefillBuffer();
        }

        /// <summary>
        /// Sets up CircularBuffer for storage and float[] for getting data from libPd.
        /// </summary>
        void SetUpBuffer()
        {
            int blocksize = _pd.BlockSize;
            _circularBuffer = new CircularBuffer(blocksize * Ticks * Channels * 4); // make the circular buffer large enough
            _pdBuffer = new float[Ticks * Channels * blocksize];
            _minBuffer = blocksize * Ticks * Channels * 2;
        }

        /// <summary>
        /// Sets up communication with libPd.
        /// </summary>
        void SetUpPd(string pdPath)
        {
            // Init new Pd instance
            _pd = new Pd(0, 2, SampleRate);
            // Open Pd patch
            _patch = _pd.LoadPatch(pdPath);
            // Start audio
            Start();
        }

        public void Start()
        {
            _pd.Start();
        }
        public void Stop()
        {
            _pd.Stop();
        }

        public void SetVolume(float volume)
        {
            if (volume > 100)
            {
                volume = 100;
            }
            else if (volume < 0)
            {
                volume = 0;
            }
            _pd.Messaging.Send("volume", new Float(volume));
        }
        /// <summary>
        /// An example for reading messages from LibPD
        /// </summary>
        //void Pd_Float(object sender, FloatEventArgs e)
        //{
        //    Console.WriteLine("{0}, {1}", e.Receiver, e.Float.Value);
        //}

        /// <summary>
        /// Let libPd compute data, while the CircularBuffer has less than _minBuffer bytes available.
        /// </summary>
        void RefillBuffer()
        {
            while (_circularBuffer.Count < _minBuffer)
            {
                // Compute audio. Take care of the array sizes for audio in and out.z
                _pd.Process(Ticks, new float[0], _pdBuffer);
                _circularBuffer.Write(PcmFromFloat(_pdBuffer), 0, _pdBuffer.Length * 4);
            }
        }

        /// <summary>
        /// Convert float[] from libPd to byte[] for CircularBuffer.
        /// 
        /// This is surely optimizable
        /// </summary>
        byte[] PcmFromFloat(float[] pdOutput)
        {
            WaveBuffer wavebuffer = new WaveBuffer(pdOutput.Length * 4);
            for (var i = 0; i < pdOutput.Length; i++)
            {
                wavebuffer.FloatBuffer[i] = pdOutput[i];
            }
            return wavebuffer.ByteBuffer;
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            int read = _circularBuffer.Read(buffer, offset, count);
            RefillBuffer();
            return read;
        }

        public WaveFormat WaveFormat
        {
            get
            {
                // We have float wave format
                return WaveFormat.CreateIeeeFloatWaveFormat(SampleRate, Channels);
            }
        }
    }
}