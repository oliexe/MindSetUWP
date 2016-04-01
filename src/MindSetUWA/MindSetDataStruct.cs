using System;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;

namespace MindSetUWP
{
    public class MindsetDataStruct 
    {
        /// <summary>
        /// A structure that contains EEG data recieved from the headset.
        /// </summary>
        public MindsetDataStruct(){}

        /// <summary>
        /// A structure that contains EEG data recieved from the headset.
        /// </summary>
        public MindsetDataStruct(int quality, int delta, int theta, int alphaLow, 
            int alphaHigh, int betaLow, int betaHigh, int gammaLow, int gammaMid, 
            int eSenseAttention, int eSenseMeditation, DateTime timestamp)
        {
            Quality = quality;
            Delta = delta;
            Theta = theta;
            AlphaLow = alphaLow;
            AlphaHigh = alphaHigh;
            BetaLow = betaLow;
            BetaHigh = betaHigh;
            GammaLow = gammaLow;
            GammaMid = gammaMid;
            this.Attention = eSenseAttention;
            this.Meditation = eSenseMeditation;
            Timestamp = timestamp;
        }

        /// <summary>
        /// Alpha brainwaves are dominant during quietly flowing thoughts, and in some meditative states. 
        /// Alpha is ‘the power of now’, being here, in the present. Alpha is the resting state for the brain.
        /// </summary>
        public int AlphaHigh { get; private set; }

        /// <summary>
        /// Alpha brainwaves are dominant during quietly flowing thoughts, and in some meditative states. 
        /// Alpha is ‘the power of now’, being here, in the present. Alpha is the resting state for the brain.
        /// </summary>
        public int AlphaLow { get; private set; }

        /// <summary>
        /// Outputs esense attention level ranged from 0 to 100. Outputs 0 when connection quality is insufficent.
        /// </summary>
        public int Attention { get; private set; }

        /// <summary>
        /// Beta brainwaves dominate our normal waking state of consciousness when attention is 
        /// directed towards cognitive tasks and the outside world. 
        /// </summary>
        public int BetaHigh { get; private set; }

        /// <summary>
        /// Beta brainwaves dominate our normal waking state of consciousness when attention is 
        /// directed towards cognitive tasks and the outside world. 
        /// </summary>
        public int BetaLow { get; private set; }

        /// <summary>
        /// Delta brainwaves are the slowest but loudest brainwaves (low frequency and deeply penetrating, like a drum beat). 
        /// They are generated in deepest meditation and dreamless sleep.
        /// </summary>
        public int Delta { get; set; }

        /// <summary>
        /// Gamma brainwaves are the fastest of brain waves (high frequency, like a flute), 
        /// and relate to simultaneous processing of information from different brain areas.
        /// </summary>
        public int GammaLow { get; private set; }

        /// <summary>
        /// Gamma brainwaves are the fastest of brain waves (high frequency, like a flute), 
        /// and relate to simultaneous processing of information from different brain areas.
        /// </summary>
        public int GammaMid { get; private set; }

        /// <summary>
        /// Outputs esense meditation level ranged from 0 to 100. Outputs 0 when connection quality is insufficent.
        /// </summary>
        public int Meditation { get; private set; }

        /// <summary>
        /// A EEG signal quality ranged from 0-200
        /// Lower is better. 0 is no quality issues.
        /// Must be 0 to recieve eSense data. Adjust the headset on your head to get better results.
        /// </summary>
        public int Quality { get; private set; }

        /// <summary>
        /// Theta brainwaves occur most often in sleep but are also dominant 
        /// in deep meditation. It acts as our gateway to learning and memory.
        /// </summary>
        public int Theta { get; private set; }

        /// <summary>
        /// Outputs a timestamp of the last recieved data in DateTime format.
        /// </summary>
        public DateTime Timestamp { get; private set; }

        /// <summary>
        /// Outputs all EEG data in integer array. In this order: Connection Quality, Delta, Theta, Low Alpha, High Alpha
        /// Low Beta, High Beta, Low Gamma, Mid Gamma, Attention, Meditation.
        /// </summary>
        public int[] AllToIntArray()
        {
            return new int[] { Quality, Delta, Theta, AlphaLow, AlphaHigh, BetaLow, BetaHigh, GammaLow, GammaMid, Attention, Meditation };
        }

        /// <summary>
        /// Outputs a formatted string of all EEG data. (except timestamp).
        /// </summary>
        public string AllToString()
        {
            return string.Format(@"Quality: {0}, Delta: {1}, Theta: {2}, AlphaLow: {3}, AlphaHigh: {4}, BetaLow: {5}, BetaHigh: {6}, GammaLow: {7}, GammaMid: {8}, eSenseAttention: {9}, eSenseMeditation: {10}", Quality, Delta, Theta, AlphaLow, AlphaHigh, BetaLow, BetaHigh, GammaLow, GammaMid, Attention, Meditation);
        }

        /// <summary>
        /// Outputs all EEG data in string array. In this order: Connection Quality, Delta, Theta, Low Alpha, High Alpha
        /// Low Beta, High Beta, Low Gamma, Mid Gamma, Attention, Meditation.
        /// </summary>
        public string[] AllToStringArray()
        {
            return new string[] { Quality.ToString(), Delta.ToString(), Theta.ToString(), AlphaLow.ToString(), AlphaHigh.ToString(), BetaLow.ToString(), BetaHigh.ToString(), GammaLow.ToString(), GammaMid.ToString(), Attention.ToString(), Meditation.ToString() };
        }
    }
}
