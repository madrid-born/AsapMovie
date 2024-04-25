using AsapMovie.Pages;

namespace AsapMovie ;

    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new InitialPage();
        }
    }