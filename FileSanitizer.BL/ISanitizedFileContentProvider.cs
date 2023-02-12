namespace FileSanitizer.BL
{
    public interface ISanitizedFileContentProvider
    {
        string GetSanitizedFileContent(Stream input, string fileExtention);
    }
}