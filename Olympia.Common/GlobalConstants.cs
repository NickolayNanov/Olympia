namespace Olympia.Common
{
    public static class GlobalConstants
    {
        #region Areas and roles
        public const string AdministratorRoleName = "Administrator";
        public const string TrainerRoleName = "Trainer";
        public const string TrainerAdministratorRoleName = "Trainer,Administrator";
        public const string ClientRoleName = "Client";

        public const string AdministrationArea = "AreaAdministration";
        public const string TrainerArea = "AreaTrainer";
        public const string ClientArea = "AreaClient";
        public const string BlogArea = "AreaBlog";
        public const string ShopArea = "AreaShop";

        #endregion

        #region Routes
        public const string ClientTrainersAll = "/AreaClient/Client/TrainersAll";
        public const string TrainerMyArticles = "/AreaTrainer/Trainer/MyArticles";
        public const string TrainerCreateArticle = "/AreaTrainer/Trainer/CreateArticle";
        public const string AdministrationUsers = "/AreaAdministration/Administration/UsersAll";
        public const string AdministrationArticles = "/AreaAdministration/Administration/ArticlesAll";
        public const string Blog = "/AreaBlog/Blog/";
        public const string Shop = "/AreaShop/Shop/";
        public const string ChooseExercisesRoute = "/AreaTrainer/Trainer/ChooseWorkout";

        public const string AccountRegister = "/Accounts/Register";
        public const string AccountLogin = "/Accounts/Login";
        public const string Index = "/";
        #endregion

        #region Errors
        public const string ErrorInputMessage = "The {0} must be at least {2} and at max {1} characters long.";

        public const string ConfirmPasswordErrorMessage = "The password and confirmation password do not match.";
        public const string AgeErrorMessage = "Your age must be between 12 and 65";
        public const string ItemPriceErrorMessage = "The price must be at least 0.01лв.";
        public const string EmailError = "The email is required";
        public const string GenderErrorMessage = "Gender is required. If you wish you can choose 'unknown'";

        public const string CommentLengthMessage = "The content's lenght must be between {0} and {1} characters long.";
        #endregion

        #region Display names
        public const string DisplayUsername = "Username";
        public const string DisplayPassword = "Password";
        public const string DisplayConfirmPassword = "Confirm password";
        public const string DisplayRememberMe = "Remember me?";

        public const string DisplayTitle = "Title";
        public const string DisplayContent = "Content";
        public const string DisplayImg = "Display Image";
        public const string DisplayProfilePic = "Profile Picture";

        public const string DisplayCategoryName = "Category";

        #endregion
    }
}
