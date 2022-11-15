using CustomMusicCreator;
using System.Windows.Controls;

namespace PanPakapon
{
    public partial class VoiceSelector : UserControl
    {
        public bool HasValidVoice => VoiceList.SelectedIndex > -1;
        public string VoiceName
            => HasValidVoice?
            ((VoiceList.Items[VoiceList.SelectedIndex].ToString())??"")
            :"";
        public VoiceSelector()
        {
            InitializeComponent();
            var voices= VoiceData.Get().Voices;
            VoiceList.ItemsSource = voices;
            if (VoiceList.Items.Count > 0)
            {
                VoiceList.SelectedIndex = 0;
            }
        }
    }
}
