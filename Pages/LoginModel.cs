using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Threading.Tasks;
using System;
using toDo.infrastructure;

namespace toDo
{
    public partial class LoginModel: ComponentBase
    {
        [Inject] public IALocalStorageService LocalStorageService {get; set;}
        [Inject] public NavigationManager NavigationManager {get; set;}
        public LoginModel()
        {
            LoginData = new LoginViewModel();
        }
    public LoginViewModel LoginData {get ; set ; }
           protected async Task LoginAsync()
        {
            var token = new SecurityToken
            {
                AccessToken = LoginData.passWord,
                userName = LoginData.userName,
                ExpiredAt = DateTime.UtcNow.AddDays(1)
            };
            await LocalStorageService.SetAsync(nameof(SecurityToken), token);
            NavigationManager.NavigateTo("/", true);
    }   
    public class LoginViewModel
    {
        [RequiredAttribute]
        [StringLength(50,ErrorMessage ="Too long!")]
        public string userName {get; set;}
        [RequiredAttribute]
        public string passWord {get; set;}    
    }   
            public class  SecurityToken
        {
            public string userName {get; set;}
            public string AccessToken {get; set;}
            public DateTime ExpiredAt {get; set;}
        }
}
}