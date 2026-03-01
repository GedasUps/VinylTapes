using System;

namespace VinylExchange.Client.Services
{
    public class AuthStateService
    {
        public bool IsAuthenticated { get; private set; }
        public string? UserEmail { get; private set; }

        public event Action? OnChange;

        public void MarkLoggedIn(string token, string email)
        {
            IsAuthenticated = !string.IsNullOrEmpty(token);
            UserEmail = email;
            NotifyStateChanged();
        }

        public void MarkLoggedOut()
        {
            IsAuthenticated = false;
            UserEmail = null;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
