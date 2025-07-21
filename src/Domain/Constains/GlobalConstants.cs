using System.Text.RegularExpressions;

namespace Domain.Constants;

public static class GlobalConstants
{
    public struct PageConfig
    {
        public const int Start = 1;
        public const int Length = 10;
        public const int MaxLength = 500;
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

    public struct RegexPatterns
    {
        public const string SemanticVersion = @"^(0|[1-9]\d*)\.(0|[1-9]\d*)\.(0|[1-9]\d*)(?:-((?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*)(?:\.(?:0|[1-9]\d*|\d*[a-zA-Z-][0-9a-zA-Z-]*))*))?$";
        public static readonly string SematicVersionAndLatest = $"^({Regex.Escape(Version.Latest)}|{SemanticVersion.Trim('^', '$')})$";
    }

    public struct Files
    {
        public const string ExcelContentType = "application/excel";
    }

    public struct Session
    {
        public const int IdLength = 15;
        public const int SessionDurationHours = 1;
    }

    public struct Headers
    {
        public const string DeviceFingerprint = "X-Device-Fingerprint";
        public const string UserAgent = "User-Agent";
        public const string SessionId = "X-Session-Id";
    }
}