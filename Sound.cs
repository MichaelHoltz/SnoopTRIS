using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
namespace Tetris
{
    public class Sound
    {
        private static List<Thread> threads = new List<Thread>();
        /// <summary>
        /// Play a sound and add it to the play threads so it can be stopped 
        /// 
        /// 
        /// And as usual it doesn't work as advertised.. 
        /// 
        /// Should be an async task likst of wavePlayers maybe.. then the waver player can be interacted with if necessary.
        /// </summary>
        /// <param name="soundFileName"></param>
        /// <param name="ct"></param>
        public static void PlaySound(string soundFileName, CancellationToken ct)
        {
            Thread thread = new Thread(() =>
            {
                IWavePlayer wavePlayer = new WaveOutEvent();
                AudioFileReader audioFileReader = new AudioFileReader(soundFileName);

                try
                {

                    wavePlayer.Init(audioFileReader);
                    wavePlayer.Volume = 1.0f;
                    wavePlayer.Play();

                    while (wavePlayer.PlaybackState == PlaybackState.Playing && !ct.IsCancellationRequested)
                    {

                        Thread.Sleep(100);
                    }

                    if (ct.IsCancellationRequested)
                    {

                        //ramp down volume
                        for (float i = 0.9f; i > 0; i -= 0.1f)
                        {
                            wavePlayer.Volume = i;
                            Thread.Sleep(100);
                        }

                        wavePlayer.Stop();

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                wavePlayer.Dispose();
                audioFileReader.Dispose();
            });

            threads.Add(thread);
            thread.Start();
        }
        /// <summary>
        /// Play and Event Sound and return a CancellationTokenSource so it can be stopped.
        /// </summary>
        /// <param name="soundEvent"></param>
        /// <returns></returns>
        public static CancellationTokenSource PlayEventSound(SoundEvents soundEvent)
        {
            //Generate a Cancelation Token Source so the sound can be stopped.
            CancellationTokenSource cts = new CancellationTokenSource();
            //return cts; //Disable sound to test crashing issues/theories.

            //Generate a random number between 1 and 5 inclusively.
            Random rnd = new Random(DateTime.Now.Millisecond);
            int randomNumber = rnd.Next(1, 6);



            string soundFileName = "";
            switch (soundEvent)
            {
                case SoundEvents.SystemStart:
                    switch (randomNumber)
                    {
                        case 1:
                            soundFileName = @"Sound\Welcome to SWIFT_Ryan.mp3";
                            break;
                        case 2:
                            soundFileName = @"Sound\Welcome to S.F.W.I.F-Villanous.mp3";
                            break;
                        case 3:
                            soundFileName = @"Sound\GrandEntrance.mp3";
                            break;
                        case 4:
                            soundFileName = @"Sound\GrandEntrance.mp3";
                            break;
                        case 5:
                            soundFileName = @"Sound\GrandEntrance.mp3";
                            break;
                    }
                    break;
                case SoundEvents.SystemStop:
                    switch (randomNumber)
                    {
                        case 1:
                            soundFileName = @"Sound\Beeps.mp3";
                            break;
                        case 2:
                            soundFileName = @"Sound\Sneaky.mp3";
                            break;
                        case 3:
                            soundFileName = @"Sound\Reverse.mp3";
                            break;
                        case 4:
                            soundFileName = @"Sound\BlinkBlink.mp3";
                            break;
                        case 5:
                            soundFileName = @"Sound\Beeps.mp3";
                            break;
                    }
                    break;
                case SoundEvents.AlignmentStart:
                    switch (randomNumber)
                    {
                        case 1:
                            soundFileName = @"Sound\HardEntry.mp3";
                            break;
                        case 2:
                            soundFileName = @"Sound\transformers-type-sfx-2.mp3.mp3";
                            break;
                        case 3:
                            soundFileName = @"Sound\transformers-type-sfx-2.mp3.mp3";
                            break;
                        case 4:
                            soundFileName = @"Sound\transformers-type-sfx-2.mp3.mp3";
                            break;
                        case 5:
                            soundFileName = @"Sound\HardEntry.mp3";
                            break;
                    }
                    break;
                case SoundEvents.AlignmentFailed:
                    switch (randomNumber)
                    {
                        case 1:
                            soundFileName = @"Sound\HeyWhatHappened.mp3";
                            break;
                        case 2:
                            soundFileName = @"Sound\OMG.mp3";
                            break;
                        case 3:
                            soundFileName = @"Sound\Bruh.mp3";
                            break;
                        case 4:
                            soundFileName = @"Sound\SwordEntry.mp3";
                            break;
                        case 5:
                            soundFileName = @"Sound\SadViolin.mp3";
                            break;
                    }
                    break;
                case SoundEvents.AlignmentSuccess:
                    switch (randomNumber)
                    {
                        case 1:
                            soundFileName = @"Sound\TrollBell.mp3";
                            break;
                        case 2:
                            soundFileName = @"Sound\LastStandTick.mp3";
                            break;
                        case 3:
                            soundFileName = @"Sound\PartyHorn.mp3";
                            break;
                        case 4:
                            soundFileName = @"Sound\TwoToneAlert.mp3";
                            break;
                        case 5:
                            soundFileName = @"Sound\A_Few_Moments_Later.mp3";
                            break;
                    }
                    break;
                case SoundEvents.DefectFound:
                    switch (randomNumber)
                    {
                        case 1:
                            soundFileName = @"Sound\Bop.mp3";
                            break;
                        case 2:
                            soundFileName = @"Sound\Bop.mp3";
                            break;
                        case 3:
                            soundFileName = @"Sound\Bop.mp3";
                            break;
                        case 4:
                            soundFileName = @"Sound\Bop.mp3";
                            break;
                        case 5:
                            soundFileName = @"Sound\Bop.mp3";
                            break;
                    }
                    break;
                case SoundEvents.WaitingForOperator:
                    switch (randomNumber)
                    {
                        case 1:
                            soundFileName = @"";
                            break;
                        case 2:
                            soundFileName = @"";
                            break;
                        case 3:
                            soundFileName = @"";
                            break;
                        case 4:
                            soundFileName = @"";
                            break;
                        case 5:
                            soundFileName = @"";
                            break;
                    }
                    break;
                case SoundEvents.OperatorCancel:
                    switch (randomNumber)
                    {
                        case 1:
                            soundFileName = @"Sound\SadViolin.mp3";
                            break;
                        case 2:
                            soundFileName = @"Sound\Run.mp3";
                            break;
                        case 3:
                            soundFileName = @"Sound\Cancel.wav";
                            break;
                        case 4:
                            soundFileName = @"Sound\Suspense.mp3";
                            break;
                        case 5:
                            soundFileName = @"Sound\LastStandTick.mp3";
                            break;
                    }
                    break;
                case SoundEvents.WaferScanStart:
                    switch (randomNumber)
                    {
                        case 1:
                            soundFileName = @"Sound\Investigating.mp3";
                            break;
                        case 2:
                            soundFileName = @"Sound\BuildUp.mp3";
                            break;
                        case 3:
                            soundFileName = @"Sound\Suspense.mp3";
                            break;
                        case 4:
                            soundFileName = @"Sound\QuietBeeps.mp3";
                            break;
                        case 5:
                            soundFileName = @"Sound\DeepStab.mp3";
                            break;
                    }
                    break;
                case SoundEvents.WaferScanComplete:
                    switch (randomNumber)
                    {
                        case 1:
                            soundFileName = @"Sound\HeavenlyChoir_Complete.mp3";
                            break;
                        case 2:
                            soundFileName = @"Sound\GTA_Complete.mp3";
                            break;
                        case 3:
                            soundFileName = @"Sound\ComercialBreak.mp3";
                            break;
                        case 4:
                            soundFileName = @"Sound\ComercialBreak.mp3";
                            break;
                        case 5:
                            soundFileName = @"Sound\Finished_Light.mp3";
                            break;
                    }
                    break;
                case SoundEvents.UnexpectedError:
                    switch (randomNumber)
                    {
                        case 1:
                            soundFileName = @"Sound\HeyWhatHappened.mp3";
                            break;
                        case 2:
                            soundFileName = @"Sound\OMG.mp3";
                            break;
                        case 3:
                            soundFileName = @"Sound\Bruh.mp3";
                            break;
                        case 4:
                            soundFileName = @"Sound\SwordEntry.mp3";
                            break;
                        case 5:
                            soundFileName = @"Sound\SadViolin.mp3";
                            break;
                    }
                    break;
                case SoundEvents.SwitchUser:
                    switch (randomNumber)
                    {
                        case 1:
                            soundFileName = @"Sound\Unlocked.mp3";
                            break;
                        case 2:
                            soundFileName = @"Sound\TinkerBell_Next.mp3";
                            break;
                        case 3:
                            soundFileName = @"Sound\Unlocked.mp3";
                            break;
                        case 4:
                            soundFileName = @"Sound\Unlocked.mp3";
                            break;
                        case 5:
                            soundFileName = @"Sound\TinkerBell_Next.mp3";
                            break;
                    }
                    break;
                case SoundEvents.SwitchToAdmin:
                    switch (randomNumber)
                    {
                        case 1:
                            soundFileName = @"Sound\WhoAreYou_SwitchUser.mp3";
                            break;
                        case 2:
                            soundFileName = @"Sound\TinkerBell_Next.mp3";
                            break;
                        case 3:
                            soundFileName = @"Sound\Unlocked.mp3";
                            break;
                        case 4:
                            soundFileName = @"Sound\WhoAreYou_SwitchUser.mp3";
                            break;
                        case 5:
                            soundFileName = @"Sound\WhoAreYou_SwitchUser.mp3";
                            break;
                    }
                    break;
                case SoundEvents.Click:
                    switch (randomNumber)
                    {
                        case 1:
                            soundFileName = @"Sound\LowDrumHit.mp3";
                            break;
                        case 2:
                            soundFileName = @"Sound\Beeps.mp3";
                            break;
                        case 3:
                            soundFileName = @"Sound\Bop.mp3";
                            break;
                        case 4:
                            soundFileName = @"Sound\Woosh.mp3";
                            break;
                        case 5:
                            soundFileName = @"Sound\ThumbBoop.mp3";
                            break;
                    }
                    break;
            }

            string actualFileName = Path.Combine(Environment.CurrentDirectory, soundFileName);
            if (soundFileName != "" && File.Exists(actualFileName))
            {
                PlaySound(actualFileName, cts.Token);
            }

            return cts;
        }
    }
    /// <summary>
    /// Sound Events associated with noise.
    /// </summary>
    public enum SoundEvents
    {
        /// <summary>
        /// Swift System Startup
        /// </summary>
        SystemStart,
        /// <summary>
        /// Swift System Shutdown
        /// </summary>
        SystemStop,
        /// <summary>
        /// Alignment Start
        /// </summary>
        AlignmentStart,
        /// <summary>
        /// Alignment Failed
        /// </summary>
        AlignmentFailed,
        /// <summary>
        /// Alignment Success
        /// </summary>
        AlignmentSuccess,
        /// <summary>
        /// Defect Found
        /// </summary>
        DefectFound,
        /// <summary>
        /// Waiting for Operator
        /// </summary>
        WaitingForOperator,
        /// <summary>
        /// Operator Cancel
        /// </summary>
        OperatorCancel,
        /// <summary>
        /// Wafer Scan Start
        /// </summary>
        WaferScanStart,
        /// <summary>
        /// Wafer Scan Complete
        /// </summary>
        WaferScanComplete,
        /// <summary>
        /// Switch User
        /// </summary>
        SwitchUser,
        /// <summary>
        /// Switch to Admin
        /// </summary>
        SwitchToAdmin,
        /// <summary>
        /// Click Sound
        /// </summary>
        Click,
        /// <summary>
        /// Unexpected Error
        /// </summary>
        UnexpectedError,
    }

}
