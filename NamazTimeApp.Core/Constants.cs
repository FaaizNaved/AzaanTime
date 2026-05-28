namespace NamazTimeApp.Core
{
    public static class Constants
    {
        public static class Common
        {
            public static readonly string NAMAZTIMEAPP_MIGRATION_ASSEMBLY = "NamazTimeApp.Data.Security";
            public static readonly string NAMAZTIMEAPP_LOG_MIGRATION_ASSEMBLY = "NamazTimeApp.Data.Log";
            public static readonly string NAMAZTIMEAPP_INFRASTRUCTURE_MIGRATION_ASSEMBLY = "NamazTimeApp.Data.Infrastructure";
            public static readonly string FORMAT_APPLICATION_JSON = "application/json";
            public static readonly string WEB_API_CLIENT = "webApiClient";
            public static readonly string AUTH_TOKEN = "authToken";
            public static readonly string AUTH_HEADER_BEARER = "bearer";
            public static readonly string RECORD_SOURCE = "NamazTimeApp_Application_API";

        }

        public static class ConfigurationSections
        {
            public static readonly string GENERAL_CONFIG = "GeneralConfig";
            public static readonly string IDENTITY_CONFIG = "IdentityConfig";
            public static readonly string EMAIL_CONFIG = "EmailConfig";
            public static readonly string FILE_CONFIG = "FileConfig";
            public static readonly string AWS_CONFIG = "AWSConfig";
            public static readonly string AUTH_CONFIG = "AuthConfig";
            public static readonly string DARWINBOXAPI_CONFIG = "DarwinBoxApiConfig";
            public static readonly string HANGFIRE_CONFIG = "HangfireConfig";
            public static readonly string HEALTH_CHECKUP_CONFIG = "HealthcheckupConfig";
        }

        public static class ConfigurationKeys
        {
            public static readonly string CONNECTION_STRING = "SecurityDbContextConnection";


            #region General



            #endregion

            #region Identity

            public static readonly string JWT_ISSUER = "JwtIssuer";
            public static readonly string JWT_AUDIENCE = "JwtAudience";
            public static readonly string JWT_SECURITY_KEY = "JwtSecurityKey";
            public static readonly string JWT_EXPIRY_IN_DAYS = "JwtExpiryInDays";
            public static readonly string JWT_EXPIRY_IN_MINUTES = "JtExwpiryInMinutes";
            public static readonly string JWT_EXPIRY_IN_HOURS = "JwtExpiryInHours";

            #endregion

            #region Email

            public static readonly string SMTP_FROM_ADDRESS = "SmtpFromAddress";
            public static readonly string SMTP_HOST = "SmtpHost";
            public static readonly string SMTP_PORT = "SmtpPort";
            public static readonly string SMTP_USERNAME = "SmtpUsername";
            public static readonly string SMTP_PASSWORD = "SmtpPassword";

            #endregion

            #region File



            #endregion

            #region Auth

            public static readonly string API_KEY = "ApiKey";
            public static readonly string DATASET_KEY = "DataSetKey";
            #endregion

            #region DarwinBoxApiConfig

            public static readonly string API_URL = "ApiUrl";
            public static readonly string USERNAME = "Username";
            public static readonly string PASSWORD = "Password";
            public static readonly string DARWINBOX_API_KEY = "ApiKey";
            public static readonly string DARWINBOX_DATASET_KEY = "DatasetKey";

            #endregion

            #region File

            public static readonly string TEMP_FOLDER_PATH = "TempFolderPath";
            public static readonly string PERMANENT_FOLDER_PATH = "PermanentFolderPath";
            public static readonly string LOCATION_IMAGE_FOLDER_PATH = "LocationImageFolderPath";

            #endregion

            #region AWSConfig

            public static readonly string ACCESS_KEY = "AccessKey";
            public static readonly string SECRET_KEY = "SecretKey";
            public static readonly string REGION = "Region";
            public static readonly string BUCKET_NAME = "BucketName";
            public static readonly string BUCKET_FOLDER_PATH = "BucketFolderPath";
            public static readonly string DOCUMENT_FOLDER_NAME = "DocumentFolderName";

            #endregion

            #region HangfireConfig

            public static readonly string JOBS = "Jobs";
            public static readonly string ENABLED = "Enabled";
            public static readonly string CRON = "CronExpression";
            public static readonly string ALLOWED_HOSTS = "AllowedHosts";

            public static readonly string SA_FORM_ACTIVATION = "saFormActivation";
            public static readonly string SA_FORM_NOTIFICATION = "saFormNotification";
            public static readonly string EMPLOYEE_DATA_SYNC = "EmployeeDataSync";
            public static readonly string SYSTEM_HEALTH_CHECK = "SystemHealthCheck";

            #endregion

        }

        public static class WebAPIEndpoints
        {
            public static readonly string MENUS_FIND_ALL = "api/Menus";

        }

        public static class DatastubConstants
        {

            public static readonly string RECORD_SOURCE_NAME_NAMAZTIMEAPP_APPLICATION_API = "NamazTimeApp Application API";

            #region UserType

            public static readonly int USER_TYPE_INTERNAL = 1;
            public static readonly int USER_TYPE_EXTERNAL = 2;

            #endregion


            #region User

            public static readonly int USER_ID_SYSTEM = 1;
            public static readonly string USER_LOGIN_ID_SYSTEM = "system";
            public static readonly string USER_PASSWORD_SYSTEM = "";
            public static readonly string USER_EMAIL_ID_SYSTEM = "system@Annuity.com";
            public static readonly string USER_AUTH_TYPE_SYSTEM = "Form";

            public static readonly int USER_ID_ADMIN = 2;
            public static readonly string USER_LOGIN_ID_ADMIN = "super@admin";
            public static readonly string USER_PASSWORD_ADMIN = "";
            public static readonly string USER_EMAIL_ID_ADMIN = "superadmin@gmail.com";
            public static readonly string USER_AUTH_TYPE_ADMIN = "Form";

            #endregion

            #region Status

            public static readonly int STATUS_ID_PENDING = 1;
            public static readonly string STATUS_NAME_PENDING = "Pending";

            public static readonly int STATUS_ID_IN_REVIEWED = 2;
            public static readonly string STATUS_NAME_REVIEWED = "Reviewed";

            public static readonly int STATUS_ID_ACKNOWLEDGED = 3;
            public static readonly string STATUS_NAME_ACKNOWLEDGED = "Acknowledged";

            public static readonly int STATUS_ID_CLOSED = 4;
            public static readonly string STATUS_NAME_CLOSED = "Closed";

            public static readonly int STATUS_ID_AUTHORIZE = 5;
            public static readonly string STATUS_NAME_AUTHORIZE = "Authorized";

            public static readonly int STATUS_ID_APPROVED = 6;
            public static readonly string STATUS_NAME_APPROVED = "Approved";

            public static readonly int STATUS_ID_REJECT = 7;
            public static readonly string STATUS_NAME_REJECT = "Rejected";

            public static readonly int STATUS_ID_DRAFT = 8;
            public static readonly string STATUS_NAME_DRAFT = "Draft";

            #endregion

            #region SAVERITY

            public static readonly int SAVERITY_ID_LOW = 1;
            public static readonly string SAVERITY_NAME_LOW = "Low";

            public static readonly int SAVERITY_ID_HIGN = 2;
            public static readonly string SAVERITY_NAME_HIGH = "High";

            public static readonly int SAVERITY_ID_MEDIUM = 3;
            public static readonly string SAVERITY_NAME_MEDIUM = "Medium";

            #endregion

          

            #region Users

            public static readonly int SYSTEM_USER_ID = 1111;

            #endregion
        }

        public static class ApplicationMessages
        {
            public static readonly string ERROR_GENERAL = "Application error. Exception: {0}. Stack trace: {1}";
            public static readonly string ERROR_GENERAL_FRIENDLY = "Application encountered an exception.";
            public static readonly string ERROR_DB_MIGRATION = "Error migrating database. Exception: {exception}. Stack trace: {stacktrace}";
            public static readonly string EMPTY_REQUEST = "Request is empty.";
            public static readonly string BAD_REQUEST = "Invalid request.";
            public static readonly string SUCCESS_CREATED = "{0} is created successfully.";
            public static readonly string SUCCESS_UPDATED = "{0} updated successfully.";
            public static readonly string SUCCESS_DELETED = "Data deleted successfully.";
            public static readonly string SUCCESS_APPROVED = "{0} approved/rejected successfully.";
            public static readonly string SUCCESS_CANCELLED = "Data cancelled successfully.";
            public static readonly string SUCCESS_SEND_EMAIL = "Email sent successfully.";
            public static readonly string FAILED_SEND_EMAIL = "Email sent successfully.";
            public static readonly string INVALID_LOGIN = "Invalid username or password.";
            public static readonly string FAILED_REGISTRATION = "Account already exists, please log in to continue.";
            public static readonly string NOT_EMPTY = "{0} must be entered.";
            public static readonly string EXISTS = "{0} already exists.";
            public static readonly string NOT_EXISTS = "{0} does not exist.";
            public static readonly string MAPPED = "{0} is mapped.";
            public static readonly string EMAIL_NOT_CONFIRMED = "Email not verified.";
            public static readonly string ROLE_NOT_FOUND = "Role not found. Please contact your support team for assistance.";
            public static readonly string EMAIL_VERIFICATION_SUCCESSFUL = "Email Verification Successful for {0}.";
            public static readonly string PASSWORD_CHANGED_SUCCESSFULLY = "Password changed successfully for {0}.";
            public static readonly string DATA_FETCHED_SUCCESSFULLY = "Data Fetched Successfully.";
            public static readonly string DATA_NOT_FOUND = "Data not found.";
            public static readonly string FILE_UPLOAD_FAILED = "File upload failed.";
            public static readonly string FILE_UPLOADED = "File uploaded successfully.";
            public static readonly string FILE_SYNCED_TO_PERMANENT = "Files are synced successfully.";
            public static readonly string NO_RECORD_TO_EXPORT = "No Records found to export.";
            public static readonly string MODULE_NOT_FOUND = "Module Not Found.";
            public static readonly string DRAFTED = "{0} is drafted successfully.";
            public static readonly string NO_SOS_PENDING = "No pending SOS reports found.";
            public static readonly string GENERIC_ERROR = "An unexpected error occurred. Please try again or contact support.";
            public static readonly string NOT_FOUND_WITH_ID = "{0} not found with Id {1}.";
            public static readonly string NOT_FOUND = "{0} not found.";
            public static readonly string INVALID_ARGUMENT = "Invalid {0} {1}";

        }

        public static class ApplicationFields
        {
            public static readonly string USERNAME = "Username";
            public static readonly string PASSWORD = "Password";
            public static readonly string EMAIL = "Email";
            public static readonly string NAME = "Name";
            public static readonly string FRIENDLY_NAME = "Friendly Name";
            public static readonly string DISPLAY_NAME = "Display Name";
            public static readonly string ID = "ID";
            public static readonly string COUNTRY_ISO2_CODE = "Country Iso2 Code";
            public static readonly string DISPLAY_ID = "Display ID";
            public static readonly string DIVISION = "Division";
            public static readonly string DESIGNATION = "Designation";
            public static readonly string STATUS = "Status";
            public static readonly string COMMENTDESC = "Remarks";

        }

        public static class CustomClaimTypes
        {
            public static readonly string EmployeeId = "employee_id";
            public static readonly string TokenVersion = "token_version";
            public static readonly string Locations = "locations";
            public static readonly string Roles = "roles";
            public static readonly string Apps = "apps";
        }
    }
}
