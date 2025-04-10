using CareerCompanionApp.Views;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("LoginPage", typeof(LoginPage));
        Routing.RegisterRoute("DashboardPage", typeof(DashboardPage));
        Routing.RegisterRoute("ResumePage", typeof(ResumePage));
        Routing.RegisterRoute("JobSearchPage", typeof(JobSearchPage));
        Routing.RegisterRoute("QuizPage", typeof(QuizPage));
       

    }

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }
}