namespace SubscriptionManager.IntegrationTests.HelperExtensions
{
    public class HelperExtension
    {
        private static readonly string[] _usernames =
        {
            "Dayou", "Pesso", "Doudou", "Momo", "Toto",
            "Lolo", "Roro", "Kiki", "Bobo", "Fifi"
        };

        private static readonly string[] DisplayName =
        {
            "Dayou", "Pesso", "Doudou", "Momo", "Toto",
            "Lolo", "Roro", "Kiki", "Bobo", "Fifi"
        };

        private static readonly string[] Email =
        {
            "dayou@example.com",
            "pesso@test.com",
            "doudou@mail.net",
            "momo@domain.com",
            "toto@email.org",
            "lolo@inbox.com",
            "roro@webmail.net",
            "kiki@outlook.com",
            "bobo@gmail.com",
            "fifi@yahoo.com"
        };
        private static readonly string[] Roles =
        {
            "Admin",
            "User"
        };
        private static readonly Random _random = new Random();

        public static string GetRandomUsername() =>
            _usernames[_random.Next(_usernames.Length)];

        public static string GetRandomDisplayName() =>
            DisplayName[_random.Next(DisplayName.Length)];

        public static string GetRandomEmail() =>
            Email[_random.Next(Email.Length)];

        public static string GetRandomRoles() =>
        Roles[_random.Next(Roles.Length)];
        

        public static string GenerateRandomPassword(int length = 12)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*";
            var password = new char[length];

            for (int i = 0; i < length; i++)
            {
                password[i] = chars[_random.Next(chars.Length)];
            }

            return new string(password);
        }
    }
}