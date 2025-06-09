
using MauiKit.Services;

namespace MauiKit.ViewModels
{
    public partial class OnboardingViewModel : ObservableObject
    {
        private readonly UserOnboardingService _onboardingService;

        public OnboardingViewModel()
        {
            _onboardingService = new UserOnboardingService();
            
        }

        [RelayCommand]
        private async Task SelectFamilyAsync()
        {
            await SaveAndNavigate("family");
        }

        [RelayCommand]
        private async Task SelectCoupleAsync()
        {
            await SaveAndNavigate("couple");
        }

        [RelayCommand]
        private async Task SelectBusinessAsync()
        {
            await SaveAndNavigate("business");
        }

        private async Task SaveAndNavigate(string role)
        {
            await _onboardingService.SetUserRoleAsync(role);
            //await Shell.Current.GoToAsync($"//Home{ToPascal(role)}Page");

            await Shell.Current.GoToAsync("//LocationMapPage?sv="+ role);
        }

        private string ToPascal(string role)
        {
            return char.ToUpper(role[0]) + role.Substring(1); // "family" => "Family"
        }
    }
}
