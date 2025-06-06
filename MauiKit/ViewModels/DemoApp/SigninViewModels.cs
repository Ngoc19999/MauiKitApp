using MauiKit.Services;
using MauiKit.Views.DemoApp;



namespace MauiKit.ViewModels.DemoApp
{
    public class SigninViewModel : INotifyPropertyChanged
    {
        private readonly ApiServiceBase _apiService;

        public string FullName { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; }
        public ICommand NavigateToLoginCommand { get; }
        public ICommand NavigateToForgotPasswordCommand { get; }

        private bool isLoggingIn;    

        public bool IsLoggingIn
        {
            get => isLoggingIn;
            set { isLoggingIn = value; OnPropertyChanged(nameof(IsLoggingIn)); }
        }

        public SigninViewModel()
        {
            _apiService = new ApiServiceBase();
            LoginCommand = new Command(async () => await OnLogin());
            NavigateToLoginCommand = new Command(async () => await NavigateToLogin());
            NavigateToForgotPasswordCommand = new Command(async () => await NavigateToForgotPassword());
        }
        private async Task NavigateToLogin()
        {
            await App.Current.MainPage.Navigation.PushAsync(new RegisterUser());
        }

        private async Task NavigateToForgotPassword()
        {
            await App.Current.MainPage.Navigation.PushAsync(new ForgotPassword());
        }

        private async Task OnLogin()
        {
            IsLoggingIn = true;

            if (string.IsNullOrWhiteSpace(FullName))
            {
                await App.Current.MainPage.DisplayAlert("Lỗi!", "Tài khoản không được để trống!", "OK");
                IsLoggingIn = false;
                return;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                await App.Current.MainPage.DisplayAlert("Lỗi!", "Mật khẩu không được để trống!", "OK");
                IsLoggingIn = false;
                return;
            }

            var loginDto = new LoginDto
            {
                Account = FullName,
                Password = Password
            };


            var response = await _apiService.PostAsync<LoginDto, MauiKitUser>("ThueLai/login", loginDto);



            if (response.Code == 0)
            {
                await App.Current.MainPage.DisplayAlert("Thành công!", $"Đăng nhập thành công", "OK");
                UserSession.CurrentUser = response.Data;
                var style = UserSession.CurrentUser.Style;
                if (style == 1) 
                {
                    // view user
                    App.Current.MainPage = new NavigationPage(new MauiKitTabbedPageUser());

                }
               
                if (style == 2)
                {
                    // view kid
                    App.Current.MainPage = new NavigationPage(new MauiKitTabbedPage());

                }
                
                //{ response.Data?.Name}
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Lỗi", response.Message, "OK");
            }

            IsLoggingIn = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}


