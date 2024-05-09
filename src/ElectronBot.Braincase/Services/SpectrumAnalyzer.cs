using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI;
using NAudio.CoreAudioApi;
using NAudio.Dsp;
using NAudio.Wave;
using System;

namespace Services
{
    public class SpectrumAnalyzer
    {
        private WasapiLoopbackCapture capture;
        private Complex[] fftBuffer;
        private int fftPos;
        private CanvasControl canvas;

        public SpectrumAnalyzer(CanvasControl canvasControl)
        {
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            MMDevice defaultDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);

            capture = new WasapiLoopbackCapture(defaultDevice);
            capture.DataAvailable += OnDataAvailable;

            int fftLength = 1024; // usually a power of 2
            fftBuffer = new Complex[fftLength];

            canvas = canvasControl;
            canvas.Draw += OnDraw;
        }

        public void Start()
        {
            capture.StartRecording();
        }

        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            for (int i = 0; i < e.BytesRecorded; i += 4)
            {
                short sample = (short)(e.Buffer[i] | (e.Buffer[i + 1] << 8));
                fftBuffer[fftPos].X = sample / 32768f; // normalized to [-1,1]
                fftBuffer[fftPos].Y = 0; // no imaginary component
                fftPos++;
                if (fftPos >= fftBuffer.Length)
                {
                    fftPos = 0;
                    FastFourierTransform.FFT(true, (int)Math.Log(fftBuffer.Length, 2.0), fftBuffer);
                    canvas.Invalidate(); // request a redraw
                }
            }
        }

        private void OnDraw(CanvasControl sender, CanvasDrawEventArgs args)
        {
            for (int i = 0; i < fftBuffer.Length / 2; i++)
            {
                float magnitude = (float)Math.Sqrt(fftBuffer[i].X * fftBuffer[i].X + fftBuffer[i].Y * fftBuffer[i].Y);
                float barHeight = magnitude * (float)canvas.ActualHeight;
                args.DrawingSession.FillRectangle(i * 2, (float)canvas.ActualHeight - barHeight, 2, barHeight, Colors.Red);
            }
        }
    }
}
