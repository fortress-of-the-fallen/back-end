using System.Text.RegularExpressions;

namespace Domain.Constants;

public static class GlobalConstants
{
    public const string JwtLoginToken = "JwtLoginToken";
    public struct Claim
    {
        public const string UserId = "UserId";
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        public const string Email = "Email";
    }

    public struct SwaggerConverterType
    {
        public const string String = "string";
        public const string TimeSpanExampleFormat = "00:00:00";
    }

    public struct Header
    {
        public const string AuthScheme = "Bearer";
        public const string Authorization = "Authorization";
        public const string DevModeScheme = "DevToken";
    }

    public struct PageConfig
    {
        public const int Start = 1;
        public const int Length = 10;
        public const int MaxLength = 500;
    }

    public struct SortDirection
    {
        public const string Ascending = "asc";
        public const string Descending = "desc";
    }

    public struct MaxBatchSize
    {
        public const int MaxBatch100 = 100;
        public const int MaxBatch1000 = 1000;
        public const int MaxBatch10000 = 10000;
        public const int MaxBatch100000 = 100000;
        public const int MaxBatch1000000 = 1000000;
    }

    public struct Tenant
    {
        public static readonly Guid DefaultTenant = new("f54a56b7-c352-4248-8a56-b7c352f248cc");
    }

    public struct MimeTypes
    {
        public const string TextPlain = "text/plain";
        public const string TextHtml = "text/html";
        public const string TextCss = "text/css";
        public const string TextJavaScript = "text/javascript";

        public const string ImageJpeg = "image/jpeg";
        public const string ImagePng = "image/png";
        public const string ImageGif = "image/gif";
        public const string ImageSvg = "image/svg+xml";

        public const string AudioMp3 = "audio/mpeg";
        public const string AudioOgg = "audio/ogg";

        public const string VideoMp4 = "video/mp4";
        public const string VideoWebm = "video/webm";

        public const string ApplicationOctetStream = "application/octet-stream";
        public const string ApplicationZip = "application/zip";
        public const string ApplicationXZip = "application/x-zip-compressed";
        public const string ApplicationRar = "application/x-rar-compressed";
        public const string ApplicationX = "application/x-compressed";
        public const string ApplicationXRar = "application/x-rar";
        public const string ApplicationVndRar = "application/vnd.rar";
        public const string ApplicationPdf = "application/pdf";
        public const string ApplicationExcel = "application/vnd.ms-excel";
        public const string ApplicationWord = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
    }

    public struct FileExtension
    {
        public const string Zip = ".zip";
        public const string Rar = ".rar";
    }

    public struct FileName
    {
        public const string Asset = "Assets.zip";
        public const string WorkItems = "WorkItems.zip";
        public const string JsonInputData = "InputData.json";
        public const string ZipInputData = "InputData.zip";
    }

    public struct Version
    {
        public const string Latest = "latest";
        public const int LatestVersion = 999999;
    }

    public struct DemoWorkItem
    {
        public static readonly Guid DemoWorkQueue = new("28C85649-1DB2-4876-8216-E6D1D2A0CFA0");

        public struct DemoWorkItemStatus
        {
            public static readonly Guid Processing = new("2e8d463a-76af-4fb0-884c-7777be044c13");
            public static readonly Guid Failed = new("df8bc8bc-78ff-4894-895a-b25b0c207534");
            public static readonly Guid Matched = new("b89e8850-b7fe-459a-98aa-bb83c9a7799d");
            public static readonly Guid Discrepancy = new("850cb9d8-9789-45bc-9050-e822289db9e6");
            public static readonly Guid Approved = new("c42e1959-3823-4978-9d94-d0c06001eff6");
            public static readonly Guid Rejected = new("f04a2a58-42aa-400e-85e1-f757b8348dc1");
        }
    }

    public struct RegexPatterns
    {
        public const string SemanticVersion = @"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?$";
        public static readonly string SematicVersionAndLatest = $"^({Regex.Escape(Version.Latest)}|{SemanticVersion.Trim('^', '$')})$";
    }

    public struct JwtScheme
    {
        public const string DevMode = "DevModeScheme";
        public const string Dynamic = "DynamicJwtScheme";
    }

    public struct DevMode
    {
        public const string ClaimIdenity = "DevMode";

        public struct Workflow
        {
            public const string DefaultVersion = "latest";
            public const string DummyPrefix = "Dummy-";
            public const string DummyDescription = "Dummy workflow for dev mode";
        }
    }

    public struct EmailTemplate
    {
        public const string ResetPasswordSubject = "Reset your password.";
        public const string SuccessResetPasswordSubject = "Reset password success notification.";
        public const string ChangePasswordSubject = "Change password notification.";
        public const string NewDeviceSubject = "New device login notification.";
    }

    public struct Files
    {
        public const string ExcelContentType = "application/excel";
    }
}