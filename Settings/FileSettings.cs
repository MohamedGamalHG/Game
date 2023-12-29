namespace firstAppAsp.Settings
{
    public static class FileSettings
    {
        public const string ImagePath = "/assets/images/games";
        public const string AllowExentions = ".jpg,.jpeg,.png";
        public const int MaxFileSizeInMB = 1;
        public const int MaxFileSizeInByte = MaxFileSizeInMB * 1024 *1024;
    }
}
