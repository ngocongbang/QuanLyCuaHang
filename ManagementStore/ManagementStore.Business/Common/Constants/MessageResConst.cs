using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStore.Business.Common.Constants
{
    public class MessageResConst
    {
        public const string NameKellyPos = "KellyGTS POS";
        public const string NameNeandr = "Neandr";
        public const string PathReturnFiles = "{0}/api/content/data/files/{1}";
        public const string PathReturnOriginImages = "{0}/api/content/data/images/original/{1}";
        public const string PathReturnThumbnailImages = "{0}/api/content/data/images/thumbnail/{1}";

        public const string PathSaveFiles = "~/Content/Data/Files";
        public const string PathSaveImages = "~/Content/Data/Images";
        public const string PathSaveOriginImages = "~/Content/Data/Images/original";
        public const string PathSaveThumbnailImages = "~/Content/Data/Images/thumbnail";
        public const int MaxFileSize = 1024 * 1024;

        public const string Success = "Success";
        public const string ErrorCommonRequestParam = "The request was unacceptable, often due to missing a required parameter";
        public const string ErrorSystem = "Internal server error";
        public const string NotExistMsg = "This record is not exist in DB";
        public const int PageSize = 10;

        // Register user
        public const string ErrorRequiredEmail = "The Email is required.";
        public const string ErrorRequiredPassword = "The password is required.";
        public const string ErrorRequiredConfirmPassword = "The confirm password is required.";
        public const string ErrorRequiredFirstName = "The first name is required.";
        public const string ErrorRequiredLastName = "The last name is required.";
        public const string ErrorNotMatchPassword = "The password and confirmation password do not match.";
        public const string ErrorEmailExists = "Email {0} is already taken.";

        // Register Restaurant
        public const string ErrorRestaurantID = "RestaurantID not Exist in data base";
        public const string ErrorRestaurantName = "the Restaurant Name is required";
        public const string ErrorRestaurantAddress1 = "The Address is required";
        public const string ErrorRestaurantCountry = "The Country is required";

        // Register Menu
        public const string ErrorMenuID = "Menu ID not Exist in data base";
        public const string ErrorMenuName = "The Menu Name is required";
        public const string ErrorMenuCurrency = "The Currency Sympol is required";
        public const string ErrorMenuLanguage = "The Language is required";
        public const string ErrorMenuDisable = "The Disable is required";
        public const string ErrorMenuDuration = "The Menu Duration Name is required";

        // Register Menu_Groups
        public const string ErrorMenuGroupID = "The Group ID not Exist in data base";
        public const string ErrorMenuGroupName = "The Group Name is required";
        public const string ErrorMenuGroupUid = "The Group UID is required";
        public const string ErrorMenuGroupDisable = "The Group Disable is required";

        // Register Menu_Items
        public const string ErrorMenuItemID = "The Item ID not Exist in data base";
        public const string ErrorMenuItemName = "The Item Name is required";
        public const string ErrorMenuItemUid = "The Item UID is required";
        public const string ErrorMenuItemDisable = "The Item Disable is required";

        // Register Menu_Item_Tags
        public const string ErrorMenuItemTagID = "The Item Tag ID not Exist in data base";
        public const string ErrorMenuItemTag = "The Item Tag is required";

        // Register Menu_Item_Sizes
        public const string ErrorMenuItemSizeID = "The Item Size ID not Exist in data base";
        public const string ErrorMenuItemSizeName = "The Item Size Name is required";

        // Register Menu_Item_Options
        public const string ErrorMenuItemModifiersID = "The Item Size ID not Exist in data base";

        // Register Menu_Item_Option_Item
        public const string ErrorMenuItemModifierOptionsID = "The Option Item ID not Exist in data base";

        // Register Menu_Item_Images
        public const string ErrorMenuItemImagesID = "The Item Image ID not Exist in data base";

        // Register Menu_Group_Options
        public const string ErrorMenuGroupOptionsID = "The Group Option ID not Exist in data base";

        // Register Menu_Group_Option_Item
        public const string ErrorMenuGroupOptionItemID = "The Option Item ID not Exist in data base";

        // Constants for sending mail
        public const string SubjectResetPasswordPos = "Reset your KellyGTS POS Password";
        public const string SubjectResetPasswordConsumer = "Reset your Neandr Password";
        public const string SubjectWellcomeAccountPos = "Welcome KellyGTS POS account";
        public const string SubjectWellcomeAccountConsumer = "Welcome Neandr account";
        public const string SubjectResetPasswordSuccessPos = "Your KellyGTS POS Password has been reset";
        public const string SubjectResetPasswordSuccessConsumer = "Your Neandr Password has been reset";

        public const string ResetPasswordMailTemplateUrlPos = "~/Views/Email/ResetPasswordPos.cshtml";
        public const string ResetPasswordMailTemplateUrlConsumer = "~/Views/Email/ResetPasswordConsumer.cshtml";

        public const string ResetPasswordMailTemplateSucessUrlPos = "~/Views/Email/ResetPasswordSuccessPos.cshtml";
        public const string ResetPasswordMailTemplateSuccessUrlConsumer = "~/Views/Email/ResetPasswordSuccessConsumer.cshtml";
        public const string ResetPasswordMailTemplateSucessUrl = "~/Views/Email/ResetPasswordSuccess.cshtml";

        public const string WelcomeAccountMailTemplateUrlPos = "~/Views/Email/ResetPasswordPos.cshtml";
        public const string WelcomeAccountMailTemplateUrlConsumer = "~/Views/Email/ResetPasswordConsumer.cshtml";

        public const string ResetPasswordViewName = "ResetPassword";
        public const string ForgotPasswordControllerName = "AccountPassword";

        public const string ForgotPasswordViewName = "ForgotPassword";
    }
}
