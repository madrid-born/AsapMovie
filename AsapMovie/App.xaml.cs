using AsapMovie.Pages;

namespace AsapMovie ;

    public partial class App : Application
    {
        public App(InitialPage mainPage)
        {
            InitializeComponent();

            MainPage = mainPage;
        }
    }