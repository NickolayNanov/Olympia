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

        public const string Login = "/Accounts/Login";
        public const string AdminArticlesAll = "/AreaAdministration/Administration/ArticlesAll";
        public const string Index = "/";

        public const string ErrorPage = "/Home/Error";
        #endregion

        #region Errors
        public const string ErrorInputMessage = "The {0} must be at least {1} and at max {2} characters long.";
        public const string ErrorInputNumberMessage = "The {0} must be between {1} and {2}.";
        public const string ErrorWeightHeightMessage = "The height and weight must be positive number.";
        public const string ErrorDescriptionMessage = "The {0} must be at least {2} and at max {1} characters long.";

        public const string ConfirmPasswordErrorMessage = "The password and confirmation password do not match.";
        public const string AgeErrorMessage = "Your age must be between 12 and 65";
        public const string ItemPriceErrorMessage = "The price must be at least 0.01лв.";
        public const string EmailError = "The email is required";
        public const string GenderErrorMessage = "Gender is required. If you wish you can choose 'unknown'";

        public const string CommentLengthMessage = "The content's lenght must be between {0} and {1} characters long.";

        public const string InvalidRegisterMessage = "Some of the input data is invalid. Please try again!";
        public const string InvalidLoginMessage = "Invalid username or password";
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

        public const string DisplayDuration = "Duration";
        public const string AlreadyAddedThisItem = "This item has already been added in the cart";

        #endregion
    }
}
