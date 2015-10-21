using System;

namespace MindSetUWA
{
    public class MindsetDataStruct
    {
        private DateTime now;
        private object p1;
        private object p10;
        private object p2;
        private object p3;
        private object p4;
        private object p5;
        private object p6;
        private object p7;
        private object p8;
        private object p9;
        private int v;
        public MindsetDataStruct()
        {
        }

        public MindsetDataStruct(int quality, int delta, int theta, int alphaLow, int alphaHigh, int betaLow, int betaHigh, int gammaLow, int gammaMid, int eSenseAttention, int eSenseMeditation, DateTime timestamp)
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

        public MindsetDataStruct(object p1, int v, object p2, object p3, object p4, object p5, object p6, object p7, object p8, object p9, object p10, DateTime now)
        {
            this.p1 = p1;
            this.v = v;
            this.p2 = p2;
            this.p3 = p3;
            this.p4 = p4;
            this.p5 = p5;
            this.p6 = p6;
            this.p7 = p7;
            this.p8 = p8;
            this.p9 = p9;
            this.p10 = p10;
            this.now = now;
        }

        public int AlphaHigh { get; private set; }

        public int AlphaLow { get; private set; }

        /// <summary>
        /// Outputs esense attention level ranged from 0 to 100. Outputs 0 when connection quality is insufficent.
        /// </summary>
        public int Attention { get; private set; }

        public int BetaHigh { get; private set; }

        public int BetaLow { get; private set; }

        public int Delta { get; set; }

        public int GammaLow { get; private set; }

        public int GammaMid { get; private set; }

        public bool IsQualityOK { get { return Quality == 0; } }
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
