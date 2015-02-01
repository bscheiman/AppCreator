namespace AppCreator.Controls {
    [ImplementPropertyChanged]
    public class Spacer : Frame {
        public int Space { get; set; }

        public Spacer() {
            InitializeComponent();

            Space = 10;
        }

        public void OnSpaceChanged() {
            Padding = new Thickness(0, 0, 0, Space);
            BackgroundColor = Color.Transparent;
        }
    }
}